// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;

namespace Gardener.Common
{
    public class FileTypeDistinguishHelper
    {
        /// <summary>
        /// 识别附件文件类型
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static AttachmentFileType GetAttachmentFileType(string contentType)
        {
            switch (contentType)
            {
                case "image/png": return AttachmentFileType.Image;
                case "image/jpg": return AttachmentFileType.Image;
                case "image/jpeg": return AttachmentFileType.Image;
                case "image/gif": return AttachmentFileType.Image;
                default:return AttachmentFileType.Other;
            }
        }
    }
}
