// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.WoChat.Enums
{
    /// <summary>
    /// 异常code
    /// </summary>
    public enum ExceptionCode
    {
        /// <summary>
        /// 会话禁止发送消息
        /// </summary>
        [Description("会话禁止发送消息")]
        SessionDisableSendMessage,
    }
}
