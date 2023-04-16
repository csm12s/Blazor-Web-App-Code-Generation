using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.Client.Base.Services;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Gardener.WoChat.Dtos;
using Gardener.WoChat.Enums;
using Gardener.WoChat.Resources;
using Gardener.WoChat.Services;
using Microsoft.AspNetCore.Components;

namespace Gardener.WoChat.Client.Components
{
    /// <summary>
    /// wo chat 聊天
    /// </summary>
    public partial class WoChat : OperationDialogBase<Guid?, bool, WoChatResource>
    {
        /// <summary>
        /// Im服务
        /// </summary>
        [Inject]
        public IWoChatImService WoChatImService { get; set; } = null!;
        /// <summary>
        /// 用户服务
        /// </summary>
        [Inject]
        public IUserService UserService { get; set; } = null!;
        /// <summary>
        /// 消息提示服务
        /// </summary>
        [Inject]
        public IClientMessageService MessageService { get; set; } = null!;
        /// <summary>
        /// 身份权限服务
        /// </summary>
        [Inject]
        public IAuthenticationStateManager AuthenticationStateManager { get; set; } = null!;

        private readonly int height = 600;

        private IEnumerable<ImSessionDto>? imSessions;

        private IEnumerable<UserDto>? users;

        private UserDto? currentUser;
        /// <summary>
        /// 加载
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            var task1 = WoChatImService.GetMyImSessions();
            var task2 = AuthenticationStateManager.GetCurrentUser();
            imSessions = await task1;
            currentUser=await task2;

        }
        /// <summary>
        /// 点击tab
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private async Task OnTabClick(string key)
        {
            if ("2".Equals(key))
            {
                var usersTemp = await UserService.GetAllUsable();
                if (usersTemp != null && usersTemp.Any())
                {
                    //排除自己
                    users = usersTemp.Where(x => x.Id != currentUser?.Id);
                    userListSelectedUser = users.FirstOrDefault();
                }
            }
        }
        private UserDto? userListSelectedUser;
        /// <summary>
        /// 点击用户列表某一项
        /// </summary>
        /// <param name="user"></param>
        private void OnClickUserListItem(UserDto user)
        {
            userListSelectedUser = user;
        }

        /// <summary>
        /// 点击发送消息按钮
        /// </summary>
        /// <returns></returns>
        private async Task OnClickSendMessage()
        {
            if (userListSelectedUser == null)
            {
                MessageService.Warn(Localizer[WoChatResource.NoRowsAreSelected]);
                return;
            }

            //发起私聊
            var sessionId = await WoChatImService.AddMyImSession(new ImSessionAddInput
            {
                UserIds = new List<int> { userListSelectedUser.Id },
                SessionType = ImSessionType.Personal,
                SessionName = userListSelectedUser.NickName ?? userListSelectedUser.UserName,
                LastMessageTime = DateTimeOffset.Now
            });
            if (sessionId == null)
            {
                MessageService.Error(Localizer.Combination(WoChatResource.OpenSession, WoChatResource.Fail));
            }
        }
    }
}
