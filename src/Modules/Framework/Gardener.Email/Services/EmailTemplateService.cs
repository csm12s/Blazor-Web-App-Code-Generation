// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Email.Domains;
using Gardener.Email.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gardener.Email.Services
{

    /// <summary>
    /// 邮件模板服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class EmailTemplateService : ServiceBase<EmailTemplate, EmailTemplateDto, Guid>, IEmailTemplateService
    {

        private readonly IEmailService _emailService;

        /// <summary>
        /// 邮件模板服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="emailService"></param>
        public EmailTemplateService(IRepository<EmailTemplate> repository, IEmailService emailService) : base(repository)
        {
            _emailService = emailService;
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <remarks>发送测试邮件</remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> SendTest(SendEmailInputDto input)
        {
            return await _emailService.Send(input);
        }
    }
}
