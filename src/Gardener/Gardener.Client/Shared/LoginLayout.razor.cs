using AntDesign.Pro.Layout;
using Gardener.Client.Models;
using Gardener.Client.Services;
using Microsoft.AspNetCore.Components;

namespace Gardener.Client.Shared
{
    public partial class LoginLayout
    {
        private readonly LinkItem[] _links =
        {
                new LinkItem{ Key = "", BlankTarget = true, Title = "Fur" ,Href="https://gitee.com/monksoul/Fur"},
                new LinkItem{ Key = "", BlankTarget = true, Title = "Ant Design",Href="https://github.com/ant-design-blazor/ant-design-pro-blazor"},
        };
        /// <summary>
        /// 系统配置服务
        /// </summary>
        [Inject]
        private ISystemConfigService SystemConfigService { get; set; }
      
        private SystemConfig systemConfig;

        protected override void OnInitialized()
        {
            systemConfig = SystemConfigService.GetSystemConfig();
        }
    }
}
