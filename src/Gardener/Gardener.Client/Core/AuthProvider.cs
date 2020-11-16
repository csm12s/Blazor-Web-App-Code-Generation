using Gardener.Application.Dtos;
using Gardener.Client.Apis;
using Microsoft.AspNetCore.Components.Authorization;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gardener.Client.Core
{
    public class AuthProvider : AuthenticationStateProvider
    {
        private RestClient restClient;
        private readonly IAuthorizeService authorizeService;
        public UserDto CurrentUser { get; set; }

        public AuthProvider(RestClient restClient, IAuthorizeService authorizeService)
        {
            this.restClient = restClient;
            this.authorizeService = authorizeService;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userResult = await authorizeService.GetCurrentUser();
            if (userResult.Successed)
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, userResult.Data.NickName ?? userResult.Data.UserName));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, userResult.Data.Id.ToString()));
                var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "apiauth"));
                return new AuthenticationState(authenticatedUser);
            }
            else 
            {
                MarkUserAsLoggedOut();
                return new AuthenticationState(new ClaimsPrincipal());
            }
        }
        /// <summary>
        /// 标记授权
        /// </summary>
        /// <param name="loginOutput"></param>
        /// <returns></returns>
        public void MarkUserAsAuthenticated(LoginOutput loginOutput)
        {
            restClient.Authenticator = new JwtAuthenticator(loginOutput.AccessToken);
            var authState =  GetAuthenticationStateAsync();
            NotifyAuthenticationStateChanged(authState);
        }

        /// <summary>
        /// 标记注销
        /// </summary>
        public void MarkUserAsLoggedOut()
        {
            restClient.Authenticator = null;
            CurrentUser = null;
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
