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
