// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.EventBus;
using Gardener.LocalizationLocalizer;
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
        private readonly ILocalizationLocalizer localizer;

        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventBus"></param>
        /// <param name="signalRClientBuilder"></param>
        /// <param name="clientLogger"></param>
        /// <param name="localizer"></param>
        public SystemNotificationSignalRClientProvider(IEventBus eventBus, ISignalRClientBuilder signalRClientBuilder, IClientLogger clientLogger, ILocalizationLocalizer localizer)
        {
            _eventBus = eventBus;
            this.signalRClientBuilder = signalRClientBuilder;
            jsonSerializerOptions.Converters.Add(new NotificationDataJsonConverter());
            jsonSerializerOptions.IncludeFields = true;
            this.clientLogger = clientLogger;
            this.localizer = localizer;
        }

        public ISignalRClient GetSignalRClient()
        {
            ISignalRClient signalRClient = signalRClientBuilder
                .GetInstance()
                .SetClientName(localizer[NotificationSystemSignalRClientNames.SystemNotificationSignalRClientName])
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
                _eventBus.Publish(notificationData);
            }
            catch (Exception ex) {
                clientLogger.Error(localizer.Combination(NotificationSystemSignalRClientNames.SystemNotificationSignalRClientName, "CallBack", "Error"), ex:ex, sendNotify:false);
            }
            return Task.CompletedTask;
        }

    }
}
