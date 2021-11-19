// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Email.Dtos;
using Gardener.Email.Services;
using System.Threading.Tasks;

namespace Gardener.Email.Client.Services
{
    [ScopedService]
    public class EmailService : IEmailService
    {
        private readonly string controller = "email";
        private readonly IApiCaller apiCaller;
        public EmailService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<bool> Send(SendEmailInputDto input)
        {
            return await apiCaller.PostAsync<SendEmailInputDto, bool>($"{controller}/send", input);
        }
    }
}
