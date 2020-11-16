using Gardener.Client.Apis;
using Microsoft.AspNetCore.Components;

namespace Gardener.Client.Shared
{
    public partial class LoginLayout
    {

        /// <summary>
        /// 系统配置服务
        /// </summary>
        [Inject]
        private ISystemConfigService SystemConfigService { get; set; }
        /// <summary>
        /// 底部内容
        /// </summary>
        private string footerContent;

        protected override void OnInitialized()
        {
            footerContent = SystemConfigService.GetFooterContent();
        }
    }
}
