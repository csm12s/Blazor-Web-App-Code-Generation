// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.Attachment.Enums
{
    /// <summary>
    /// 上传文件类型
    /// </summary>
    public enum AttachmentFileType
    {
        /// <summary>
        /// 图片
        /// </summary>
        [Description("Image")]
        Image,
        /// <summary>
        /// 视频
        /// </summary>
        [Description("Video")]
        Video,
        /// <summary>
        /// 音频
        /// </summary>
        [Description("Audio")]
        Audio,
        /// <summary>
        /// Other
        /// </summary>
        [Description("Other")]
        Other
    }
}
