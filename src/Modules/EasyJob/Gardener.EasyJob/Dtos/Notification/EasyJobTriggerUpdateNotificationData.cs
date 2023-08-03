// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.NotificationSystem.Dtos;
using Gardener.NotificationSystem.Enums;

namespace Gardener.EasyJob.Dtos.Notification
{
    /// <summary>
    /// 定时任务触发器更新通知
    /// </summary>
    public class EasyJobTriggerUpdateNotificationData : NotificationData
    {
        /// <summary>
        /// 定时任务触发器更新通知
        /// </summary>
        public EasyJobTriggerUpdateNotificationData(SysJobTriggerDto trigger) : base(NotificationDataType.EasyJobTriggerUpdate)
        {
            this.Trigger = trigger;
        }
        /// <summary>
        /// 最新触发器
        /// </summary>
        public SysJobTriggerDto Trigger { get; set; }
    }
}
