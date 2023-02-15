// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.NotificationSystem.Dtos;
using System.Threading.Tasks;

namespace Gardener.Client.Base.NotificationSystem
{
    public interface ISystemNotificationSender
    {
        Task Send(NotificationData notificationData);
    }
}