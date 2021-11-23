// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Extensions.Options;
using System.Linq;
using Gardener.UserCenter.Dtos;
using Gardener.Authorization.Dtos;
using Gardener.UserCenter.Services;
using Gardener.UserCenter.Enums;
using Gardener.Client.Base;
using Microsoft.AspNetCore.Components;

namespace Gardener.Client.Core
{
    
    /// <summary>
    /// 身份状态管理
    /// </summary>
    public class AuthenticationStateManager : IAuthenticationStateManager
    {
        private readonly IJsTool jsTool;
        private readonly HttpClientManager httpClientManager;
        private readonly IAccountService accountService;
        private readonly IClientLogger logger;
        private UserDto currentUser;
        private bool currentUserIsSuperAdmin = false;
        private List<string> uiResourceKeys;
        private List<ResourceDto> menuResources;
        private Hashtable uiHashtableResources;
        private NavigationManager navigationManager;
        /// <summary>
        /// 登录的时候选中记住我/自动登录时，refre token 记录到 localsession中
        /// </summary>
        private bool isAutoLogin = true;
        private AuthSettings authSettings;

        public AuthenticationStateManager(IJsTool jsTool, HttpClientManager httpClientManager, IAccountService accountService, IClientLogger logger, IOptions<AuthSettings> authSettingsOpt, NavigationManager navigationManager)
        {
            this.authSettings = authSettingsOpt.Value;
            this.jsTool = jsTool;
            this.httpClientManager = httpClientManager;
            this.accountService = accountService;
            this.logger = logger;
            timer = new Timer(TimerCallback, true, 10000, authSettings.RefreshTokenCheckInterval * 1000);
            this.navigationManager = navigationManager;
        }

        #region refresh token job
        /// <summary>
        /// 定时器 用来刷新token
        /// </summary>
        private Timer timer;
        /// <summary>
        /// 定时执行方法 检查已拿到的token是否需要刷新
        /// </summary>
        /// <param name="state"></param>
        private async void TimerCallback(object state)
        {
            if (navigationManager.Uri.IndexOf("/auth/login") > 0) 
            {
                return;
            }
            TokenOutput currentToken =await GetCurrentToken();
            //未登录
            if (currentToken == null) return;
            //RefreshToken已经过期了
            if (currentToken.RefreshTokenExpires < DateTimeOffset.Now.ToUnixTimeSeconds()) { await Logout(); return; }
            //AccessToken时间还很充裕
            if (currentToken.AccessTokenExpires - DateTimeOffset.Now.ToUnixTimeSeconds() > authSettings.RefreshTokenTimeThreshold) return;
            //拿到新的token
            var tokenResult = await accountService.RefreshToken(new RefreshTokenInput() { RefreshToken = currentToken.RefreshToken });
            if (tokenResult != null)
            {
                //token 设置
                await SetToken(tokenResult);
                await logger.Debug($"token refresh successed {DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")}");
            }
            else
            {
                //失败时退出登录
                //await Logout();
            }
        }
        #endregion

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="token"></param>
        /// <param name="isAutoLogin">自动登录</param>
        /// <returns></returns>
        public async Task Login(TokenOutput token, bool isAutoLogin = true)
        {
            this.isAutoLogin = isAutoLogin;
            await SetToken(token);
            notifyAuthenticationStateChangedAction();
        }
        /// <summary>
        /// 注销
        /// </summary>
        public async Task Logout()
        {
            await accountService.RemoveCurrentUserRefreshToken();
            await CleanUserInfo();
        }
        /// <summary>
        /// CleanClientLoginInfo
        /// </summary>
        public async Task CleanUserInfo()
        {
            //移除所有tab
            ClientNavTabControl.RemoveAllNavTabPage();
            this.currentUser = null;
            this.uiResourceKeys = null;
            this.uiHashtableResources = null;
            this.menuResources = null;
            await RemoveToken();
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

        #region userinfos
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        public async Task<UserDto> GetCurrentUser()
        {
            return currentUser;
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        public async Task ReloadCurrentUserInfos()
        {
            //刷新了，或者首次登录
            var token = await ReloadToken();
            if (token != null)
            {
                //重新请求user信息
                var userResult = await accountService.GetCurrentUser();
                if (userResult != null)
                {
                    //超级管理员
                    currentUserIsSuperAdmin = userResult.Roles!=null && userResult.Roles.Any(x => x.IsSuperAdministrator);
                    this.uiResourceKeys = await accountService.GetCurrentUserResourceKeys(ResourceType.View,ResourceType.Menu,ResourceType.Action);
                    this.uiHashtableResources = null;
                    this.menuResources = await accountService.GetCurrentUserMenus(AuthConstant.ClientResourceRootKey);
                    this.currentUser = userResult;
                    onMenusLoaded.Invoke(this.menuResources);
                }
            }
        }
        private Action<List<ResourceDto>> onMenusLoaded = (menus) => { };
        public void SetOnMenusLoaded(Action<List<ResourceDto>> action)
        {
            this.onMenusLoaded = action;
        }
        private List<string> GetCurrentUserUiResourceKeys()
        {
            return uiResourceKeys ?? new List<string>();
        }
        public async Task<bool> CheckCurrentUserHaveBtnResourceKey(object key)
        {
            //超级管理员
            if (currentUserIsSuperAdmin) return true;

            if (uiHashtableResources == null)
            {
                var resources = GetCurrentUserUiResourceKeys();
                uiHashtableResources = new Hashtable(resources.Count);
                resources.ForEach(x => { uiHashtableResources.Add(x, null); });
            }
            return uiHashtableResources.ContainsKey(key);
        }
        public List<ResourceDto> GetCurrentUserEmnus()
        {
            return this.menuResources ?? new List<ResourceDto>() ;
        }
        #endregion

        #region set httpclient token


        /// <summary>
        /// 给httpclient设置验证token
        /// </summary>
        /// <param name="token"></param>
        private void SetHttpClientAuthorization(string token)
        {
            //httpClient.Authenticator = new JwtAuthenticator(token);
            httpClientManager.SetClientAuthorization(token);
        }
        #endregion

        #region local token  
        private async Task<TokenOutput> GetCurrentToken()
        {
            var isAutoLoginStr = await jsTool.LocalStorage.GetAsync<string>(nameof(isAutoLogin));
            this.isAutoLogin = string.IsNullOrEmpty(isAutoLoginStr) ? false : bool.Parse(isAutoLoginStr);
            var accessTokenExpiresLocal = await GetWebStorageFromAutoLogin(isAutoLogin).GetAsync<string>(nameof(TokenOutput.AccessTokenExpires));
            var accessTokenLocal = await GetWebStorageFromAutoLogin(isAutoLogin).GetAsync<string>(nameof(TokenOutput.AccessToken));
            var refreshTokenLocal = await GetWebStorageFromAutoLogin(this.isAutoLogin).GetAsync<string>(nameof(TokenOutput.RefreshToken));
            var refreshTokenExpiresLocal = await GetWebStorageFromAutoLogin(this.isAutoLogin).GetAsync<string>(nameof(TokenOutput.RefreshTokenExpires));
            //refretoken无效
            if (string.IsNullOrEmpty(refreshTokenLocal) || string.IsNullOrEmpty(refreshTokenExpiresLocal)) return null;
            var token = new TokenOutput
            {
                AccessToken = accessTokenLocal,
                RefreshToken = refreshTokenLocal,
                AccessTokenExpires = long.Parse(accessTokenExpiresLocal),
                RefreshTokenExpires = long.Parse(refreshTokenExpiresLocal)
            };
            return token;
;
        }

        /// <summary>
        /// get token
        /// </summary>
        /// <returns></returns>
        private async Task<TokenOutput> ReloadToken()
        {
            TokenOutput token = await GetCurrentToken();
            //无效
            if (token==null) return null;
            //accessToken可用
            if (!string.IsNullOrEmpty(token.AccessToken) && token.AccessTokenExpires - DateTimeOffset.Now.ToUnixTimeSeconds() > authSettings.RefreshTokenTimeThreshold)
            {
                await SetToken(token);
                return token;
            }
            ///accessToken不可用，使用refreshToken拿到新的token
            var newToken = await accountService.RefreshToken(new RefreshTokenInput() { RefreshToken = token.RefreshToken });
            if (newToken == null) return null;
            await SetToken(newToken);
            return newToken;
        }
        /// <summary>
        /// token 设置到浏览器缓存
        /// </summary>
        /// <param name="loginOutput"></param>
        /// <returns></returns>
        private async Task SetToken(TokenOutput token)
        {
            await GetWebStorageFromAutoLogin(isAutoLogin).SetAsync(nameof(TokenOutput.AccessToken), token.AccessToken);
            await GetWebStorageFromAutoLogin(isAutoLogin).SetAsync(nameof(TokenOutput.AccessTokenExpires), token.AccessTokenExpires);
            await GetWebStorageFromAutoLogin(isAutoLogin).SetAsync(nameof(TokenOutput.RefreshToken), token.RefreshToken);
            await GetWebStorageFromAutoLogin(isAutoLogin).SetAsync(nameof(TokenOutput.RefreshTokenExpires), token.RefreshTokenExpires);
            await jsTool.LocalStorage.SetAsync(nameof(isAutoLogin), isAutoLogin);
            SetHttpClientAuthorization(token.AccessToken);
        }
        /// <summary>
        ///  移除浏览器token缓存
        /// </summary>
        private async Task RemoveToken()
        {
            await GetWebStorageFromAutoLogin(isAutoLogin).RemoveAsync(nameof(TokenOutput.AccessToken));
            await GetWebStorageFromAutoLogin(isAutoLogin).RemoveAsync(nameof(TokenOutput.AccessTokenExpires));
            await GetWebStorageFromAutoLogin(isAutoLogin).RemoveAsync(nameof(TokenOutput.RefreshToken));
            await GetWebStorageFromAutoLogin(isAutoLogin).RemoveAsync(nameof(TokenOutput.RefreshTokenExpires));
            await jsTool.LocalStorage.RemoveAsync(nameof(isAutoLogin));
            SetHttpClientAuthorization("");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IWebStorage GetWebStorageFromAutoLogin(bool isAutoLogin)
        {
            return isAutoLogin ? jsTool.LocalStorage : jsTool.SessionStorage;
        }
        #endregion
        /// <summary>
        /// 获取当前身份的token头，可以添加于自定义的httpclient中验证使用
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> GetCurrentTokenHeaders()
        {
            TokenOutput currentToken =await GetCurrentToken();
            if (currentToken == null) return null;

            return new Dictionary<string, string>
            {
                {"authorization","Bearer "+currentToken.AccessToken }
            };
        }
    }
}
