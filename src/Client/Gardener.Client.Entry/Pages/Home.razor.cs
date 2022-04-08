// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base;
using Gardener.EventBus;
using Gardener.NotificationSystem;
using Gardener.NotificationSystem.Client;
using Gardener.NotificationSystem.Dtos;
using Gardener.NotificationSystem.Dtos.Notification;
using Gardener.NotificationSystem.Enums;
using Gardener.NotificationSystem.Services;
using Gardener.UserCenter.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Entry.Pages
{
    public partial class Home : ReuseTabsPageBase
    {
        public List<ChatNotificationData> datas = new List<ChatNotificationData>();

        private bool _submitting = false;
        private string _message;
        private string _currentUserAvatar;
        private IJSObjectReference? jsRef;

        [Inject]
        private IAuthenticationStateManager authenticationStateManager { get; set; }

        [Inject]
        private IClientLocalizer localizer { get; set; }

        [Inject]
        private SystemNotificationTransceiver systemNotificationSignalRHandler { get; set; }
        [Inject]
        private MessageService messageService { get; set; }
        [Inject]
        private IJSRuntime js { get; set; }
        [Inject]
        private IChatDemoService chatDemoService { get; set; }
        [Inject]
        private IEventBus eventBus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            jsRef = await js.InvokeAsync<IJSObjectReference>("import", "./Pages/Home.razor.js");

            var user= await authenticationStateManager.GetCurrentUser();
            if (user != null)
            {
                _currentUserAvatar=user.Avatar;
            }
            
            IEnumerable<ChatNotificationData> history= await chatDemoService.GetHistory();
            datas.AddRange(history);
            //进行订阅
            eventBus.Subscribe<EventInfo<NotificationData>>(EventCallBack);
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
        private async void OnSubmit()
        {
            if (string.IsNullOrEmpty(_message))
            {
                return;
            }
            if (_message.Length > 100)
            {
                messageService.Warn("内容长度不能超过100");
                return;
            }
            var user = await authenticationStateManager.GetCurrentUser();

            if (user == null)
            {
                return;
            }
            ChatNotificationData chatData = new ChatNotificationData();
            chatData.Avatar = user.Avatar;
            chatData.Message = _message;
            chatData.NickName = user.NickName;
            NotificationData notificationData = new NotificationData();
            notificationData.Data = System.Text.Json.JsonSerializer.Serialize(chatData);
            notificationData.Type = NotificationDataType.Chat;
            await systemNotificationSignalRHandler.Send(notificationData);
            _message = "";
        }

        /// <summary>
        /// 事件回调
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task EventCallBack(EventInfo<NotificationData> e)
        {
            NotificationData notificationData = e.Data;
            if (notificationData.Type.Equals(NotificationDataType.Chat))
            {
                ChatNotificationData chatData = System.Text.Json.JsonSerializer.Deserialize<ChatNotificationData>(notificationData.Data);
                await ShowMessage(chatData);
            }
            else if (notificationData.Type.Equals(NotificationDataType.UserOnline))
            {
                UserDto user = await authenticationStateManager.GetCurrentUser();
                if (user.Id.ToString().Equals(notificationData.Identity.Id))
                {
                    return;
                }
                UserOnlineChangeNotificationData userOnlineNotification = System.Text.Json.JsonSerializer.Deserialize<UserOnlineChangeNotificationData>(notificationData.Data);
                //用户上下线
                ChatNotificationData chatData = new ChatNotificationData();
                chatData.Avatar = "./assets/logo.png";
                chatData.NickName = "系统";
                if (userOnlineNotification.OnlineStatus.Equals(UserOnlineStatus.Online))
                {
                    chatData.Message = $"{notificationData.Identity.GivenName} 刚刚上线了。IP:[{notificationData.Ip}]";
                }
                else if (userOnlineNotification.OnlineStatus.Equals(UserOnlineStatus.Offline))
                {
                    chatData.Message = $"{notificationData.Identity.GivenName} 刚刚离线了。IP:[{notificationData.Ip}]";
                }
                await ShowMessage(chatData);
            }
           
        }
        
        /// <summary>
        /// 展示消息
        /// </summary>
        /// <param name="chatData"></param>
        /// <returns></returns>
        private async Task ShowMessage(ChatNotificationData chatData) 
        {
            datas.Add(chatData);
            await InvokeAsync(StateHasChanged);
        }
    }
}
