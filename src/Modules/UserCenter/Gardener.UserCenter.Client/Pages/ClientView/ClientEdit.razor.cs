// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base;
using Gardener.UserCenter.Dtos;
using System;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.ClientView
{
    public partial class ClientEdit : EditDrawerBase<ClientDto, Guid>
    {

        private FormValidationRule[] emailRules = new FormValidationRule[]{
            new FormValidationRule { Type = FormFieldType.Email, Required = true,Message="请输入正确的邮箱地址" },
        };

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
            }
        }
    }
}
