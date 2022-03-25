// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Dtos;
using Gardener.Client.Base;
using Gardener.EventBus;
using Gardener.NotificationSystem.Client.Core;
using Gardener.NotificationSystem.Dtos;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;

namespace Gardener.NotificationSystem.Client
{
    /// <summary>
    /// 系统通知处理
    /// </summary>
    public class SystemNotificationSignalRHandler
    {
        private readonly SignalRClient signalRClient;
        private readonly IClientLogger clientLogger;
        private readonly IEventBus eventBus;
        /// <summary>
        /// 系统通知处理
        /// </summary>
        /// <param name="options"></param>
        /// <param name="navigationManager"></param>
        public SystemNotificationSignalRHandler(IOptions<ApiSettings> options, IClientLogger clientLogger, IEventBus eventBus, IAuthenticationStateManager authenticationStateManager)
        {
            this.clientLogger = clientLogger;
            this.eventBus = eventBus;
            string url = options.Value.BaseAddres + "ws/system-notification";
            signalRClient = new SignalRClient(url, clientLogger, ConnectedCallBack, async () => {
                TokenOutput token= await authenticationStateManager.GetCurrentToken();
                return token.AccessToken;
            });
        }
        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        public async Task Start()
        {
            await signalRClient.Connection();
        }
        /// <summary>
        /// 连接回调
        /// </summary>
        /// <param name="hubConnection"></param>
        private void ConnectedCallBack(HubConnection hubConnection)
        {
            hubConnection.On<NotificationData>("ReceiveMessage", async (data) =>
            {
                if (data==null)
                {
                    return;
                }
                //解析为基本通知事件
                EventInfo<NotificationData> eventInfo = new EventInfo<NotificationData>(EventType.SystemNotify, data);
                eventInfo.EventGroup = data.Type.ToString();
                await eventBus.Publish(eventInfo);
            });
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public async Task Close()
        {
            await signalRClient.Close();
        }
    }

}
