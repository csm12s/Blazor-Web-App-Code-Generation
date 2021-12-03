// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attachment.Enums;

namespace Gardener.Attachment.Core
{
    /// <summary>
    /// 识别附件文件类型
    /// </summary>
    public class FileTypeDistinguishHelper
    {
        /// <summary>
        /// 识别附件文件类型
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static AttachmentFileType GetAttachmentFileType(string contentType)
        {
            if (string.IsNullOrEmpty(contentType))
            {
                return AttachmentFileType.Other;
            }
            contentType = contentType.Split("/")[0];
            switch (contentType)
            {
                case "image": return AttachmentFileType.Image;
                case "video": return AttachmentFileType.Video;
                case "audio": return AttachmentFileType.Audio;
                default:return AttachmentFileType.Other;
            }
        }
    }
}
