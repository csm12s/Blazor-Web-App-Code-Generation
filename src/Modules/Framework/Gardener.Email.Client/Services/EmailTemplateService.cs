// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Email.Dtos;
using Gardener.Email.Services;
using System;
using System.Threading.Tasks;

namespace Gardener.Email.Client.Services
{
    [ScopedService]
    public class EmailTemplateService : ClientServiceBase<EmailTemplateDto,Guid>, IEmailTemplateService
    {
        public EmailTemplateService(IApiCaller apiCaller) : base(apiCaller, "email-template")
        {
        }

        public async Task<bool> SendTest(SendEmailInputDto input)
        {
            return await apiCaller.PostAsync<SendEmailInputDto, bool>($"{controller}/send-test", input);
        }
    }
}
