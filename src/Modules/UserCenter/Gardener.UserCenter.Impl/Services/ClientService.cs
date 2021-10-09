// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Impl.Domains;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Gardener.UserCenter.Impl.Services
{
    /// <summary>
    /// 客户端服务
    /// </summary>
    [ApiDescriptionSettings("UserCenterServices")]
    public class ClientService : ServiceBase<Client, ClientDto, Guid>, IClientService
    {
        /// <summary>
        /// 客户端服务
        /// </summary>
        public ClientService(IRepository<Client> repository) : base(repository)
        {
        }
    }
}
