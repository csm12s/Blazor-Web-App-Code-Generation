// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Base.Components;
using Gardener.Email.Dtos;
using System;
using System.Threading.Tasks;

namespace Gardener.Email.Client.Pages
{
    public partial class EmailTemplate : ListTableBase<EmailTemplateDto, Guid, EmailTemplateEdit>
    {
        protected override void SetOperationDialogSettings(OperationDialogSettings dialogSettings)
        {
            dialogSettings.Width = 600;
        }
        /// <summary>
        /// 点击发送按钮
        /// </summary>
        /// <param name="roleDto"></param>
        protected async Task OnClickSend(Guid id)
        {
            OperationDialogInput<Guid> input = OperationDialogInput<Guid>.IsSelect(id);
            await OpenOperationDialogAsync<EmailTemplateTest, OperationDialogInput<Guid>, OperationDialogOutput<Guid>>(localizer["发送"], input);
        }
    }
}
