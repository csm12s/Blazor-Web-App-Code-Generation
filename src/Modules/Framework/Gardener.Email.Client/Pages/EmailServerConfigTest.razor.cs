﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base.Resources;
using Gardener.Client.Base;
using Gardener.Client.Base.Services;
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
    public partial class EmailServerConfigTest : FeedbackComponent<OperationDialogInput<Guid>, OperationDialogOutput<Guid>>
    {
        private bool _isLoading = false;
        private SendEmailInputDto _sendEmailInput = new SendEmailInputDto();
        private List<EmailTemplateDto>? emailTemplates;
        [Inject]
        protected IClientLocalizer Localizer { get; set; } = null!;
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
            get {
                return _sendEmailInput.Data==null?"":System.Text.Json.JsonSerializer.Serialize(_sendEmailInput.Data);
            }
            set {
                if (!string.IsNullOrEmpty(value)) 
                {
                    _sendEmailInput.Data = System.Text.Json.JsonSerializer.Deserialize<dynamic>(value)??string.Empty;
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
            _sendEmailInput.EmailServerConfigId = this.Options.Data;
            emailTemplates = await EmailTemplateService.GetAllUsable();
            if (emailTemplates != null && emailTemplates.Any())
            {
                EmailTemplateDto templateDto = emailTemplates.First();

                _sendEmailInput.TemplateId = templateDto.Id;

                if (templateDto.Example != null)
                {
                    _emailData = templateDto.Example;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateDto"></param>
        /// <returns></returns>
        private void OnEmailTemplateChanged(EmailTemplateDto templateDto)
        {
            if (templateDto.Example != null)
            {
                _sendEmailInput.Data = templateDto.Example;
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
                MessageService.Success(Localizer.Combination(SharedLocalResource.Send, SharedLocalResource.Success));
            }
            else 
            {
                MessageService.Error(Localizer.Combination(SharedLocalResource.Send, SharedLocalResource.Fail));
            }
            _isLoading = false;
        }
    }
}
