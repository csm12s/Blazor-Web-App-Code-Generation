﻿// -----------------------------------------------------------------------------
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
    public class LoginTokenService : ClientServiceBase<LoginTokenDto, Guid>, ILoginTokenService
    {
        public LoginTokenService(IApiCaller apiCaller) : base(apiCaller, "login-token")
        {
        }

        public Task<bool> CheckLoginIdUsable(string clientId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Disable(Guid id, bool isDisabled = true)
        {
            return await apiCaller.PutAsync<object, bool>($"{controller}/{id}/disable/{isDisabled}");
        }
    }
}
