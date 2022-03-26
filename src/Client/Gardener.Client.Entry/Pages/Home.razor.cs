// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Authentication.Dtos;
using Gardener.Client.Base;
using Gardener.EventBus;
using Gardener.NotificationSystem;
using Gardener.NotificationSystem.Client;
using Gardener.NotificationSystem.Dtos;
using Gardener.NotificationSystem.Dtos.Notification;
using Gardener.NotificationSystem.Enums;
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

        public static Action<ChatNotificationData> ShowMessages = (data) => {  };

        private bool _submitting = false;
        private string _message;
        private string _currentUserAvatar;
        private IJSObjectReference? module;

        [Inject]
        private IAuthenticationStateManager authenticationStateManager { get; set; }

        [Inject]
        private IClientLocalizer localizer { get; set; }

        [Inject]
        private SystemNotificationSignalRHandler systemNotificationSignalRHandler { get; set; }
        [Inject]
        private MessageService messageService { get; set; }
        [Inject]
        private IJSRuntime js { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            module = await js.InvokeAsync<IJSObjectReference>("import", "./Pages/Home.razor.js");

            var user= await authenticationStateManager.GetCurrentUser();
            if (user != null)
            {
                _currentUserAvatar=user.Avatar;
            }
            ShowMessages = async (data) => 
            {
                datas.Add(data);
                await InvokeAsync(StateHasChanged);
                await ChatMessageBoxToBottom();
            };
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 聊天信息框滚动到最下面
        /// </summary>
        /// <returns></returns>
        private async Task ChatMessageBoxToBottom()
        {
            await module.InvokeVoidAsync("chatMessageBoxToBottom");
        }
        /// <summary>
        /// 
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
    }
}
