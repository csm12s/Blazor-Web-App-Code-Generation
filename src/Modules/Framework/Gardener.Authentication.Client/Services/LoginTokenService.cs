// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Dtos;
using Gardener.Authentication.Services;
using Gardener.Client.Base;
using System;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    /// <summary>
    /// 用户Token服务
    /// </summary>
    [ScopedService]
    public class LoginTokenService : ApplicationServiceBase<LoginTokenDto, Guid>, ILoginTokenService
    {
        private static readonly string controller = "login-token";
        private readonly IApiCaller apiCaller;
        public LoginTokenService(IApiCaller apiCaller) : base(apiCaller, controller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<bool> Disable(Guid id, bool isDisabled = true)
        {
            return await apiCaller.PutAsync<object, bool>($"{controller}/{id}/disable/{isDisabled}");
        }
    }
}
