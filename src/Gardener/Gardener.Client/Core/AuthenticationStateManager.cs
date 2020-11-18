// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace Gardener.Client
{
    /// <summary>
    /// 验证状态管理
    /// </summary>
    public interface IAuthenticationStateManager
    {
        IAuthIdentityManager GetAuthManagerService();
        Task Login(string token);
        Task Logout();
    }
    public class AuthenticationStateManager : IAuthenticationStateManager
    {
        private readonly IAuthIdentityManager authManager;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthenticationStateManager(AuthenticationStateProvider authenticationStateProvider, IAuthIdentityManager authManager)
        {
            this.authenticationStateProvider = authenticationStateProvider;
            this.authManager = authManager;
        }
        /// <summary>
        /// 标记授权
        /// </summary>
        /// <param name="loginOutput"></param>
        /// <returns></returns>
        public async Task Login(string token)
        {
            await authManager.SetToken(token);
            ((CustomAuthenticationStateProvider)authenticationStateProvider).Refresh();
        }
        /// <summary>
        /// 标记注销
        /// </summary>
        public async Task Logout()
        {
            await authManager.RemoveIdentity();
            ((CustomAuthenticationStateProvider)authenticationStateProvider).Refresh();
        }

        /// <summary>
        /// 获取 身份管理
        /// </summary>
        /// <returns></returns>
        public IAuthIdentityManager GetAuthManagerService()
        {
            return authManager;
        }

    }
}
