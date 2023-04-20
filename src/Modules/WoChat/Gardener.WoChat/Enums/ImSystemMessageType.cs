// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.WoChat.Enums
{
    /// <summary>
    /// 系统消息
    /// </summary>
    public enum ImSystemMessageType
    {
        /// <summary>
        /// 添加用户
        /// </summary>
        [Description("用户加入")]
        UserJoin,
        /// <summary>
        /// 移除用户
        /// </summary>
        [Description("用户离开")]
        UserQuit,
        /// <summary>
        /// 会话禁言
        /// </summary>
        [Description("会话禁言")]
        DisableSessionSendMessage,
        /// <summary>
        /// 解除会话禁言
        /// </summary>
        [Description("解除会话禁言")]
        EnableSessionSendMessage
    }
}
