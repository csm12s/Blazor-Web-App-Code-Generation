// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Client.Services;
using Gardener.Core.Dtos;
using System;
using System.Threading.Tasks;

namespace Gardener.Client
{
    /// <summary>
    /// 验证状态管理
    /// </summary>
    public interface IAuthenticationStateManager
    {
        Task FromLocalResetToken();
        UserDto GetCurrentUser();
        Task Login(string token);
        Task Logout();
        Task RemoveIdentity();
        void SetCurrentUser(UserDto currentUser);
        void SetNotifyAuthenticationStateChangedAction(Action c);
        Task SetToken(string token);
    }
    public class AuthenticationStateManager : IAuthenticationStateManager
    {
        private readonly JsTool jsTool;
        private readonly HttpClientManager httpClientManager;

        public AuthenticationStateManager(JsTool jsTool, HttpClientManager httpClientManager)
        {
            this.jsTool = jsTool;
            this.httpClientManager = httpClientManager;
        }
        /// <summary>
        /// 标记授权
        /// </summary>
        /// <param name="loginOutput"></param>
        /// <returns></returns>
        public async Task Login(string token)
        {
            await SetToken(token);
            notifyAuthenticationStateChangedAction();
        }
        /// <summary>
        /// 标记注销
        /// </summary>
        public async Task Logout()
        {
            await RemoveIdentity();
            notifyAuthenticationStateChangedAction();
        }

        Action notifyAuthenticationStateChangedAction = () => { };

        public void SetNotifyAuthenticationStateChangedAction(Action c)
        {
            notifyAuthenticationStateChangedAction = c;
        }
        private UserDto currentUser;
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public UserDto GetCurrentUser()
        {
            return currentUser;
        }
        public void SetCurrentUser(UserDto currentUser)
        {
            this.currentUser = currentUser;
        }
        /// <summary>
        /// 检测本地token,并使用
        /// </summary>
        /// <param name="loginOutput"></param>
        /// <returns></returns>
        public async Task FromLocalResetToken()
        {
            //本地是否有token
            string token = await jsTool.SessionStorage.GetAsync<string>("AccessToken");
            if (!string.IsNullOrEmpty(token))
            {
                SetHttpClientAuthorization(token);
            }
        }

        /// <summary>
        /// 标记授权
        /// </summary>
        /// <param name="loginOutput"></param>
        /// <returns></returns>
        public async Task SetToken(string token)
        {
            await jsTool.SessionStorage.SetAsync("AccessToken", token);
            SetHttpClientAuthorization(token);
        }
        /// <summary>
        /// 标记注销
        /// </summary>
        public async Task RemoveIdentity()
        {
            await jsTool.SessionStorage.RemoveAsync("AccessToken");
            SetHttpClientAuthorization("");
            currentUser = null;
        }
        #region private
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

    }
}
