// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign.ProLayout;
using Gardener.Client.Core;
using Gardener.Client.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Gardener.Client.Shared
{
    public partial class LoginLayout
    {
        private readonly LinkItem[] _links =
        {
                new LinkItem{ Key = "", BlankTarget = true, Title = "Furion" ,Href="https://gitee.com/monksoul/Furion"},
                new LinkItem{ Key = "", BlankTarget = true, Title = "Ant Design",Href="https://github.com/ant-design-blazor/ant-design-pro-blazor"},
        };
        /// <summary>
        /// 系统配置服务
        /// </summary>
        [Inject]
        private ISystemConfigService SystemConfigService { get; set; }
        [Inject]
        private JsTool JsTool { get; set; }
        private SystemConfig systemConfig;

        protected async override Task OnInitializedAsync()
        {
            systemConfig = SystemConfigService.GetSystemConfig();
            await JsTool.Document.SetTitle(systemConfig.SystemName);
        }
    }
}
