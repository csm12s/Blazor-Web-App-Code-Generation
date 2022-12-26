// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base.Resources;
using Gardener.Client.Base;
using Gardener.Email.Dtos;
using Gardener.Email.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Email.Client.Pages
{
    public partial class EmailTemplateTest : FeedbackComponent<OperationDialogInput<Guid>, OperationDialogOutput<Guid>>
    {
        private bool _isLoading = false;
        private SendEmailInputDto _sendEmailInput = new SendEmailInputDto();
        private List<EmailServerConfigDto> emailServerConfigs;
        [Inject]
        protected IClientLocalizer localizer { get; set; }
        [Inject]
        protected IEmailTemplateService emailTemplateService { get; set; }
        [Inject]
        protected IEmailService emailService { get; set; }
        [Inject]
        protected IEmailServerConfigService emailServerConfigService { get; set; }
        [Inject]
        protected MessageService messageService { get; set; }
        private string _emailData
        {
            get
            {
                return _sendEmailInput.Data == null ? "" : System.Text.Json.JsonSerializer.Serialize(_sendEmailInput.Data);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _sendEmailInput.Data = System.Text.Json.JsonSerializer.Deserialize<dynamic>(value);
                }
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        protected virtual async Task OnFormCancel()
        {
            await base.FeedbackRef.CloseAsync(OperationDialogOutput<Guid>.Cancel());
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _sendEmailInput.TemplateId = this.Options.Id;
            EmailTemplateDto templateDto = await emailTemplateService.Get(this.Options.Id);
            if (templateDto == null)
            {
                messageService.Error(localizer[SharedLocalResource.DataNotFound]);
            }
            else
            {
                if (templateDto.Example != null)
                {
                    _emailData = templateDto.Example;
                }
            }
            emailServerConfigs = await emailServerConfigService.GetAllUsable();
            if (emailServerConfigs != null && emailServerConfigs.Any())
            {
                _sendEmailInput.EmailServerConfigId = emailServerConfigs.First().Id;
            }
        }

        /// <summary>
        /// 表单完成时
        /// </summary>
        /// <param name="editContext"></param>
        /// <returns></returns>
        protected async Task OnFormFinish(EditContext editContext)
        {
            _isLoading = true;
            bool result=await emailService.Send(_sendEmailInput);
            if (result)
            {
                messageService.Success(localizer.Combination(SharedLocalResource.Send, SharedLocalResource.Success));
            }
            else 
            {
                messageService.Error(localizer.Combination(SharedLocalResource.Send, SharedLocalResource.Fail));
            }
            _isLoading = false;
        }
    }
}
