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

        private Task Notify(string msg, string description, NotificationType type)
        {
            if (description?.Length > msgMaxLength)
            {
                return notificationService.Open(new NotificationConfig()
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
                return msgSvr.Open(new MessageConfig()
                {
                    Content = description,
                    Duration = duration,
                    Type = messageType
                });

            }
        }
        public Task Error(string description, Exception? ex = null)
        {
            return Error(localizer[SharedLocalResource.Error], description, ex);
        }
        public Task Error(string msg, string description, Exception? ex = null)
        {
            return Notify(msg, description, NotificationType.Error);
        }
        public Task Info(string description)
        {
            return Info(localizer[SharedLocalResource.Info], description);
        }
        public Task Info(string msg, string description)
        {
            return Notify(msg, description, NotificationType.Info);
        }
        public Task Success(string description)
        {
            return Success(localizer[SharedLocalResource.Success], description);
        }
        public Task Success(string msg, string description)
        {
            return Notify(msg, description, NotificationType.Success);
        }
        public Task Warn(string description, Exception? ex = null)
        {
            return Warn(localizer[SharedLocalResource.Warn], description, ex);
        }
        public Task Warn(string msg, string description, Exception? ex = null)
        {
            return Notify(msg, description, NotificationType.Warning);
        }
    }
}
