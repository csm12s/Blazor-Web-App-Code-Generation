using AntDesign;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.Client.Base.Components;
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

        /// <summary>
        /// 用户选择绑定值
        /// </summary>
        private ClientListBindValue<int, bool> openGroupSelectedUsers = new ClientListBindValue<int, bool>(false);

        private readonly int height = 600;

        private IEnumerable<ImSessionDto>? imSessions;

        private IEnumerable<UserDto>? users;

        private UserDto? currentUser;
        private static readonly string tabUsersKey = "users";
        private static readonly string tabMessageKey = "message";
        /// <summary>
        /// tab控制
        /// message、users
        /// </summary>
        private string tabActiveKey = tabMessageKey;
        /// <summary>
        /// 用户列表加载中
        /// </summary>
        private bool userListLoading = false;
        /// <summary>
        /// 会话列表加载中
        /// </summary>
        private bool sessionListLoading = false;
        /// <summary>
        /// 当前选择的会话编号
        /// </summary>
        private Guid? sessionListSelectedSessionId;
        /// <summary>
        /// 当前选择的会话
        /// </summary>
        private ImSessionDto? sessionListSelectedSession;
        /// <summary>
        /// 开启群聊模式
        /// </summary>
        private bool openGroupMode = false;
        /// <summary>
        /// 群聊名称
        /// </summary>
        private string? openGroupName=null;
        /// <summary>
        /// 加载
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            //传入的会话编号
            sessionListSelectedSessionId = this.Options;
            //当前用户
            currentUser = await AuthenticationStateManager.GetCurrentUser();
            //默认到消息列表
            await OnChange(tabMessageKey);
        }
        /// <summary>
        /// 点击tab
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private async Task OnChange(string key)
        {
            tabActiveKey = key;
            if (tabUsersKey.Equals(key))
            {
                userListLoading = true;
                var usersTemp = await UserService.GetAllUsable();
                if (usersTemp != null && usersTemp.Any())
                {
                    //排除自己
                    users = usersTemp.Where(x => x.Id != currentUser?.Id);
                    userListSelectedUser = users.FirstOrDefault();
                }
                userListLoading = false;
            }
            else if (tabMessageKey.Equals(key))
            {
                sessionListLoading = true;
                imSessions = await WoChatImService.GetMyImSessions();
                if (imSessions != null && imSessions.Any())
                {
                    if (sessionListSelectedSessionId != null)
                    {
                        sessionListSelectedSession=imSessions.Where(x=>x.Id.Equals(sessionListSelectedSessionId)).FirstOrDefault();
                    }
                    else
                    {
                        //默认第一个
                        sessionListSelectedSession = imSessions.FirstOrDefault();
                        sessionListSelectedSessionId = sessionListSelectedSession?.Id;
                    }
                }
                sessionListLoading = false;
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
            else
            {
                //选中
                sessionListSelectedSessionId = sessionId;
                tabActiveKey = tabMessageKey;
            }
        }


        /// <summary>
        /// 点击用户会话列表某一项
        /// </summary>
        /// <param name="user"></param>
        private void OnClickSessionListItem(ImSessionDto session)
        {
            sessionListSelectedSessionId = session.Id;
            sessionListSelectedSession = session;
        }
        /// <summary>
        /// 开启会话模式
        /// </summary>
        private void OnClickOpenGroupMode()
        { 
            openGroupMode = true;
        }
        /// <summary>
        /// 开启会话模式确认
        /// </summary>
        private async Task OnClickOpenGroupModeOk()
        {
           IEnumerable<int> userIds= openGroupSelectedUsers.SelectKeys(true);
            if (!userIds.Any()) 
            {
                MessageService.Warn(Localizer[WoChatResource.NoRowsAreSelected]);
                return;
            }
            //发起群聊
            var sessionId = await WoChatImService.AddMyImSession(new ImSessionAddInput
            {
                UserIds = userIds,
                SessionType = ImSessionType.Group,
                SessionName = openGroupName,
                LastMessageTime = DateTimeOffset.Now
            });
            if (sessionId == null)
            {
                MessageService.Error(Localizer.Combination(WoChatResource.OpenSession, WoChatResource.Fail));
            }
            else
            {
                //选中
                sessionListSelectedSessionId = sessionId;
                tabActiveKey = tabMessageKey;
            }
            
        }
    }
}
