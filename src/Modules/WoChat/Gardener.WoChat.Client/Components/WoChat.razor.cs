using AntDesign;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.Client.Base.Components;
using Gardener.Client.Base.Services;
using Gardener.EventBus;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Gardener.WoChat.Dtos;
using Gardener.WoChat.Dtos.Notification;
using Gardener.WoChat.Enums;
using Gardener.WoChat.Resources;
using Gardener.WoChat.Services;
using Microsoft.AspNetCore.Components;
using System.Linq;

namespace Gardener.WoChat.Client.Components
{
    /// <summary>
    /// wo chat 聊天
    /// </summary>
    public partial class WoChat : OperationDialogBase<WoChatDialogConfig, bool, WoChatResource>
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
        /// 时间总线
        /// </summary>
        [Inject]
        private IEventBus EventBus { get; set; } = null!;
        [Inject]
        private IJsTool JsTool { get; set; } = null!;
        /// <summary>
        /// 用户选择绑定值
        /// </summary>
        private ClientListBindValue<int, bool> openGroupSelectedUsers = new ClientListBindValue<int, bool>(false);
        /// <summary>
        /// 界面高度
        /// </summary>
        [Parameter]
        public int Height { get; set; } = 600;
        /// <summary>
        /// 消息标题区高度
        /// </summary>
        [Parameter]
        public int MessageTitleHeight { get; set; } = 50;
        /// <summary>
        /// 消息输入区高度
        /// </summary>
        [Parameter]
        public int MessageInputHeight { get; set; } = 200;
        /// <summary>
        /// 默认选择的会话编号
        /// </summary>
        [Parameter]
        public Guid? DefaultSelectedSessionId { get; set; }
        /// <summary>
        /// 消息列表区高度
        /// </summary>
        private int messageListHeight = 350;
        /// <summary>
        /// 头像显示形状
        /// </summary>
        private readonly string avatarShape = AvatarShape.Square;
        /// <summary>
        /// 每页消息数
        /// </summary>
        private readonly int messagePageSize = 100;
        /// <summary>
        /// 会话列表
        /// </summary>
        private IEnumerable<ImSessionDto>? imSessions;
        /// <summary>
        /// 用户列表
        /// </summary>
        private IEnumerable<UserDto>? users;
        /// <summary>
        /// 消息列表
        /// </summary>
        private List<ImSessionMessageDto>? messages;
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
        private string? openGroupName = null;
        /// <summary>
        /// 输入消息内容
        /// </summary>
        private string? inputMessage = null;
        /// <summary>
        /// 设置抽屉信息
        /// </summary>
        private bool settingDrawerVisible = false;
        /// <summary>
        /// 订阅者
        /// </summary>
        private Subscriber? messageNotificationSubscriber;
        /// <summary>
        /// 用户列表选中的用户
        /// </summary>
        private UserDto? userListSelectedUser;
        /// <summary>
        /// 消息显示容器编号-用于js控制
        /// </summary>
        private string woChatMessageBoxId = "wo-chat-message-box";

        /// <summary>
        /// 参数设置后
        /// </summary>
        /// <returns></returns>
        protected override Task OnParametersSetAsync()
        {
            woChatMessageBoxId = "wo-chat-message-box-" + Guid.NewGuid();

            
            if (this.Options != null)
            {
                //弹框打开
                sessionListSelectedSessionId = this.Options.DefaultSelectedSessionId;
                this.Height = this.Options.Height;
                this.MessageInputHeight = this.Options.MessageInputHeight;
                this.MessageTitleHeight = this.Options.MessageTitleHeight;
            }
            else 
            {
                //页面或组件打开
                sessionListSelectedSessionId = this.DefaultSelectedSessionId;
            }
            //消息区域高度
            messageListHeight = Height - MessageInputHeight - MessageTitleHeight;
            return base.OnParametersSetAsync();
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            //当前用户
            currentUser = await AuthenticationStateManager.GetCurrentUser();

            //订阅消息
            messageNotificationSubscriber = EventBus.Subscribe<WoChatImMessageNotificationData>(OnReceiveMessage);

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
                        sessionListSelectedSession = imSessions.Where(x => x.Id.Equals(sessionListSelectedSessionId)).FirstOrDefault();
                    }
                    else
                    {
                        //默认第一个
                        sessionListSelectedSession = imSessions.FirstOrDefault();
                        sessionListSelectedSessionId = sessionListSelectedSession?.Id;
                    }
                    await LoadCurrentSessionMessage();
                }
                sessionListLoading = false;
            }

        }
        /// <summary>
        /// 点击用户列表某一项
        /// </summary>
        /// <param name="user"></param>
        private void OnClickUserListItem(UserDto user)
        {
            userListSelectedUser = user;
        }

        /// <summary>
        /// 点击发送消息按钮-开启私聊会话
        /// </summary>
        /// <returns></returns>
        private async Task OnClickOpenSession()
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
        private async Task OnClickSessionListItem(ImSessionDto session)
        {
            sessionListSelectedSessionId = session.Id;
            sessionListSelectedSession = session;

            await LoadCurrentSessionMessage();
        }
        /// <summary>
        /// 加载消息列表
        /// </summary>
        /// <returns></returns>
        private async Task LoadCurrentSessionMessage()
        {
            if (sessionListSelectedSessionId != null)
            {
                //查找这个会话的消息列表
                var list = await WoChatImService.GetMySessionMessages(sessionListSelectedSessionId.Value, pageSize: messagePageSize);
                if (list != null)
                {
                    messages = list.ToList();
                }
            }
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
            IEnumerable<int> userIds = openGroupSelectedUsers.SelectKeys(true);
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
                openGroupMode = false;
                openGroupSelectedUsers.SetAllValue(false);
                //选中
                sessionListSelectedSessionId = sessionId;
                await OnChange(tabMessageKey);
            }

        }
        /// <summary>
        /// 点击设置
        /// </summary>
        private void OnClickSetting()
        {
            settingDrawerVisible = true;
        }
        /// <summary>
        /// 给当前会话发送消息
        /// </summary>
        /// <returns></returns>
        private async Task OnClickSendMessage()
        {

            if (sessionListSelectedSession == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(inputMessage))
            {
                MessageService.Warn(Localizer[WoChatResource.PleaseInputContent]);
                return;
            }
            ImSessionMessageDto message = new ImSessionMessageDto();
            message.ImSessionId = sessionListSelectedSession.Id;
            message.Message = inputMessage;
            message.MessageType = ImMessageType.Text;
            await WoChatImService.SendMessage(message);
            inputMessage=string.Empty;
        }
        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private Task OnReceiveMessage(WoChatImMessageNotificationData message)
        {
            System.Console.WriteLine("***"+sessionListSelectedSessionId);
            System.Console.WriteLine("==="+ message.ImMessage.ImSessionId);
            if (message.ImMessage.ImSessionId.Equals(sessionListSelectedSessionId))
            {
                //当前打开会话窗口
                if (messages == null) { messages = new List<ImSessionMessageDto>(); }
                messages.Add(message.ImMessage);
                return InvokeAsync(StateHasChanged);
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// 页面释放
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (messageNotificationSubscriber != null)
            {
                EventBus.UnSubscribe(messageNotificationSubscriber);
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 聊天框滚动到最下面
        /// </summary>
        /// <returns></returns>
        private async Task MessageBoxScrollBarToBottom()
        {
            await JsTool.Document.ScrollBarToBottom(woChatMessageBoxId);
        }
        /// <summary>
        /// 页面渲染后
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await MessageBoxScrollBarToBottom();
            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
