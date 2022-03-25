// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


using Gardener.Authentication.Dtos;
using Gardener.NotificationSystem.Enums;

namespace Gardener.NotificationSystem.Dtos
{
    /// <summary>
    /// 通知基础消息
    /// </summary>
    public class NotificationData : NotificationDataBase
    {
        /// <summary>
        /// 通知事件类型
        /// </summary>
        public NotificationDataType Type { get; set; }

        /// <summary>
        /// 发送者身份
        /// </summary>
        public Identity? Identity { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }
    }
    ///// <summary>
    ///// 通知基础消息
    ///// </summary>
    ///// <typeparam name="TData"></typeparam>
    //public class NotificationData<TData>: NotificationData where TData : NotificationDataBase
    //{ 
    //    /// <summary>
    //    /// 数据
    //    /// </summary>
    //    public TData? Data { get; set; }
    //}
}
