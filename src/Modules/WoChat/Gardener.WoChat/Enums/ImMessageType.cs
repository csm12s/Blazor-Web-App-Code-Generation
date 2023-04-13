// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.WoChat.Enums
{
    /// <summary>
    /// Im消息类型
    /// </summary>
    public enum ImMessageType
    {
        /// <summary>
        /// 系统
        /// </summary>
        [Description("系统")]
        System,
        /// <summary>
        /// 用户离线
        /// </summary>
        [Description("用户离线")]
        UserOffline,
        /// <summary>
        /// 用户上线
        /// </summary>
        [Description("用户上线")]
        UserOnline,
        /// <summary>
        /// 添加用户
        /// </summary>
        [Description("添加用户")]
        AddUser,
        /// <summary>
        /// 移除用户
        /// </summary>
        [Description("移除用户")]
        RemoveUser,
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
        /// 图片
        /// </summary>
        [Description("图片")]
        Image,
        /// <summary>
        /// 文本
        /// </summary>
        [Description("文本")]
        Text,
        /// <summary>
        /// 文件
        /// </summary>
        [Description("文件")]
        File
    }
}
