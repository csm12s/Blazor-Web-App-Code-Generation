using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Gardener.WoChat.Dtos;
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

        private readonly int height = 600;


        private IEnumerable<ImSessionDto>? imSessions;

        private IEnumerable<UserDto>? users;
        /// <summary>
        /// 加载
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            var task1 = WoChatImService.GetMyImSessions();
            imSessions = await task1;
        }
        /// <summary>
        /// 点击tab
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private async Task OnTabClick(string key)
        { 
            if("2".Equals(key))
            {
                users = await UserService.GetAllUsable();
                if(users!=null &&users.Any())
                {
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
    }
}
