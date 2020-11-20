﻿using Gardener.Client.Services;
using Gardener.Core.Dtos;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gardener.Client
{
    /// <summary>
    /// 自定义验证状态提供器
    /// </summary>
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthIdentityManager authManager;
        private readonly IAuthorizeService authorizeService;
        public CustomAuthenticationStateProvider(IAuthIdentityManager authManager, IAuthorizeService authorizeService)
        {
            this.authManager = authManager;
            this.authorizeService = authorizeService;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            AuthenticationState authenticationState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            try
            {
                if (authManager.GetCurrentUser() == null)
                {
                    //从本地刷新新token
                    await authManager.FromLocalResetToken();
                    //请求
                    var userResult = await authorizeService.GetCurrentUser();
                    if (userResult.Successed)
                    {
                        authManager.SetCurrentUser(userResult.Data);
                        authenticationState = CreateAuthenticationState(userResult.Data);
                    }
                }
                else
                {
                    authenticationState = CreateAuthenticationState(authManager.GetCurrentUser());
                }
                return authenticationState;
            }
            catch (HttpRequestException ex)
            {
                return authenticationState;
            }
            catch (Exception ex)
            {
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
            if (currentUser == null) return null;
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, currentUser.NickName ?? currentUser.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, currentUser.Id.ToString()));
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "apiauth"));
            return new AuthenticationState(authenticatedUser);
        }
    }
}
