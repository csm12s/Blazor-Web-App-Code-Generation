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
        private readonly IOptions<ApiSettings> options;
        private readonly IAuthenticationStateManager authenticationStateManager;
        /// <summary>
        /// 系统通知处理
        /// </summary>
        /// <param name="options"></param>
        /// <param name="navigationManager"></param>
        public SystemNotificationSignalRHandler(IOptions<ApiSettings> options, IClientLogger clientLogger, IEventBus eventBus, IAuthenticationStateManager authenticationStateManager)
        {
            this.options = options;
            this.clientLogger = clientLogger;
            this.eventBus = eventBus;
            this.authenticationStateManager = authenticationStateManager;
            this.signalRClient = BuildClient();
            
        }
        /// <summary>
        /// 构建client
        /// </summary>
        /// <returns></returns>
        private SignalRClient BuildClient()
        {
            string url = options.Value.BaseAddres + "ws/system-notification";
            SignalRClient signalClient= new SignalRClient(url, clientLogger, async () => {
                TokenOutput token = await authenticationStateManager.GetCurrentToken();
                return token.AccessToken;
            })
            .On<NotificationData>("ReceiveMessage", async (data) =>
            {
                //注册接收调用方法
                if (data == null)
                {
                    return;
                }
                //解析为基本通知事件
                EventInfo<NotificationData> eventInfo = new EventInfo<NotificationData>(EventType.SystemNotify, data);
                eventInfo.EventGroup = data.Type.ToString();
                await eventBus.Publish(eventInfo);
            });

            return signalClient;
        }
        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        public Task Start()
        {
            return signalRClient.Connection();
        }
        
        /// <summary>
        /// 关闭
        /// </summary>
        public Task Close()
        {
            return signalRClient.Close();
        }
        /// <summary>
        /// 向服务端发送通知数据
        /// </summary>
        /// <param name="notificationData"></param>
        /// <returns></returns>
        public Task Send(NotificationData notificationData)
        {
            return signalRClient.SendAsync("Send", notificationData);
        }
    }

}
