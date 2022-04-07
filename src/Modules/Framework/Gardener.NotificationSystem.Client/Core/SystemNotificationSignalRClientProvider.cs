// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.EventBus;
using Gardener.NotificationSystem.Dtos;

namespace Gardener.NotificationSystem.Client.Core
{
    [ScopedService]
    public class SystemNotificationSignalRClientProvider : ISignalRClientProvider
    {
        private readonly IEventBus _eventBus;
        private readonly ISignalRClientBuilder signalRClientBuilder;

        public SystemNotificationSignalRClientProvider(IEventBus eventBus, ISignalRClientBuilder signalRClientBuilder)
        {
            _eventBus = eventBus;
            this.signalRClientBuilder = signalRClientBuilder;
        }

        public ISignalRClient GetSignalRClient()
        {
            ISignalRClient signalRClient= signalRClientBuilder
                .GetInstance()
                .SetClientName(NotificationSystemSignalRClientNames.SystemNotificationSignalRClientNames)
                .SetUrl("ws/system-notification")
                .Build();

            signalRClient.On<NotificationData>("ReceiveMessage", CallBack);

            return signalRClient;
        }
        /// <summary>
        /// 接收
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        private Task CallBack(NotificationData data)
        {
            //注册接收调用方法
            if (data == null)
            {
                return Task.CompletedTask;
            }
            //解析为基本通知事件
            EventInfo<NotificationData> eventInfo = new EventInfo<NotificationData>(EventType.SystemNotify, data);
            eventInfo.EventGroup = data.Type.ToString();
            return _eventBus.Publish(eventInfo);
        }        
    }
}
