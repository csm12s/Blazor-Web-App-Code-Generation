// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.UserCenter.Dtos;
using System;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.ClientView
{
    public partial class ClientEdit : EditOperationDialogBase<ClientDto, Guid>
    {

        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            if (this.Options.Type.Equals(DrawerInputType.Add))
            { 
                 _editModel.Id = Guid.NewGuid();
                 _editModel.SecretKey = Guid.NewGuid().ToString();
            }
        }
    }
}
