// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Microsoft.AspNetCore.Components;

namespace Gardener.Client.AntDesignUi.Entry
{
    public partial class App
    {
        [Inject]
        private ClientModuleContext moduleContext { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }

        private void GoHome() 
        {
            Navigation.NavigateTo("/");
        }
    }
}
