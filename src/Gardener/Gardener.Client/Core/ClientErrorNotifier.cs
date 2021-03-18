// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Services;
using System;
using System.Threading.Tasks;

namespace Gardener.Client
{
    public class ClientErrorNotifier : IClientErrorNotifier
    {
        private MessageService msgSvr;
        private NotificationService notificationService;
        private double duration = 3;
        private int msgMaxLength = 20;
        public ClientErrorNotifier(MessageService msgSvr, NotificationService notificationService)
        {
            this.msgSvr = msgSvr;
            this.notificationService = notificationService;
        }
        public async Task Success(string msg, Exception ex = null)
        {
            if (msg?.Length > msgMaxLength)
            {
                Notify("成功通知", msg, NotificationType.Success);
            }
            else
            {
                await msgSvr.Success(msg, duration);
            }
        }
        public async Task Error(string msg, Exception ex = null)
        {
            if (msg?.Length > msgMaxLength)
            {
                Notify("异常通知", msg, NotificationType.Error);
            }
            else
            {
                await msgSvr.Error(msg, duration);
            }
        }
        public async Task Warn(string msg, Exception ex = null)
        {
            if (msg?.Length > msgMaxLength)
            {
                Notify("警告通知", msg, NotificationType.Warning);
            }
            else
            {
                await msgSvr.Warn(msg, duration);
            }
        }
        public async Task Info(string msg, Exception ex = null)
        {
            if (msg?.Length > msgMaxLength)
            {
                Notify("通知", msg, NotificationType.Info);
            }
            else 
            {
                await msgSvr.Info(msg, duration);
            }
        }

        private void Notify(string msg,string description , NotificationType type) 
        {
            notificationService.Open(new NotificationConfig()
            {
                Message = msg,
                Description = description,
                NotificationType = type
            });
        }
    }
}
