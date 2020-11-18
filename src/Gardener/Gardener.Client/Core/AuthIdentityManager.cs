// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Client.Services;
using Gardener.Core.Dtos;
using System.Threading.Tasks;

namespace Gardener.Client
{
    /// <summary>
    /// 身份管理
    /// </summary>
    public interface IAuthIdentityManager
    {
        Task FromLocalResetToken();
        UserDto GetCurrentUser();
        Task RemoveIdentity();
        Task SetToken(string token);
        void SetCurrentUser(UserDto currentUser);
    }
    /// <summary>
    /// 身份管理
    /// </summary>
    public class AuthIdentityManager : IAuthIdentityManager
    {
        private readonly JsTool jsTool;
        private readonly HttpClientManager httpClientManager;
        private readonly IAuthorizeService authorizeService;
        public AuthIdentityManager(JsTool jsTool, HttpClientManager httpClientManager, IAuthorizeService authorizeService)
        {
            this.jsTool = jsTool;
            this.httpClientManager = httpClientManager;
            this.authorizeService = authorizeService;
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
            this.currentUser= currentUser;
        }
        /// <summary>
        /// 检测本地token,并使用
        /// </summary>
        /// <param name="loginOutput"></param>
        /// <returns></returns>
        public async Task FromLocalResetToken()
        {
            //本地是否有token
            string token = await jsTool.SessionStorage.Get<string>("AccessToken");
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
            await jsTool.SessionStorage.Set("AccessToken", token);
            SetHttpClientAuthorization(token);
        }
        /// <summary>
        /// 标记注销
        /// </summary>
        public async Task RemoveIdentity()
        {
            await jsTool.SessionStorage.Remove("AccessToken");
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
