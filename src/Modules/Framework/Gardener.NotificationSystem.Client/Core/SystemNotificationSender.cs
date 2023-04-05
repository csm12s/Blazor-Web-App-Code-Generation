// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Base.NotificationSystem;
using Gardener.NotificationSystem.Dtos;

namespace Gardener.NotificationSystem.Client.Core
{
    [ScopedService]
    public class SystemNotificationSender : ISystemNotificationSender
    {
        private readonly ISignalRClientManager signalRClientManager;

        public SystemNotificationSender(ISignalRClientManager signalRClientManager)
        {
            this.signalRClientManager = signalRClientManager;
        }



        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="notificationData"></param>
        /// <returns></returns>
        public Task Send(NotificationData notificationData)
        {
            ISignalRClient signalRClient = signalRClientManager.Get(NotificationSystemSignalRClientNames.SystemNotificationSignalRClientNames);
            return signalRClient.SendAsync("Send", notificationData);
        }
    }
}
