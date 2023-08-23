// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.LocalizationLocalizer;
using Gardener.UserCenter.Dtos;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gardener.Client.Core
{
    /// <summary>
    /// 自定义验证状态提供器
    /// </summary>
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthenticationStateManager authenticationStateManager;
        private readonly IClientLogger logger;
        private readonly ILocalizationLocalizer localizer;
        public CustomAuthenticationStateProvider(IAuthenticationStateManager authenticationStateManager, IClientLogger logger, ILocalizationLocalizer localizer)
        {
            this.authenticationStateManager = authenticationStateManager;
            authenticationStateManager.SetNotifyAuthenticationStateChangedAction(Refresh);
            this.logger = logger;
            this.localizer = localizer;
        }
        /// <summary>
        /// 刷新页面后会执行
        /// </summary>
        /// <returns></returns>
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            AuthenticationState authenticationState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            try
            {
                //取不到
                var user =await authenticationStateManager.GetCurrentUser();
                if (user == null)
                {
                    //尝试刷新
                    (user,_,_,_) =await authenticationStateManager.ReloadCurrentUserInfos();
                }
                //还是取不到
                if (user == null)
                {
                    await logger.ErrorAsync(localizer["User_Info_Get_Error_Retry_Login"]);
                    return authenticationState;
                }
                else 
                {
                    authenticationState = CreateAuthenticationState(user);
                    return authenticationState;
                }
            }
            catch (Exception ex)
            {
                await logger.ErrorAsync(localizer["User_Info_Get_Error_Retry_Login"], ex:ex);
                return authenticationState;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationState"></param>
        public void Refresh()
        {
            base.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        /// <summary>
        /// 根据 userdto 创建 AuthenticationState
        /// </summary>
        /// <returns></returns>
        private AuthenticationState CreateAuthenticationState(UserDto currentUser)
        {
            if (currentUser == null) return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, currentUser.NickName ?? currentUser.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, currentUser.Id.ToString()));
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "apiauth"));
            return new AuthenticationState(authenticatedUser);
        }
    }
}
