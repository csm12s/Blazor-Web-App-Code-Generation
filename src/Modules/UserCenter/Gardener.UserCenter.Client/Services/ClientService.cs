// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using System;

namespace Gardener.UserCenter.Client.Services
{
    [ScopedService]
    public class ClientService : ClientServiceBase<ClientDto, Guid>, IClientService
    {
        private static readonly string controller = "client";
        public ClientService(IApiCaller apiCaller) : base(apiCaller, controller)
        {
        }
    }
}
