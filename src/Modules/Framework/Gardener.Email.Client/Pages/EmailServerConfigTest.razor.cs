// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
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
    public partial class EmailServerConfigTest : FeedbackComponent<DrawerInput<Guid>, DrawerOutput<Guid>>
    {
        private bool _isLoading = false;
        private SendEmailInputDto _sendEmailInput = new SendEmailInputDto();
        private List<EmailTemplateDto> emailTemplates;
        [Inject]
        protected IClientLocalizer localizer { get; set; }
        [Inject]
        protected IEmailTemplateService emailTemplateService { get; set; }
        [Inject]
        protected IEmailServerConfigService emailServerConfigService { get; set; }
        [Inject]
        protected MessageService messageService { get; set; }
        /// <summary>
        /// 取消
        /// </summary>
        protected virtual async Task OnFormCancel()
        {
            await base.FeedbackRef.CloseAsync(DrawerOutput<Guid>.Cancel());
        }

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _sendEmailInput.EmailServerConfigId = this.Options.Id;
            emailTemplates = await emailTemplateService.GetAllUsable();
            if (emailTemplates != null && emailTemplates.Any())
            {
                EmailTemplateDto templateDto = emailTemplates.First();

                _sendEmailInput.TemplateId = templateDto.Id;

                if (templateDto.Example != null)
                {
                    _sendEmailInput.Data = templateDto.Example;
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
            bool result=await emailTemplateService.SendTest(_sendEmailInput);
            if (result)
            {
                messageService.Success(localizer.Combination("发送", "成功"));
            }
            else 
            {
                messageService.Error(localizer.Combination("发送", "失败"));
            }
            _isLoading = false;
        }
    }
}
