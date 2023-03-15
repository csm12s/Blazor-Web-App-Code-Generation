// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Base.Services;
using Microsoft.AspNetCore.Components;

namespace Gardener.Client.AntDesignUi.Base.Shared
{
    public partial class OtherPageLayout
    {
        /// <summary>
        /// 系统配置服务
        /// </summary>
        [Inject]
        private ISystemConfigService SystemConfigService { get; set; } = null!;
        /// <summary>
        /// js工具
        /// </summary>
        [Inject]
        private IJsTool JsTool { get; set; } = null!;
        /// <summary>
        /// 系统配置
        /// </summary>
        private SystemConfig systemConfig = null!;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        protected async override Task OnInitializedAsync()
        {
            systemConfig = SystemConfigService.GetSystemConfig();
            await JsTool.Document.SetTitle(systemConfig.SystemName);
            await base.OnInitializedAsync();
        }
    }
}
