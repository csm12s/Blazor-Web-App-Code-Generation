using AntDesign;
using Gardener.Attachment.Dtos;
using Gardener.Attachment.Enums;
using Gardener.Attachment.Services;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.Client.Base.Components;
using Gardener.Client.Base.Services;
using Gardener.Common;
using Gardener.EventBus;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Gardener.WoChat.Dtos;
using Gardener.WoChat.Dtos.Notification;
using Gardener.WoChat.Enums;
using Gardener.WoChat.Resources;
using Gardener.WoChat.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

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
        private IWoChatImService woChatImService { get; set; } = null!;
        /// <summary>
        /// 用户服务
        /// </summary>
        [Inject]
        private IUserService userService { get; set; } = null!;
        /// <summary>
        /// 消息提示服务
        /// </summary>
        [Inject]
        private IClientMessageService messageService { get; set; } = null!;
        /// <summary>
        /// 身份权限服务
        /// </summary>
        [Inject]
        private IAuthenticationStateManager authenticationStateManager { get; set; } = null!;
        /// <summary>
        /// 时间总线
        /// </summary>
        [Inject]
        private IEventBus eventBus { get; set; } = null!;
        /// <summary>
        /// js工具
        /// </summary>
        [Inject]
        private IJsTool jsTool { get; set; } = null!;
        /// <summary>
        /// 附件
        /// </summary>
        [Inject]
        private IAttachmentService attachmentService { get; set; } = null!;
        /// <summary>
        /// 接口配置
        /// </summary>
        [Inject]
        private IOptions<ApiSettings> ApiSettings { get; set; } = null!;
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
        /// 上传附件附带参数
        /// </summary>
        private Dictionary<string, object> uploadAttachmentInput = new Dictionary<string, object>()
        {
            { "BusinessType", AttachmentBusinessType.WoChat}
        };
        /// <summary>
        /// 上传时添加请求头
        /// </summary>
        private Dictionary<string, string> uploadHeaders = new Dictionary<string, string>();
        /// <summary>
        /// 上传地址
        /// </summary>
        private string uploadUrl
        {
            get
            {
                return ApiSettings.Value.BaseAddres + ApiSettings.Value.UploadPath;
            }
        }
        /// <summary>
        /// 当前用户的聊天图片
        /// </summary>
        private List<string>? currentUserWoChatImages;
        /// <summary>
        /// 是否显示-当前用户的聊天图片
        /// </summary>
        private bool currentUserWoChatImagesBoxVisible = false;
        /// <summary>
        /// 用户选择绑定值
        /// </summary>
        private ClientListBindValue<int, bool> openGroupSelectedUsers = new ClientListBindValue<int, bool>(false);
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
        private List<ImSessionDto>? imSessions;
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
        private Guid? sessionListSelectedSessionId = null;
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
        /// 加载
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
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

            //当前用户
            currentUser = await authenticationStateManager.GetCurrentUser();
            //默认到消息列表
            await OnChange(tabMessageKey);
            //订阅消息
            messageNotificationSubscriber = eventBus.Subscribe<WoChatImUserMessageNotificationData>(OnReceiveMessage);



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
                var usersTemp = await userService.GetAllUsable();
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
                imSessions = (await woChatImService.GetMyImSessions()).ToList();
                if (imSessions != null && imSessions.Any())
                {
                    if (this.sessionListSelectedSessionId != null)
                    {
                        this.sessionListSelectedSession = imSessions.Where(x => x.Id.Equals(sessionListSelectedSessionId)).FirstOrDefault();
                    }
                    else
                    {
                        //默认第一个
                        this.sessionListSelectedSession = imSessions.FirstOrDefault();
                        this.sessionListSelectedSessionId = sessionListSelectedSession?.Id;
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
                messageService.Warn(Localizer[WoChatResource.NoRowsAreSelected]);
                return;
            }

            //发起私聊
            var sessionId = await woChatImService.AddMyImSession(new ImSessionAddInput
            {
                UserIds = new List<int> { userListSelectedUser.Id },
                SessionType = ImSessionType.Personal,
                SessionName = userListSelectedUser.NickName ?? userListSelectedUser.UserName,
                LastMessageTime = DateTimeOffset.Now
            });
            if (sessionId == null)
            {
                messageService.Error(Localizer.Combination(WoChatResource.OpenSession, WoChatResource.Fail));
            }
            else
            {
                //选中
                sessionListSelectedSessionId = sessionId;
                await OnChange(tabMessageKey);
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
            session.UnreadMessageCount = 0;
        }
        /// <summary>
        /// 加载消息列表
        /// </summary>
        /// <returns></returns>
        private async Task LoadCurrentSessionMessage()
        {
            if (this.sessionListSelectedSessionId != null)
            {
                //查找这个会话的消息列表
                var list = await woChatImService.GetMySessionMessages(sessionListSelectedSessionId.Value, pageSize: messagePageSize);
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
                messageService.Warn(Localizer[WoChatResource.NoRowsAreSelected]);
                return;
            }
            //发起群聊
            var sessionId = await woChatImService.AddMyImSession(new ImSessionAddInput
            {
                UserIds = userIds,
                SessionType = ImSessionType.Group,
                SessionName = openGroupName,
                LastMessageTime = DateTimeOffset.Now
            });
            if (sessionId == null)
            {
                messageService.Error(Localizer.Combination(WoChatResource.OpenSession, WoChatResource.Fail));
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
                messageService.Warn(Localizer[WoChatResource.PleaseInputContent]);
                return;
            }
            ImSessionMessageDto message = new ImSessionMessageDto();
            message.ImSessionId = sessionListSelectedSession.Id;
            message.Message = inputMessage;
            message.MessageType = ImMessageType.Text;
            await woChatImService.SendMessage(message);
            inputMessage = string.Empty;
        }
        /// <summary>
        /// 收到消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task OnReceiveMessage(WoChatImUserMessageNotificationData message)
        {
            if (message.ImMessage.ImSessionId.Equals(sessionListSelectedSessionId))
            {
                //当前打开会话窗口
                if (messages == null) { messages = new List<ImSessionMessageDto>(); }
                messages.Add(message.ImMessage);

            }
            else
            {
                //如果会话列表中没有此会话，尝试去获取
                if (imSessions != null && !imSessions.Any(x => x.Id.Equals(message.ImMessage.ImSessionId)))
                {
                    var imSession = await woChatImService.GetImSession(message.ImMessage.ImSessionId);
                    if (imSession != null)
                    {
                        imSession.UnreadMessageCount++;
                        if (imSessions == null)
                        {
                            imSessions = new List<ImSessionDto> { imSession };
                        }
                        else
                        {
                            imSessions.Add(imSession);
                        }
                        imSessions = imSessions.OrderByDescending(x => x.LastMessageTime).ToList();
                    }
                }
                else
                {
                    var session = imSessions?.Where(x => x.Id.Equals(message.ImMessage.ImSessionId)).FirstOrDefault();
                    if (session != null)
                    {
                        session.UnreadMessageCount++;
                    }
                }

            }
            await InvokeAsync(StateHasChanged);
        }
        /// <summary>
        /// 页面释放
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (messageNotificationSubscriber != null)
            {
                eventBus.UnSubscribe(messageNotificationSubscriber);
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 聊天框滚动到最下面
        /// </summary>
        /// <returns></returns>
        private async Task MessageBoxScrollBarToBottom()
        {
            await jsTool.Document.ScrollBarToBottom(woChatMessageBoxId);
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

        /// <summary>
        /// 退出群聊
        /// </summary>
        /// <returns></returns>
        private async Task OnClickQuitGroupChat(string action)
        {
            if (sessionListSelectedSessionId == null)
            {
                messageService.Warn(Localizer[WoChatResource.NoRowsAreSelected]);
                return;
            }

            bool result = await woChatImService.QuitMyImSession(sessionListSelectedSessionId.Value);
            if (result)
            {
                sessionListSelectedSessionId = null;
                sessionListSelectedSession = null;
                await OnChange(tabMessageKey);
            }
            else
            {
                messageService.Error(action + Localizer[WoChatResource.Fail]);
            }
        }

        /// <summary>
        /// 禁言加载中
        /// </summary>
        private bool disableSendMessageLoading = false;
        /// <summary>
        /// 禁言
        /// </summary>
        /// <returns></returns>
        private async Task OnDisableSendMessage()
        {
            if (sessionListSelectedSession == null) { return; }
            disableSendMessageLoading = true;
            bool result = false;
            if (sessionListSelectedSession.DisableSendMessage)
            {
                //开启会话消息发送权限
                result = await woChatImService.EnableSessionSendMessage(sessionListSelectedSession.Id);
            }
            else
            {
                //关闭会话消息发送权限
                result = await woChatImService.DisableSessionSendMessage(sessionListSelectedSession.Id);
            }
            if (result)
            {
                sessionListSelectedSession.DisableSendMessage = !sessionListSelectedSession.DisableSendMessage;
            }
            else
            {
                messageService.Warn(Localizer.Combination(sessionListSelectedSession.DisableSendMessage ? WoChatResource.EnableSessionSendMessage : WoChatResource.DisableSessionSendMessage, WoChatResource.Fail));
            }
            disableSendMessageLoading = false;
        }

        /// <summary>
        /// 点击显示图片列表
        /// </summary>
        /// <returns></returns>
        private async Task OnShowImageList(bool visible)
        {
            //currentUserWoChatImagesBoxVisible = visible;
            if (!visible) { return; }
            //加载图片
            var task = attachmentService.GetMyAttachments(AttachmentBusinessType.WoChat);
            //token
            var task1= authenticationStateManager.GetCurrentTokenHeaders();
            //测试token是否可用
            if (await authenticationStateManager.TestToken("WoChat"))
            {
                //上传附件附带身份信息
                uploadHeaders = await task1 ?? new Dictionary<string, string>();
                //上传附件附带参数
                if (uploadAttachmentInput.ContainsKey("BusinessId"))
                {
                    uploadAttachmentInput["BusinessId"] = sessionListSelectedSessionId != null ? sessionListSelectedSessionId : Guid.Empty;
                }
                else
                {
                    uploadAttachmentInput.Add("BusinessId", sessionListSelectedSessionId != null ? sessionListSelectedSessionId : Guid.Empty);
                }
                //
                IEnumerable<AttachmentDto> attachmentDtos = await task;
                currentUserWoChatImages = attachmentDtos.Select(x => x.Url).ToList();
            }
        }
        /// <summary>
        /// 上传前拦截
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool BeforeUpload(UploadFileItem file)
        {
            var typeOk = file.Type == "image/jpeg" || file.Type == "image/png" || file.Type == "image/gif";
            if (!typeOk)
            {
                messageService.Error("图片只能选择JPG/PNG/GIF文件！");
            }
            var sizeOk = file.Size / 1024 < 500;
            if (!sizeOk)
            {
                messageService.Error("图片必须小于500KB！");
            }
            return typeOk && sizeOk;
        }
        /// <summary>
        /// 上传过程
        /// </summary>
        /// <param name="fileinfo"></param>
        /// <returns></returns>
        private void UploadHandleChange(UploadInfo fileinfo)
        {
            if (sessionListSelectedSession == null) { return; }
            if (fileinfo.File.State == UploadState.Success)
            {
                ApiResult<UploadAttachmentOutput> apiResult = fileinfo.File.GetResponse<ApiResult<UploadAttachmentOutput>>(new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (apiResult.Succeeded && apiResult.Data != null)
                {
                    if (currentUserWoChatImages == null) 
                    {
                        currentUserWoChatImages = new List<string>();
                    }
                    currentUserWoChatImages.Add(apiResult.Data.Url);
                   
                }
                else
                {
                    messageService.Error($"{apiResult.Errors} [{apiResult.StatusCode}]");
                    messageService.Error(Localizer.Combination(SharedLocalResourceKeys.Upload, SharedLocalResourceKeys.Success));
                }
            }
            else if (fileinfo.File.State == UploadState.Fail)
            {
                messageService.Error(Localizer.Combination(SharedLocalResourceKeys.Upload, SharedLocalResourceKeys.Fail));
            }
        }
        /// <summary>
        /// 点击图片
        /// </summary>
        /// <param name="images"></param>
        /// <returns></returns>
        private async Task OnClickImage(string images)
        {
            if (sessionListSelectedSession == null) { return; }
            ImSessionMessageDto message = new ImSessionMessageDto();
            message.ImSessionId = sessionListSelectedSession.Id;
            message.Message = images;
            message.MessageType = ImMessageType.Image;
            await woChatImService.SendMessage(message);
            currentUserWoChatImagesBoxVisible = false;
        }
    }
}
