// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.ClayObject;
using Furion.DatabaseAccessor;
using Furion.DataValidation;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Furion.ViewEngine;
using Gardener.Email.Core;
using Gardener.Email.Domains;
using Gardener.Email.Dtos;
using Gardener.Email.Enums;
using Gardener.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Email.Services
{
    /// <summary>
    /// 邮件服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class EmailService : IEmailService, IScoped, IDynamicApiController
    {
        private readonly IRepository<EmailServerConfig> _emailServerRepository;
        private readonly IRepository<EmailTemplate> _emailTemplateRepository;
        private readonly IViewEngine _viewEngine;
        /// <summary>
        /// 邮件服务
        /// </summary>
        /// <param name="emailServerRepository"></param>
        /// <param name="emailTemplateRepository"></param>
        /// <param name="viewEngine"></param>
        public EmailService(IRepository<EmailServerConfig> emailServerRepository,
            IRepository<EmailTemplate> emailTemplateRepository, IViewEngine viewEngine)
        {
            _emailServerRepository = emailServerRepository;
            _emailTemplateRepository = emailTemplateRepository;
            _viewEngine = viewEngine;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<EmailServerConfig> GetEmailServerConfig()
        {
            return await GetEmailServerConfig(EmailServerTag.Base);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverTag"></param>
        /// <returns></returns>
        private async Task<EmailServerConfig> GetEmailServerConfig(EmailServerTag serverTag)
        {
            return await _emailServerRepository.AsQueryable(false)
                .Where(x => ("," + x.Tags + ",").Contains("," + serverTag + ",")
                && x.IsDeleted==false && x.IsLocked==false
                )
                .FirstOrDefaultAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emailServerConfig"></param>
        /// <param name="emailTemplate"></param>
        /// <param name="toEmail"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task SendEmail(EmailServerConfig emailServerConfig, EmailTemplate emailTemplate,string toEmail, object data)
        {
            string Subject = _viewEngine.RunCompileFromCached(emailTemplate.SubjectTemplate, data);
            string content = _viewEngine.RunCompileFromCached(emailTemplate.ContentTemplate, data);
            await EmailHelper.SendEmail(emailServerConfig.Host,
                emailServerConfig.AccountName,
                emailServerConfig.AccountPassword, 
                Subject, 
                content,
                emailServerConfig.FromEmail,
                toEmail,
                emailServerConfig.Port, 
                emailTemplate.IsHtml,
                emailServerConfig.EnableSsl,
                emailTemplate.FromName);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <remarks>发送邮件</remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<bool> Send(SendEmailInputDto input)
        {
            Guid templateId = input.TemplateId;
            EmailServerTag serverTag = input.ServerTag;
            object data = Clay.Object(input.Data);
            string toEmail = input.ToEmail;
            EmailServerConfig emailServerConfig;
            if (!Guid.Empty.Equals(input.EmailServerConfigId))
            {
                emailServerConfig =await _emailServerRepository.FindAsync(input.EmailServerConfigId);
            }
            else 
            {
                emailServerConfig = await GetEmailServerConfig(serverTag);
            }

            if (emailServerConfig == null) 
            {
                throw Oops.Bah(ExceptionCode.EMAIL_SERVER_NO_FIND);
            }

            EmailTemplate emailTemplate = await _emailTemplateRepository.FindAsync(templateId);
            await SendEmail(emailServerConfig, emailTemplate,toEmail, data);
            return true;
        }
    }
}
