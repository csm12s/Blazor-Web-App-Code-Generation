// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Constants;
using Gardener.Client.Base;
using Gardener.LocalizationLocalizer;

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
        private readonly ILocalizationLocalizer localizer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgSvr"></param>
        /// <param name="notificationService"></param>
        /// <param name="localizer"></param>
        public ClientNotifier(MessageService msgSvr, NotificationService notificationService, ILocalizationLocalizer localizer)
        {
            this.msgSvr = msgSvr;
            this.notificationService = notificationService;
            this.localizer = localizer;
        }
        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private Task Notify(string title, string description, NotificationType type)
        {
            if (description?.Length > msgMaxLength)
            {
                return notificationService.Open(new NotificationConfig()
                {
                    Message = title,
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
                    Content = (string.IsNullOrEmpty(title) ? "" : "[" + title + "]") + description,
                    Duration = duration,
                    Type = messageType
                });

            }
        }

        public void Error(string description, string? title = null, Exception? ex = null)
        {
            ErrorAsync(description, title, ex);
        }

        public Task ErrorAsync(string description, string? title = null, Exception? ex = null)
        {
            return Notify(title ?? SharedLocalResource.Error, description, NotificationType.Error);
        }

        public void Info(string description, string? title = null)
        {
            InfoAsync(description, title);
        }

        public Task InfoAsync(string description, string? title = null)
        {
            return Notify(title ?? SharedLocalResource.Info, description, NotificationType.Info);
        }

        public void Success(string description, string? title = null)
        {
            SuccessAsync(description, title);
        }

        public Task SuccessAsync(string description, string? title = null)
        {
            return Notify(title ?? SharedLocalResource.Success, description, NotificationType.Success);
        }

        public void Warn(string description, string? title = null, Exception? ex = null)
        {
            WarnAsync(description, title, ex);
        }

        public Task WarnAsync(string description, string? title = null, Exception? ex = null)
        {
            return Notify(title ?? SharedLocalResource.Warn, description, NotificationType.Warning);
        }
    }
}
