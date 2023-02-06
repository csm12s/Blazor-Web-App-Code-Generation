// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Base.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Gardener.Client.Core.Shared
{
    public partial class OtherPageLayout
    {
        /// <summary>
        /// 系统配置服务
        /// </summary>
        [Inject]
        private ISystemConfigService SystemConfigService { get; set; }
        [Inject]
        private IJsTool JsTool { get; set; }

        private SystemConfig systemConfig;

        protected async override Task OnInitializedAsync()
        {
            systemConfig = SystemConfigService.GetSystemConfig();
            await JsTool.Document.SetTitle(systemConfig.SystemName);
            await base.OnInitializedAsync();
        }
    }
}
