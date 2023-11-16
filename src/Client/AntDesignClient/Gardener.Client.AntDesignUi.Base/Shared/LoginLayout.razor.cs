// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base;
using Gardener.Client.Base.Services;
using Microsoft.AspNetCore.Components;

namespace Gardener.Client.AntDesignUi.Base.Shared
{
    public partial class LoginLayout
    {
        private string[] _locales=null!;

        private SystemConfig systemConfig = null!;
        /// <summary>
        /// 系统配置服务
        /// </summary>
        [Inject]
        private ISystemConfigService SystemConfigService { get; set; } = null!;
        [Inject]
        private IClientCultureService clientCultureService { get; set; } = null!;
        [Inject]
        public NavigationManager Navigation { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Task OnInitializedAsync()
        {
            _locales = clientCultureService.GetSupportedCultures();
            systemConfig = SystemConfigService.GetSystemConfig();
            return base.OnInitializedAsync();
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
