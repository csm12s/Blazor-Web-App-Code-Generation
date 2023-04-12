// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.EventBus;
using Gardener.NotificationSystem.Core;
using Gardener.NotificationSystem.Dtos;
using System.Text.Json;

namespace Gardener.NotificationSystem.Client.Core
{
    [ScopedService]
    public class SystemNotificationSignalRClientProvider : ISignalRClientProvider
    {
        private readonly IEventBus _eventBus;
        private readonly ISignalRClientBuilder signalRClientBuilder;
        private readonly IClientLogger clientLogger;

        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventBus"></param>
        /// <param name="signalRClientBuilder"></param>
        public SystemNotificationSignalRClientProvider(IEventBus eventBus, ISignalRClientBuilder signalRClientBuilder, IClientLogger clientLogger)
        {
            _eventBus = eventBus;
            this.signalRClientBuilder = signalRClientBuilder;
            jsonSerializerOptions.Converters.Add(new NotificationDataJsonConverter());
            jsonSerializerOptions.IncludeFields = true;
            this.clientLogger = clientLogger;
        }

        public ISignalRClient GetSignalRClient()
        {
            ISignalRClient signalRClient = signalRClientBuilder
                .GetInstance()
                .SetClientName(NotificationSystemSignalRClientNames.SystemNotificationSignalRClientNames)
                .SetUrl("ws/system-notification")
                .Build();

            signalRClient.On<string>("ReceiveMessage", CallBack);

            return signalRClient;
        }
        /// <summary>
        /// 接收
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        private Task CallBack(string? json)
        {
            try
            {
                if(string.IsNullOrEmpty(json))
                { 
                    return Task.CompletedTask; 
                }
                NotificationData? notificationData = JsonSerializer.Deserialize<NotificationData>(json, jsonSerializerOptions);
                //注册接收调用方法
                if (notificationData == null)
                {
                    return Task.CompletedTask;
                }
                return _eventBus.PublishAsync(notificationData);
            }
            catch (Exception ex) {
                clientLogger.Error("Notification System CallBack Error", ex:ex, sendNotify:false);
                return Task.CompletedTask;
            }
        }

    }
}
