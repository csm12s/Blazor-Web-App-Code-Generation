// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.Email.Dtos;
using Gardener.Email.Resources;
using System;
using System.Threading.Tasks;

namespace Gardener.Email.Client.Pages
{
    public partial class EmailServerConfig : ListOperateTableBase<EmailServerConfigDto, Guid, EmailServerConfigEdit, EmailLocalResource>
    {
        /// <summary>
        /// 点击发送按钮
        /// </summary>
        /// <param name="roleDto"></param>
        protected async Task OnClickSend(Guid id)
        {
            OperationDialogSettings drawerSettings = GetOperationDialogSettings();
            OperationDialogInput<Guid> input = OperationDialogInput<Guid>.Select(id);
            await OpenOperationDialogAsync<EmailServerConfigTest, OperationDialogInput<Guid>, OperationDialogOutput<Guid>>(SharedLocalResource.Send, input);
        }
    }
}
