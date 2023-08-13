// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.EasyJob.Resources;
using Gardener.LocalizationLocalizer;
using Microsoft.AspNetCore.Components;

namespace Gardener.EasyJob.Client.Pages.JobView
{
    public partial class JobLogDashboard : ReuseTabsPageBase
    {
        /// <summary>
        /// 本地化
        /// </summary>
        [Inject]
        protected ILocalizationLocalizer<EasyJobLocalResource> Localizer { get; set; } = null!;
    }
}
