using Gardener.Core.Dtos;
using Gardener.Client.Apis;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using Microsoft.JSInterop;

namespace Gardener.Client.Core
{
    public class AuthProvider : AuthenticationStateProvider
    {
        //private RestClient restClient;
        private readonly HttpClient httpClient;
        private readonly IAuthorizeService authorizeService;
        private readonly IJSRuntime js;

        public UserDto CurrentUser { get; set; }

        public AuthProvider(HttpClient httpClient, IAuthorizeService authorizeService, IJSRuntime js)
        {
            this.httpClient = httpClient;
            this.authorizeService = authorizeService;
            this.js = js;
        }

        
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            Console.WriteLine("====================>GetAuthenticationStateAsync");
            AuthenticationState authenticationState = new AuthenticationState(new ClaimsPrincipal());
            try
            {

                if (CurrentUser == null)
                {
                    //本地是否有token
                    string token=await GetLocalToken();
                    if (!string.IsNullOrEmpty(token))
                    {
                        SetHttpClientAuthorization(token);
                    }
                    //请求
                    var userResult = await authorizeService.GetCurrentUser();
                    if (userResult.Successed)
                    {
                        CurrentUser = userResult.Data;
                        authenticationState = CreateAuthenticationState();
                        Console.WriteLine(" ====================44> " + System.Text.Json.JsonSerializer.Serialize(authenticationState.User.Identity));
                    }
                    else
                    {
                        Console.WriteLine(" ====================33> " + System.Text.Json.JsonSerializer.Serialize(authenticationState.User.Identity));
                        MarkUserAsLoggedOut();
                    }
                }
                Console.WriteLine(" ====================11> " + System.Text.Json.JsonSerializer.Serialize(authenticationState.User.Identity));
                return authenticationState;
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode.HasValue && ex.StatusCode.Value.Equals(HttpStatusCode.Unauthorized))
                {
                    //未登录
                    MarkUserAsLoggedOut();
                }
                Console.WriteLine(" ====================22> " + System.Text.Json.JsonSerializer.Serialize(authenticationState.User.Identity));
                return authenticationState;
            }
           
        }
        /// <summary>
        /// 标记授权
        /// </summary>
        /// <param name="loginOutput"></param>
        /// <returns></returns>
        public void MarkUserAsAuthenticated(string token)
        {
            SetLocalToken(token);
            SetHttpClientAuthorization(token);
            base.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        /// <summary>
        /// 标记注销
        /// </summary>
        public void MarkUserAsLoggedOut()
        {
            RemoveLocalToken();
            SetHttpClientAuthorization("");
            CurrentUser = null;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private AuthenticationState CreateAuthenticationState()
        {
            if (CurrentUser == null) return null;
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, CurrentUser.NickName ?? CurrentUser.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, CurrentUser.Id.ToString()));
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "apiauth"));
            return new AuthenticationState(authenticatedUser);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        private async void SetLocalToken(string token)
        {
           await js.InvokeVoidAsync("sessionStorage.setItem", "AccessToken", token);
        }
        /// <summary>
        /// 
        /// </summary>
        private async Task<string> GetLocalToken()
        {
           return await js.InvokeAsync<string>("sessionStorage.getItem", "AccessToken");
        }
        /// <summary>
        /// 
        /// </summary>
        private async void RemoveLocalToken()
        {
            await js.InvokeVoidAsync("sessionStorage.removeItem", "AccessToken");
        }
        /// <summary>
        /// 给httpclient设置验证token
        /// </summary>
        /// <param name="token"></param>
        private void SetHttpClientAuthorization(string token)
        {
            //httpClient.Authenticator = new JwtAuthenticator(token);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
    }
}
