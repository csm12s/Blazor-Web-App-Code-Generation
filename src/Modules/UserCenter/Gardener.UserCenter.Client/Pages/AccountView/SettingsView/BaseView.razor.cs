using System.Threading.Tasks;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.UserCenter.Client.Pages.UserView;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Resources;
using Microsoft.AspNetCore.Components;

namespace Gardener.UserCenter.Client.Pages.AccountView.SettingsView
{
    public partial class BaseView : OperationDialogBase<int, bool, UserCenterResource>
    {
        private UserDto? _currentUser;

        [Inject] 
        protected IAuthenticationStateManager AuthenticationStateManager { get; set; } = null!;

        private void HandleFinish()
        {
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _currentUser = await AuthenticationStateManager.GetCurrentUser();
        }

        /// <summary>
        /// µã»÷Í·Ïñ
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task OnAvatarClick(UserDto user)
        {
            int avatarDrawerWidth = 300;
            await OpenOperationDialogAsync<UserUploadAvatar, UserUploadAvatarParams, string>(
                Localizer[SharedLocalResource.UplaodAvatar],
                new UserUploadAvatarParams(user, false),
                width: avatarDrawerWidth);
        }
    }
}