// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.Client.Base.Services;
using Gardener.Email.Dtos;
using Gardener.Email.Resources;
using Gardener.Email.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Email.Client.Pages
{
    public partial class EmailTemplateTest : OperationDialogBase<OperationDialogInput<Guid>, OperationDialogOutput<Guid>, EmailLocalResource>
    {
        private bool _isLoading = false;
        private SendEmailInputDto _sendEmailInput = new SendEmailInputDto();
        private List<EmailServerConfigDto>? emailServerConfigs;
        [Inject]
        protected IEmailTemplateService EmailTemplateService { get; set; } = null!;
        [Inject]
        protected IEmailService EmailService { get; set; } = null!;
        [Inject]
        protected IEmailServerConfigService EmailServerConfigService { get; set; } = null!;
        [Inject]
        protected IClientMessageService MessageService { get; set; } = null!;
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
                    _sendEmailInput.Data = System.Text.Json.JsonSerializer.Deserialize<dynamic>(value) ?? string.Empty;
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
            _sendEmailInput.TemplateId = this.Options.Data;
            EmailTemplateDto templateDto = await EmailTemplateService.Get(this.Options.Data);
            if (templateDto.Example != null)
            {
                _emailData = templateDto.Example;
            }
            emailServerConfigs = await EmailServerConfigService.GetAllUsable();
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
            bool result=await EmailService.Send(_sendEmailInput);
            if (result)
            {
                MessageService.Success(Localizer.Combination(nameof(SharedLocalResource.Send), nameof(SharedLocalResource.Success)));
            }
            else 
            {
                MessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Send), nameof(SharedLocalResource.Fail)));
            }
            _isLoading = false;
        }
    }
}
