// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Constants;
using Gardener.Client.Base;

namespace Gardener.Client.AntDesignUi.Services
{
    [ScopedService]
    public class ClientNotifier : IClientNotifier
    {
        private double duration = ClientConstant.ClientNotifierMessageDuration;
        private int msgMaxLength = ClientConstant.ClientNotifierUseMessageMaxLength;

        private readonly MessageService msgSvr;
        private readonly NotificationService notificationService;
        private readonly IClientLocalizer localizer;
        public ClientNotifier(MessageService msgSvr, NotificationService notificationService, IClientLocalizer localizer)
        {
            this.msgSvr = msgSvr;
            this.notificationService = notificationService;
            this.localizer = localizer;
        }

        private async Task Notify(string msg, string description, NotificationType type)
        {
            if (description?.Length > msgMaxLength)
            {
                await notificationService.Open(new NotificationConfig()
                {
                    Message = msg,
                    Description = description,
                    NotificationType = type
                });
            }
            else
            {
                MessageType messageType = MessageType.Info;
                switch (type)
                {
                    case NotificationType.Error: messageType = MessageType.Error; break;
                    case NotificationType.Warning: messageType = MessageType.Warning; break;
                    case NotificationType.Info: messageType = MessageType.Info; break;
                    case NotificationType.Success: messageType = MessageType.Success; break;
                    default: messageType = MessageType.Info; break;
                }
                await msgSvr.Open(new MessageConfig()
                {
                    Content = description,
                    Duration = duration,
                    Type = messageType
                });

            }
        }
        public async Task Error(string description, Exception ex = null)
        {
            await Error(localizer[SharedLocalResource.Error], description, ex);
        }
        public async Task Error(string msg, string description, Exception ex = null)
        {
            await Notify(msg, description, NotificationType.Error);
        }
        public async Task Info(string description)
        {
            await Info(localizer[SharedLocalResource.Info], description);
        }
        public async Task Info(string msg, string description)
        {
            await Notify(msg, description, NotificationType.Info);
        }
        public async Task Success(string description)
        {
            await Success(localizer[SharedLocalResource.Success], description);
        }
        public async Task Success(string msg, string description)
        {
            await Notify(msg, description, NotificationType.Success);
        }
        public async Task Warn(string description, Exception ex = null)
        {
            await Warn(localizer[SharedLocalResource.Warn], description, ex);
        }
        public async Task Warn(string msg, string description, Exception ex = null)
        {
            await Notify(msg, description, NotificationType.Warning);
        }
    }
}
