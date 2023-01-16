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
        private string[] _locales;

        private SystemConfig systemConfig;
        /// <summary>
        /// 系统配置服务
        /// </summary>
        [Inject]
        private ISystemConfigService SystemConfigService { get; set; }
        [Inject]
        private IJsTool JsTool { get; set; }
        [Inject]
        private IClientCultureService clientCultureService { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }

        protected async override Task OnInitializedAsync()
        {
            _locales = clientCultureService.GetSupportedCultures();
            systemConfig = SystemConfigService.GetSystemConfig();
            await JsTool.Document.SetTitle(systemConfig.SystemName);
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// HandleSelectLang
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task HandleSelectLang(MenuItem item)
        {
            string name = item.Key;
            if (await clientCultureService.SetCulture(name))
            {
                Navigation.NavigateTo(Navigation.Uri, forceLoad: true);
            }
        }
    }
}
