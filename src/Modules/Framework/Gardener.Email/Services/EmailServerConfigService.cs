// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Email.Domains;
using Gardener.Email.Dtos;
using Gardener.EntityFramwork;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Gardener.Email.Services
{

    /// <summary>
    /// 邮件服务器配置服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class EmailServerConfigService : ServiceBase<EmailServerConfig, EmailServerConfigDto,Guid>, IEmailServerConfigService
    {
        /// <summary>
        /// 邮件服务器配置服务
        /// </summary>
        /// <param name="repository"></param>
        public EmailServerConfigService(IRepository<EmailServerConfig> repository) : base(repository)
        {
        }
    }
}
