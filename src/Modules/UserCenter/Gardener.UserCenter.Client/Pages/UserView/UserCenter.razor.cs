// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.LocalizationLocalizer;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Resources;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.UserView
{
    public partial class UserCenter : IReuseTabsPage
    {
        /// <summary>
        /// 本地化
        /// </summary>
        [Inject]
        public ILocalizationLocalizer<UserCenterResource> localizer { get; set; } = null!;

        [Inject]
        IUserService userService { get; set; } = null!;

        UserDto? userDto { get; set; }

        public RenderFragment GetPageTitle()
        {
            return localizer["UserCenter"].ToRenderFragment();
        }

        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            var id = await userService.GetCurrentUserId();
            userDto = await userService.Get(int.Parse(id));

            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 保存用户信息
        /// </summary>
        /// <returns></returns>
        protected virtual async Task SaveUserInfo()
        {
            if (userDto != null)
            {
                await userService.Update(userDto);
            }
        }
    }
}

