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
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class ClientNotifier : IClientNotifier
    {
        private double duration = ClientConstant.ClientNotifierMessageDuration;
        private int msgMaxLength = ClientConstant.ClientNotifierUseMessageMaxLength;

        private readonly MessageService msgSvr;
        private readonly NotificationService notificationService;
        private readonly IClientLocalizer localizer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgSvr"></param>
        /// <param name="notificationService"></param>
        /// <param name="localizer"></param>
        public ClientNotifier(MessageService msgSvr, NotificationService notificationService, IClientLocalizer localizer)
        {
            this.msgSvr = msgSvr;
            this.notificationService = notificationService;
            this.localizer = localizer;
        }
        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="description"></param>
        /// <param name="type"></param>
        /// <returns></returns>
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
        public void Error(string description, Exception? ex = null)
        {
            Error(localizer[SharedLocalResource.Error], description, ex);
        }
        public void Error(string msg, string description, Exception? ex = null)
        {
            Notify(msg, description, NotificationType.Error);
        }
        public void Info(string description)
        {
            Info(localizer[SharedLocalResource.Info], description);
        }
        public void Info(string msg, string description)
        {
            Notify(msg, description, NotificationType.Info);
        }
        public void Success(string description)
        {
            Success(localizer[SharedLocalResource.Success], description);
        }
        public void Success(string msg, string description)
        {
            Notify(msg, description, NotificationType.Success);
        }
        public void Warn(string description, Exception? ex = null)
        {
            Warn(localizer[SharedLocalResource.Warn], description, ex);
        }
        public void Warn(string msg, string description, Exception? ex = null)
        {
            Notify(msg, description, NotificationType.Warning);
        }

        public async Task ErrorAsync(string description, Exception? ex = null)
        {
           await ErrorAsync(localizer[SharedLocalResource.Error], description, ex);
        }

        public async Task ErrorAsync(string msg, string description, Exception? ex = null)
        {
            await Notify(msg, description, NotificationType.Error);
        }

        public async Task InfoAsync(string description)
        {
           await InfoAsync(localizer[SharedLocalResource.Info], description);
        }

        public async Task InfoAsync(string msg, string description)
        {
           await Notify(msg, description, NotificationType.Info);
        }

        public async Task SuccessAsync(string description)
        {
           await SuccessAsync(localizer[SharedLocalResource.Success], description);
        }

        public async Task SuccessAsync(string msg, string description)
        {
           await Notify(msg, description, NotificationType.Success);
        }

        public async Task WarnAsync(string description, Exception? ex = null)
        {
           await WarnAsync(localizer[SharedLocalResource.Warn], description, ex);
        }

        public async Task WarnAsync(string msg, string description, Exception? ex = null)
        {
           await Notify(msg, description, NotificationType.Warning);
        }
    }
}
