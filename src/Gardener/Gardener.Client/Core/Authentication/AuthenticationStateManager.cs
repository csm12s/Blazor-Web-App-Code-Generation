// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Services;
using Gardener.Application.Dtos;
using System;
using System.Threading.Tasks;
using System.Threading;
using Gardener.Enums;
using System.Collections.Generic;
using Gardener.Application.Interfaces;
using Gardener.Client.Constants;

namespace Gardener.Client
{
    /// <summary>
    /// 验证状态管理
    /// </summary>
    public interface IAuthenticationStateManager
    {
        Task<UserDto> GetCurrentUser();
        Task Login(TokenOutput loginOutput,bool autoLogin=true);
        Task Logout();
        void SetNotifyAuthenticationStateChangedAction(Action c);
        /// <summary>
        /// 获取用户拥有的资源
        /// </summary>
        /// <returns></returns>
        List<ResourceDto> GetCurrentUserResources();
        /// <summary>
        /// 刷新用户拥有的资源
        /// </summary>
        /// <returns></returns>
        Task<List<ResourceDto>> RefreshUserResources();
    }
    public class AuthenticationStateManager : IAuthenticationStateManager
    {
        private readonly JsTool jsTool;
        private readonly HttpClientManager httpClientManager;
        private readonly IAuthorizeService authorizeService;
        private readonly ILogger logger;

        private int RefreshTokenErrorCount = 0;
        private UserDto currentUser;
        private List<ResourceDto> resources;
        private TokenOutput localToken;
        /// <summary>
        /// 登录的时候选中记住我/自动登录时，refre token 记录到 localsession中
        /// </summary>
        private bool autoLogin=true;
        /// <summary>
        /// 定时器 用来刷新token
        /// </summary>
        private Timer timer;
        public AuthenticationStateManager(JsTool jsTool, HttpClientManager httpClientManager, IAuthorizeService authorizeService, ILogger logger)
        {
            this.jsTool = jsTool;
            this.httpClientManager = httpClientManager;
            this.authorizeService = authorizeService;
            this.logger = logger;
            timer = new Timer(TimerCallback, true, 10000, AuthConstant.RefreshTokenCheckInterval * 1000);
        }

        #region refresh token job
        /// <summary>
        /// 定时执行方法
        /// </summary>
        /// <param name="state"></param>
        private async void TimerCallback(object state)
        {
            //未登录
            if (localToken == null) return;
            //RefreshToken已经过期了
            if (localToken.RefreshTokenExpires < DateTimeOffset.Now.ToUnixTimeSeconds()) { await Logout(); return; }
            //AccessToken时间还很充裕
            if (localToken.AccessTokenExpires - DateTimeOffset.Now.ToUnixTimeSeconds() > AuthConstant.RefreshTokenTimeThreshold) return;
            //拿到新的token
            var tokenResult = await authorizeService.RefreshToken(new RefreshTokenInput() { RefreshToken = localToken.RefreshToken });
            if (tokenResult != null)
            {
                RefreshTokenErrorCount = 0;
                //token 设置
                await SetLocalToken(tokenResult);
                await logger.Debug($"token refresh successed {DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")}");
            }
            else
            {
                RefreshTokenErrorCount++;
                if (RefreshTokenErrorCount > AuthConstant.RefreshTokenErrorCountMax)
                {
                    //失败时退出登录
                    await Logout();
                }
            }
        }
        #endregion

        /// <summary>
        /// 标记授权
        /// </summary>
        /// <param name="token"></param>
        /// <param name="autoLogin">自动登录</param>
        /// <returns></returns>
        public async Task Login(TokenOutput token, bool autoLogin = true)
        {
            this.autoLogin = autoLogin;
            this.localToken = token;
            await SetLocalToken(token);
            
            notifyAuthenticationStateChangedAction();
        }
        /// <summary>
        /// 标记注销
        /// </summary>
        public async Task Logout()
        {
            await authorizeService.RemoveCurrentUserRefreshToken();
            await RemoveLocalIdentity();
            notifyAuthenticationStateChangedAction();
        }

        #region 状态通知
        Action notifyAuthenticationStateChangedAction = () => { };
        /// <summary>
        /// 设置状态通知回调
        /// </summary>
        /// <param name="c"></param>
        public void SetNotifyAuthenticationStateChangedAction(Action c)
        {
            notifyAuthenticationStateChangedAction = c;
        }
        #endregion

        /// <summary>
        /// 刷新用户
        /// </summary>
        /// <returns></returns>
        public async Task<UserDto> GetCurrentUser()
        {
            if (currentUser != null)
                return currentUser;
            if (localToken == null)
            {
                //从浏览器重新加载token
                var token=await GetLocalRefreshToken();
                if (token != null)
                    await SetLocalToken(token);
                else
                    await RemoveLocalIdentity();
            }
            if (localToken != null)
            {
                //请求
                var userResult = await authorizeService.GetCurrentUser();
                if (userResult != null)
                {
                    this.currentUser = userResult;
                    await RefreshUserResources();
                    return userResult;
                }
            }
            return null;
        }

        #region Resource
        public List<ResourceDto> GetCurrentUserResources()
        {
            return resources;
        }
        /// <summary>
        /// 刷新用户拥有的资源
        /// </summary>
        /// <returns></returns>
        public async Task<List<ResourceDto>> RefreshUserResources()
        {
            if (currentUser == null) return null;
            var result = await authorizeService.GetCurrentUserResources(ResourceType.BUTTON, ResourceType.MENU);
            resources = result ?? new List<ResourceDto>();
            return resources;
        }
        #endregion

        #region private
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IWebStorage GetWebStorageFromAutoLogin()
        {
            return autoLogin ? jsTool.LocalStorage : jsTool.SessionStorage;
        }
        /// <summary>
        /// 检测本地是否有可用RefreshToken,有的话使用
        /// </summary>
        /// <returns></returns>
        private async Task<TokenOutput> GetLocalRefreshToken()
        {
            //从本地还原
            var accessTokenExpiresLocal = await jsTool.SessionStorage.GetAsync<string>(nameof(TokenOutput.AccessTokenExpires));
            var accessTokenLocal = await jsTool.SessionStorage.GetAsync<string>(nameof(TokenOutput.AccessToken));

            var refreshTokenLocal = await GetWebStorageFromAutoLogin().GetAsync<string>(nameof(TokenOutput.RefreshToken));
            var refreshTokenExpiresLocal = await GetWebStorageFromAutoLogin().GetAsync<string>(nameof(TokenOutput.RefreshTokenExpires));
            if (string.IsNullOrEmpty(refreshTokenLocal) || string.IsNullOrEmpty(refreshTokenExpiresLocal))
            {
                return null;
            }
            //accessToken可用
            if (!string.IsNullOrEmpty(accessTokenExpiresLocal) && long.Parse(accessTokenExpiresLocal) - DateTimeOffset.Now.ToUnixTimeSeconds() > AuthConstant.RefreshTokenTimeThreshold
                && !string.IsNullOrEmpty(accessTokenLocal) 
                )
            {
                return new TokenOutput
                {
                    AccessToken = accessTokenLocal,
                    RefreshToken = refreshTokenLocal,
                    AccessTokenExpires = long.Parse(accessTokenExpiresLocal),
                    RefreshTokenExpires = long.Parse(refreshTokenExpiresLocal)
                };
            }
            //使用refreshToken
            //拿到新的token
            var tokenResult = await authorizeService.RefreshToken(new RefreshTokenInput() { RefreshToken = refreshTokenLocal });
            return tokenResult;
        }
        /// <summary>
        /// 给httpclient设置验证token
        /// </summary>
        /// <param name="token"></param>
        private void SetHttpClientAuthorization(string token)
        {
            //httpClient.Authenticator = new JwtAuthenticator(token);
            httpClientManager.SetClientAuthorization(token);
        }
        /// <summary>
        /// token 设置到浏览器缓存 和 httpclient 头部
        /// </summary>
        /// <param name="loginOutput"></param>
        /// <returns></returns>
        private async Task SetLocalToken(TokenOutput token)
        {
            this.localToken = token;
            await jsTool.SessionStorage.SetAsync(nameof(TokenOutput.AccessToken), token.AccessToken);
            await jsTool.SessionStorage.SetAsync(nameof(TokenOutput.AccessTokenExpires), token.AccessTokenExpires);
            await GetWebStorageFromAutoLogin().SetAsync(nameof(TokenOutput.RefreshToken), token.RefreshToken);
            await GetWebStorageFromAutoLogin().SetAsync(nameof(TokenOutput.RefreshTokenExpires), token.RefreshTokenExpires);
            SetHttpClientAuthorization(token.AccessToken);
        }
        /// <summary>
        ///  移除浏览器token缓存
        ///  移除httpclient 头部token
        ///  本地currentUser设置为null
        /// </summary>
        private async Task RemoveLocalIdentity()
        {
            await jsTool.SessionStorage.RemoveAsync(nameof(TokenOutput.AccessToken));
            await jsTool.SessionStorage.RemoveAsync(nameof(TokenOutput.AccessTokenExpires));
            await GetWebStorageFromAutoLogin().RemoveAsync(nameof(TokenOutput.RefreshToken));
            await GetWebStorageFromAutoLogin().RemoveAsync(nameof(TokenOutput.RefreshTokenExpires));
            SetHttpClientAuthorization("");
            currentUser = null;
            localToken = null;
        }
        #endregion
    }
}
