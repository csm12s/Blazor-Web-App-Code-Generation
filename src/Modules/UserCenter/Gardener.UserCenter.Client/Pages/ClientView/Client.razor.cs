// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.UserCenter.Dtos;
using System;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.ClientView
{
    public partial class Client : ListOperateTableBase<ClientDto, Guid, ClientEdit>
    {
        /// <summary>
        /// 点击展示关联接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task OnShowFunctionClick(ClientDto model)
        {
            await OpenOperationDialogAsync<ClientFunctionEdit, ClientFunctionEditOption, bool>(
            $"{localizer["BindingApi"]}-[{model.Name}]",
            new ClientFunctionEditOption
            {
                Id = model.Id,
                Type = 0,
                Name = model.Name
            },
            width: 1200
            );
        }
    }
}
