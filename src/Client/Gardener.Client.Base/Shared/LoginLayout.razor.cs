// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Gardener.Client.Base.Shared
{
    public partial class LoginLayout
    {
        /// <summary>
        /// 系统配置服务
        /// </summary>
        [Inject]
        private ISystemConfigService SystemConfigService { get; set; }
        [Inject]
        private IJsTool JsTool { get; set; }

        private SystemConfig systemConfig;

        private ReuseTabs reuseTabs;

        protected async override Task OnInitializedAsync()
        {
            systemConfig = SystemConfigService.GetSystemConfig();
            await JsTool.Document.SetTitle(systemConfig.SystemName);
            //登录模板导航控制
            ClientNavTabControl.SetReuseTabs(reuseTabs);
        }
    }
}
