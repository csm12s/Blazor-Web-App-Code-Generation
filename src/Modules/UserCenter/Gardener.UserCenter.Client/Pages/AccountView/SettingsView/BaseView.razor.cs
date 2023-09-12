using System.Threading.Tasks;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.UserCenter.Client.Pages.UserView;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Resources;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;

namespace Gardener.UserCenter.Client.Pages.AccountView.SettingsView
{
    public partial class BaseView : OperationDialogBase<int, bool, UserCenterResource>
    {
        private UserDto? _currentUser;

        [Inject]
        protected IAuthenticationStateManager AuthenticationStateManager { get; set; } = null!;
        [Inject]
        protected IAccountService AccountService { get; set; } = null!;
        [Inject]
        protected IClientNotifier Notifier { get; set; } = null!;

        private bool _saveBtnLoading = false;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _currentUser = await AuthenticationStateManager.GetCurrentUser();
        }

        /// <summary>
        /// 点击头像
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task OnAvatarClick(UserDto user)
        {
            int avatarDrawerWidth = 300;
            await OpenOperationDialogAsync<UserUploadAvatar, UserUploadAvatarParams, string>(
                SharedLocalResource.UplaodAvatar,
                new UserUploadAvatarParams(user, false),
                width: avatarDrawerWidth);
        }

        /// <summary>
        /// 点击保存用户基本信息
        /// </summary>
        /// <returns></returns>
        private async Task SaveUserBaseInfo()
        {
            if (_currentUser == null) return;

            _saveBtnLoading = true;

            bool result = await AccountService.UpdateCurrentUserBaseInfo(_currentUser);

            if (result)
            {
                Notifier.Success(Localizer.Combination(nameof(SharedLocalResource.Save), nameof(SharedLocalResource.Success)));
            }
            else
            {
                Notifier.Error(Localizer.Combination(nameof(SharedLocalResource.Save), nameof(SharedLocalResource.Error)));
            }
            _saveBtnLoading = false;
        }
    }
}