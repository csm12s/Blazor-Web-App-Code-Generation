// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.WoChat.Enums
{
    /// <summary>
    /// 会话类型
    /// </summary>
    public enum ImSessionType
    {
        /// <summary>
        /// 群组
        /// </summary>
        [Description("群组")]
        Group,
        /// <summary>
        /// 私聊
        /// </summary>
        [Description("私聊")]
        Personal
    }
}
