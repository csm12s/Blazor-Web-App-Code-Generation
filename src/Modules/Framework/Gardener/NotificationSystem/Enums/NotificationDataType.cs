// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.NotificationSystem.Enums
{
    /// <summary>
    /// 通知数据类型
    /// </summary>
    public enum NotificationDataType
    {
        /// <summary>
        /// 用户上线下线
        /// </summary>
        UserOnlineChange,
        /// <summary>
        /// 聊天
        /// </summary>
        Chat,
        /// <summary>
        /// WoChatIm用户消息
        /// </summary>
        WoChatImUserMessage,
        /// <summary>
        /// WoChatIm系统消息
        /// </summary>
        WoChatImSystemMessage,
        /// <summary>
        /// 定时任务触发器更新
        /// </summary>
        EasyJobTriggerUpdate,
        /// <summary>
        /// 定时任务运行日志
        /// </summary>
        EasyJobRunLog
    }
}
