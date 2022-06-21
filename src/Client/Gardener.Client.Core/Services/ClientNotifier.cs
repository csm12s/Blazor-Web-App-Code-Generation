// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base;
using System;
using System.Threading.Tasks;

namespace Gardener.Client.Core
{
    [ScopedService]
    public class ClientNotifier : IClientNotifier
    {
        private MessageService msgSvr;
        private NotificationService notificationService;
        private double duration = 3;
        private int msgMaxLength = 20;
        public ClientNotifier(MessageService msgSvr, NotificationService notificationService)
        {
            this.msgSvr = msgSvr;
            this.notificationService = notificationService;
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
            await Error("异常通知", description, ex);
        }
        public async Task Error(string msg, string description, Exception ex = null)
        {
            await Notify(msg, description, NotificationType.Error);
        }
        public async Task Info(string description)
        {
            await Info("通知", description);
        }
        public async Task Info(string msg, string description)
        {
            await Notify(msg, description, NotificationType.Info);
        }
        public async Task Success(string description)
        {
            await Success("成功通知", description);
        }
        public async Task Success(string msg, string description)
        {
            await Notify(msg, description, NotificationType.Success);
        }
        public async Task Warn(string description, Exception ex = null)
        {
            await Warn("警告通知", description, ex);
        }
        public async Task Warn(string msg, string description, Exception ex = null)
        {
            await Notify(msg, description, NotificationType.Warning);
        }
    }
}
