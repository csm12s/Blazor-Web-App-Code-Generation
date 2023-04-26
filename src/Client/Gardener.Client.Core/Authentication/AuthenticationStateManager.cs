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
using Gardener.Client.Base;
using Microsoft.AspNetCore.Components;
using Gardener.Client.Base.EventBus.Events;
using Gardener.EventBus;
using Gardener.SystemManager.Dtos;
using Gardener.Base.Enums;

namespace Gardener.Client.Core
{

    /// <summary>
    /// 身份状态管理
    /// </summary>
    public class AuthenticationStateManager : IAuthenticationStateManager
    {
        private UserDto? currentUser;
        private List<string>? uiResourceKeys;
        private List<ResourceDto>? menuResources;
        private Hashtable? uiHashtableResources;

        private readonly IJsTool jsTool;
        private readonly HttpClientManager httpClientManager;
        private readonly IAccountService accountService;
        private readonly IClientLogger logger;
        private readonly NavigationManager navigationManager;
        private readonly IEventBus eventBus;
        /// <summary>
        /// 登录的时候选中记住我/自动登录时，refre token 记录到 localsession中
        /// </summary>
        private bool isAutoLogin = true;
        private AuthSettings authSettings;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsTool"></param>
        /// <param name="httpClientManager"></param>
        /// <param name="accountService"></param>
        /// <param name="logger"></param>
        /// <param name="authSettingsOpt"></param>
        /// <param name="navigationManager"></param>
        /// <param name="eventBus"></param>
        public AuthenticationStateManager(IJsTool jsTool, HttpClientManager httpClientManager, IAccountService accountService, IClientLogger logger, IOptions<AuthSettings> authSettingsOpt, NavigationManager navigationManager, IEventBus eventBus)
        {
            this.authSettings = authSettingsOpt.Value;
            this.jsTool = jsTool;
            this.httpClientManager = httpClientManager;
            this.accountService = accountService;
            this.logger = logger;
            if (authSettings.EnableAutoRefresh)
            {
                timer = new Timer(TimerCallback, true, 10000, authSettings.RefreshTokenCheckInterval * 1000);
            }
            this.navigationManager = navigationManager;
            this.eventBus = eventBus;
        }

        #region refresh token job
        /// <summary>
        /// 定时器 用来刷新token
        /// </summary>
        private Timer? timer;
        /// <summary>
        /// 定时执行方法 检查已拿到的token是否需要刷新
        /// </summary>
        /// <param name="state"></param>
        private async void TimerCallback(object? state)
        {
            //刷新token
            await RefreshToken();
        }
        #endregion

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="force">强制刷新</param>
        /// <returns></returns>
        public async Task<bool> RefreshToken(bool force = false)
        {
            //如果在登录页，无法刷新
            if (navigationManager.Uri.IndexOf(authSettings.LoginPagePath) > 0)
            {
                return false;
            }
            TokenOutput? currentToken = await GetCurrentToken();
            //未登录
            if (currentToken == null) { await CleanUserInfo(); return false; }
            if (!force)
            {
                //RefreshToken已经过期了
                if (currentToken.RefreshTokenExpires < DateTimeOffset.Now.ToUnixTimeSeconds()) { await CleanUserInfo(); return false; }
                //AccessToken时间还很充裕
                if (currentToken.AccessTokenExpires - DateTimeOffset.Now.ToUnixTimeSeconds() > authSettings.RefreshTokenTimeThreshold) { return false; }
            }
            //拿到新的token
            var tokenResult = await accountService.RefreshToken(new RefreshTokenInput() { RefreshToken = currentToken.RefreshToken });
            if (tokenResult != null)
            {
                eventBus.Publish(new RefreshTokenSucceedAfterEvent(tokenResult));
                //token 设置
                await SetToken(tokenResult);
                logger.Debug($"token refresh successed {DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")}");
                return true;
            }
            else
            {
                await CleanUserInfo();
                return false;
            }
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="token"></param>
        /// <param name="isAutoLogin">自动登录</param>
        /// <returns></returns>
        public async Task Login(TokenOutput token, bool isAutoLogin = true)
        {
            //自动加载
            this.isAutoLogin = isAutoLogin;
            //设置token
            await SetToken(token);
            //加载当前用户信息
            await ReloadCurrentUserInfos();
            //通知状态变更
            notifyAuthenticationStateChangedAction();
            //发送一个登录成功事件
            eventBus.Publish(new LoginSucceedAfterEvent(token));

        }
        /// <summary>
        /// 注销
        /// </summary>
        public async Task Logout()
        {
            await accountService.RemoveCurrentUserRefreshToken();
            await CleanUserInfo();
            eventBus.Publish(new LogoutSucceedAfterEvent());
        }
        /// <summary>
        /// CleanClientLoginInfo
        /// </summary>
        public async Task CleanUserInfo()
        {
            //移除所有tab
            this.currentUser = null;
            this.uiResourceKeys = null;
            this.uiHashtableResources = null;
            this.menuResources = null;
            await RemoveToken();
            notifyAuthenticationStateChangedAction();

        }
        #region 状态通知
        /// <summary>
        /// 用户身份变化需要调用此通知
        /// </summary>
        /// <remarks>
        /// <see cref="CustomAuthenticationStateProvider"/>
        /// </remarks>
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
        public Task<UserDto?> GetCurrentUser()
        {
            return Task.FromResult(currentUser);
        }

        /// <summary>
        /// 重新加载用户相关信息
        /// </summary>
        /// <returns></returns>
        public async Task<(UserDto?, bool?, List<ResourceDto>?, List<string>?)> ReloadCurrentUserInfos()
        {
            //刷新了，或者首次登录
            var token = await TryGetToken();
            if (token != null)
            {
                SetHttpClientAuthorization(token.AccessToken);
                //重新请求user信息
                var userResult = await accountService.GetCurrentUser();
                if (userResult != null)
                {
                    this.uiHashtableResources = null;
                    this.currentUser = userResult;

                    //超级管理员
                    bool currentUserIsSuperAdmin = CurrentUserIsSuperAdmin();
                    var task1 = accountService.GetCurrentUserResourceKeys(ResourceType.View, ResourceType.Menu, ResourceType.Action);
                    var task2 = accountService.GetCurrentUserMenus(AuthConstant.ClientResourceRootKey);
                    var task3 = eventBus.PublishAsync(new ReloadCurrentUserEvent(token));

                    this.uiResourceKeys = await task1;
                    this.menuResources = await task2;
                    await task3;
                    onAuthenticationRefreshSuccessed.Invoke(this.currentUser, currentUserIsSuperAdmin, this.menuResources, this.uiResourceKeys);
                    return (userResult, currentUserIsSuperAdmin, this.menuResources, this.uiResourceKeys);
                }
            }
            return (null, null, null, null);
        }
        /// <summary>
        /// 当前用户是否是超级管理员
        /// </summary>
        /// <returns></returns>
        public bool CurrentUserIsSuperAdmin()
        {
            if (this.currentUser == null)
            {
                return false;
            }
            return this.currentUser.Roles != null && this.currentUser.Roles.Any(x => x.IsSuperAdministrator);
        }

        /// <summary>
        /// 身份刷新成功后
        /// </summary>
        private Action<UserDto, bool, List<ResourceDto>, List<string>> onAuthenticationRefreshSuccessed = (user, isSuperAdmin, menus, uiResourceKeys) => { };

        /// <summary>
        /// 身份刷新成功后
        /// </summary>
        /// <param name="action"></param>
        public void SetOnAuthenticationRefreshSuccessed(Action<UserDto, bool, List<ResourceDto>, List<string>> action)
        {
            this.onAuthenticationRefreshSuccessed = action;
        }
        private List<string> GetCurrentUserUiResourceKeys()
        {
            return uiResourceKeys ?? new List<string>();
        }
        /// <summary>
        /// 判断当前用户是否用于资源key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<bool> CheckCurrentUserHaveBtnResourceKey(object key)
        {
            //超级管理员
            if (CurrentUserIsSuperAdmin())
            {
                return Task.FromResult(true);
            }
            if (uiHashtableResources == null)
            {
                var resources = GetCurrentUserUiResourceKeys();
                uiHashtableResources = new Hashtable(resources.Count);
                resources.ForEach(x => { uiHashtableResources.Add(x, null); });
            }
            return Task.FromResult(uiHashtableResources.ContainsKey(key));
        }
        /// <summary>
        /// 获取当前用户菜单
        /// </summary>
        /// <returns></returns>
        public List<ResourceDto>? GetCurrentUserMenus()
        {
            return this.menuResources;
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<TokenOutput?> GetCurrentToken()
        {
            var task1 = jsTool.LocalStorage.GetAsync<string>(nameof(isAutoLogin));
            var task2 = GetWebStorageFromAutoLogin(isAutoLogin).GetAsync<string>(nameof(TokenOutput.AccessTokenExpires));
            var task3 = GetWebStorageFromAutoLogin(isAutoLogin).GetAsync<string>(nameof(TokenOutput.AccessToken));
            var task4 = GetWebStorageFromAutoLogin(this.isAutoLogin).GetAsync<string>(nameof(TokenOutput.RefreshToken));
            var task5 = GetWebStorageFromAutoLogin(this.isAutoLogin).GetAsync<string>(nameof(TokenOutput.RefreshTokenExpires));
            var isAutoLoginStr = await task1;
            var accessTokenExpiresLocal = await task2;
            var accessTokenLocal = await task3;
            var refreshTokenLocal = await task4;
            var refreshTokenExpiresLocal = await task5;
            this.isAutoLogin = string.IsNullOrEmpty(isAutoLoginStr) ? false : bool.Parse(isAutoLoginStr);
            //accessToken || refretoken 不存在，可能被破坏了
            if (string.IsNullOrEmpty(accessTokenLocal) || string.IsNullOrEmpty(accessTokenExpiresLocal) || string.IsNullOrEmpty(refreshTokenLocal) || string.IsNullOrEmpty(refreshTokenExpiresLocal)) return null;
            var token = new TokenOutput
            {
                AccessToken = accessTokenLocal,
                RefreshToken = refreshTokenLocal,
                AccessTokenExpires = long.Parse(accessTokenExpiresLocal),
                RefreshTokenExpires = long.Parse(refreshTokenExpiresLocal)
            };
            return token;
        }

        /// <summary>
        /// 尝试获取token
        /// </summary>
        /// <remarks>
        /// 先从本地获取，本地没有或不可用，尝试刷新一下再获取
        /// </remarks>
        /// <returns></returns>
        private async Task<TokenOutput?> TryGetToken()
        {
            TokenOutput? token = await GetCurrentToken();
            //无效
            if (token == null) return null;
            //accessToken可用
            if (!string.IsNullOrEmpty(token.AccessToken) && token.AccessTokenExpires - DateTimeOffset.Now.ToUnixTimeSeconds() > authSettings.RefreshTokenTimeThreshold)
            {
                return token;
            }
            else
            {
                ///accessToken不可用，强制刷新后拿到
                await RefreshToken(true);
                return await GetCurrentToken();
            }

        }
        /// <summary>
        /// token 设置到浏览器缓存
        /// </summary>
        /// <param name="loginOutput"></param>
        /// <returns></returns>
        private Task SetToken(TokenOutput token)
        {
            var task1 = GetWebStorageFromAutoLogin(isAutoLogin).SetAsync(nameof(TokenOutput.AccessToken), token.AccessToken);
            var task2 = GetWebStorageFromAutoLogin(isAutoLogin).SetAsync(nameof(TokenOutput.AccessTokenExpires), token.AccessTokenExpires);
            var task3 = GetWebStorageFromAutoLogin(isAutoLogin).SetAsync(nameof(TokenOutput.RefreshToken), token.RefreshToken);
            var task4 = GetWebStorageFromAutoLogin(isAutoLogin).SetAsync(nameof(TokenOutput.RefreshTokenExpires), token.RefreshTokenExpires);
            var task5 = jsTool.LocalStorage.SetAsync(nameof(isAutoLogin), isAutoLogin);
            SetHttpClientAuthorization(token.AccessToken);
            return Task.WhenAll(task1, task2, task3, task4, task5);
        }
        /// <summary>
        ///  移除浏览器token缓存
        /// </summary>
        private Task RemoveToken()
        {
            //httpClient clear
            SetHttpClientAuthorization("");
            //浏览器本地 clear
            var task1 = GetWebStorageFromAutoLogin(isAutoLogin).RemoveAsync(nameof(TokenOutput.AccessToken));
            var task2 = GetWebStorageFromAutoLogin(isAutoLogin).RemoveAsync(nameof(TokenOutput.AccessTokenExpires));
            var task3 = GetWebStorageFromAutoLogin(isAutoLogin).RemoveAsync(nameof(TokenOutput.RefreshToken));
            var task4 = GetWebStorageFromAutoLogin(isAutoLogin).RemoveAsync(nameof(TokenOutput.RefreshTokenExpires));
            var task5 = jsTool.LocalStorage.RemoveAsync(nameof(isAutoLogin));
            return Task.WhenAll(task1, task2, task3, task4, task5);
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
        public Task<Dictionary<string, string>?> GetCurrentTokenHeaders()
        {
            var auth = httpClientManager.GetAuthorizationHeaders();
            if (auth == null)
            {
                return Task.FromResult<Dictionary<string, string>?>(null);
            }
            return Task.FromResult<Dictionary<string, string>?>(new Dictionary<string, string>
            {
                {auth.Value.Key,auth.Value.Value }
            });
        }
        /// <summary>
        /// 测试token是否可用
        /// </summary>
        /// <param name="flag">标记</param>
        /// <returns></returns>
        /// <remarks>
        /// 不执行任何内容，token无效将响应401；
        /// 在特殊位置，不通过apicaller调用接口，无法实现token的被动刷新，就需要调用该方法去触发一下；
        /// 当然其他通过apicaller调用的接口也可以达到该效果；
        /// </remarks>
        public async Task<bool> TestToken(string? flag = null)
        {
            return await accountService.TestToken(flag);
        }
    }
}
