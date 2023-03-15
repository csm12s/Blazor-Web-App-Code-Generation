// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Attachment.Dtos;
using Gardener.Attachment.Enums;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.Client.Base.NotificationSystem;
using Gardener.Common;
using Gardener.EventBus;
using Gardener.NotificationSystem;
using Gardener.NotificationSystem.Dtos.Notification;
using Gardener.NotificationSystem.Enums;
using Gardener.NotificationSystem.Services;
using Gardener.UserCenter.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Gardener.Client.AntDesignUi.Pages
{
    public partial class Home : ReuseTabsPageBase
    {
        public List<ChatDemoNotificationData > datas = new List<ChatDemoNotificationData >();

        private bool _submitting = false;
        private string? _message;
        private string? _currentUserAvatar;
        private IJSObjectReference? jsRef;

        [Inject]
        private IAuthenticationStateManager authenticationStateManager { get; set; } = null!;

        [Inject]
        private IClientLocalizer localizer { get; set; } = null!;

        [Inject]
        private ISystemNotificationSender systemNotificationSender { get; set; } = null!;
        [Inject]
        private MessageService messageService { get; set; } = null!;
        [Inject]
        private IJSRuntime js { get; set; } = null!;
        [Inject]
        private IChatDemoService chatDemoService { get; set; } = null!;
        [Inject]
        private IEventBus eventBus { get; set; } = null!;

        bool _uploadLoading = false;

        [Inject]
        private IOptions<ApiSettings> apiSettings { get; set; } = null!;

        /// <summary>
        /// 上传地址
        /// </summary>
        private string _uploadUrl
        {
            get
            {
                return apiSettings.Value.BaseAddres + apiSettings.Value.UploadPath;
            }
        }

        // <summary>
        /// 上传附件附带参数
        /// </summary>
        private Dictionary<string, object> uploadAttachmentInput = new Dictionary<string, object>()
        {
            { "BusinessType", AttachmentBusinessType.Chat}
        };

        private Dictionary<string, string> headers = null!;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            jsRef = await js.InvokeAsync<IJSObjectReference>("import", "./_content/Gardener.Client.AntDesignUi/Pages/Home.razor.js");

            var user = await authenticationStateManager.GetCurrentUser();
            if (user != null)
            {
                _currentUserAvatar = user.Avatar;
            }

            IEnumerable<ChatDemoNotificationData> history = await chatDemoService.GetHistory();
            if(history != null)
            {
                datas.AddRange(history);
            }
            //进行订阅
            eventBus.Subscribe<ChatDemoNotificationData>(ChatDemoNotificationEventCallBack);
            eventBus.Subscribe<UserOnlineChangeNotificationData>(UserOnlineChangeNotificationEventCallBack);

            //上传附件附带身份信息
            headers = await authenticationStateManager.GetCurrentTokenHeaders();

            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await ChatMessageBoxToBottom();
            await base.OnAfterRenderAsync(firstRender);
        }
        /// <summary>
        /// 聊天信息框滚动到最下面
        /// </summary>
        /// <returns></returns>
        private async Task ChatMessageBoxToBottom()
        {
            if (jsRef != null)
            {
                await jsRef.InvokeVoidAsync("chatMessageBoxToBottom");
            }
        }
        
        /// <summary>
        /// 提交消息
        /// </summary>
        private async Task OnSubmit()
        {
            if (string.IsNullOrEmpty(_message))
            {
                return;
            }
            if (_message.Length > 100)
            {
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                messageService.Warn("内容长度不能超过100");
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                return;
            }
            var user = await authenticationStateManager.GetCurrentUser();

            if (user == null)
            {
                return;
            }
            ChatDemoNotificationData notificationData = new ChatDemoNotificationData ();
            notificationData.Avatar = user.Avatar;
            notificationData.Message = _message;
            notificationData.NickName = user.NickName;
            await systemNotificationSender.Send(notificationData);
            _message = "";
        }

        /// <summary>
        /// 事件回调
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task ChatDemoNotificationEventCallBack(ChatDemoNotificationData e)
        {
            await ShowMessage(e);

        } 
        /// <summary>
        /// 事件回调
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task UserOnlineChangeNotificationEventCallBack(UserOnlineChangeNotificationData e)
        {
            UserOnlineChangeNotificationData notificationData = e;
            UserDto? user = await authenticationStateManager.GetCurrentUser();
            if (user==null || user.Id.ToString().Equals(notificationData.Identity.Id))
            {
                return;
            }
            //用户上下线
            ChatDemoNotificationData chatData = new ChatDemoNotificationData();
            chatData.Avatar = "./assets/logo.png";
            chatData.NickName = "系统";
            if (notificationData.OnlineStatus.Equals(UserOnlineStatus.Online))
            {
                chatData.Message = $"{notificationData.Identity.NickName} 刚刚上线了。IP:[{notificationData.Ip}]";
            }
            else if (notificationData.OnlineStatus.Equals(UserOnlineStatus.Offline))
            {
                chatData.Message = $"{notificationData.Identity.NickName} 刚刚离线了。IP:[{notificationData.Ip}]";
            }
            await ShowMessage(chatData);
        }

        /// <summary>
        /// 展示消息
        /// </summary>
        /// <param name="chatData"></param>
        /// <returns></returns>
        private async Task ShowMessage(ChatDemoNotificationData  chatData) 
        {
            datas.Add(chatData);
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 上传前检测
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private bool UploadBefore(UploadFileItem file)
        {
            var typeOk = file.Type == "image/jpeg" || file.Type == "image/png" || file.Type == "image/gif";
            if (!typeOk)
            {
                messageService.Error("只能选择JPG/PNG/GIF文件！");
            }
            var sizeOk = file.Size / 1024 < 500;
            if (!sizeOk)
            {
                messageService.Error("必须小于500KB！");
            }
            if (uploadAttachmentInput.ContainsKey("BusinessId")) 
            {
                uploadAttachmentInput.Remove("BusinessId");
            }
            //上传附件附带参数
            uploadAttachmentInput.Add("BusinessId", Guid.NewGuid());

            return typeOk && sizeOk;
        }
        /// <summary>
        /// 上传过程
        /// </summary>
        /// <param name="fileinfo"></param>
        /// <returns></returns>
        private async Task UploadHandleChange(UploadInfo fileinfo)
        {
            _uploadLoading = fileinfo.File.State == UploadState.Uploading;

            if (fileinfo.File.State == UploadState.Success)
            {
                ApiResult<UploadAttachmentOutput> apiResult = fileinfo.File.GetResponse<ApiResult<UploadAttachmentOutput>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (apiResult.Succeeded)
                {
                    //发送
                    string imageUrl = apiResult.Data.Url;
                    var user = await authenticationStateManager.GetCurrentUser();

                    if (user == null)
                    {
                        return;
                    }
                    ChatDemoNotificationData notificationData = new ChatDemoNotificationData ();
                    notificationData.Avatar = user.Avatar;
                    notificationData.Images = new string [] { imageUrl };
                    notificationData.NickName = user.NickName;
                    await systemNotificationSender.Send(notificationData);
                }
                else
                {
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                    messageService.Error($"{apiResult.Errors} [{apiResult.StatusCode}]");
                    messageService.Error(localizer[SharedLocalResource.UploadFail]);
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                }
            }
            else if (fileinfo.File.State == UploadState.Fail)
            {
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
                messageService.Error(localizer[SharedLocalResource.UploadFail]);
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
            }
        }
    }
}
