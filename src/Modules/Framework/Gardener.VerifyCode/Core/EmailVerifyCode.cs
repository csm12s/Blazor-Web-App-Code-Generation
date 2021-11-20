// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Common;
using Gardener.Email.Services;
using Gardener.VerifyCode.Core.Settings;
using Gardener.VerifyCode.Dtos;
using Gardener.VerifyCode.Enums;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Gardener.VerifyCode.Core
{
    /// <summary>
    /// 邮件验证码服务
    /// </summary>
    public class EmailVerifyCode : IVerifyCode
    {
        private readonly IVerifyCodeStoreService store;
        private readonly EmailVerifyCodeOptions settings;
        private readonly IEmailService emailService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="store"></param>
        /// <param name="settings"></param>
        /// <param name="emailService"></param>
        public EmailVerifyCode(IVerifyCodeStoreService store, IOptions<EmailVerifyCodeOptions> settings, IEmailService emailService)
        {
            this.store = store;
            this.settings = settings.Value;
            this.emailService = emailService;
        }
        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<VerifyCodeOutput> Create(VerifyCodeInput input)
        {
            EmailVerifyCodeInput emailInput = input as EmailVerifyCodeInput;
            return await Send(emailInput.ToEmail, emailInput.CreateCodeParam);

        }

        /// <summary>
        /// 移除验证码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> Remove(string key)
        {
            await this.store.Remove(VerifyCodeTypeEnum.Email, key);
            return true;
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private async Task<VerifyCodeOutput> Send(string toEmail, CharacterCodeCreateParam param = null)
        {
            param = param ?? new CharacterCodeCreateParam();
            if (!param.CharacterCount.HasValue)
            {
                param.CharacterCount = settings.CodeCharacterCount;
            }
            if (!param.Type.HasValue)
            {
                param.Type = settings.CodeType;
            }
            string code = CreateCode(param);
            string key = Guid.NewGuid().ToString();

            await emailService.Send(new Email.Dtos.SendEmailInputDto {
                Data = new {Code=code },
                ServerTag=settings.EmailServerTag,
                TemplateId= settings.EmailTemplateId,
                ToEmail=toEmail
            });
            await this.store.Add(VerifyCodeTypeEnum.Email, key, code, settings.CodeExpire);
            return new EmailVerifyCodeOutput { Key= key };
        }

        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="key"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<bool> Verify(string key, string code)
        {
            //code为空，直接返回错误
            if (string.IsNullOrEmpty(code)) return false;
            string dbCode = await this.store.GetCode(VerifyCodeTypeEnum.Email, key);
            bool success = string.Compare(dbCode, code, settings.IgnoreCase) == 0;
            if (success && settings.UseOnce)
            {
                await this.store.Remove(VerifyCodeTypeEnum.Email, key);
            }
            return success;
        }

        /// <summary>
        /// 创建一个验证码code
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string CreateCode(CharacterCodeCreateParam param)
        {
            if (param == null) return null;
            return RandomCodeCreator.Create(param.Type.Value, param.CharacterCount.Value);
        }
    }
}
