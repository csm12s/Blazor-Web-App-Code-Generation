// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.Attachment.Enums
{
    /// <summary>
    /// 附件业务类型类型
    /// </summary>
    public enum AttachmentBusinessType
    {
        /// <summary>
        /// 头像
        /// </summary>
        [Description("头像")]
        Avatar,
        /// <summary>
        /// 订单
        /// </summary>
        [Description("订单")]
        Order,
        /// <summary>
        /// 聊天
        /// </summary>
        [Description("聊天")]
        Chat
    }
}
