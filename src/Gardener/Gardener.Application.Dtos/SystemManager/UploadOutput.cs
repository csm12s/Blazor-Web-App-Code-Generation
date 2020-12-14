// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Application.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class UploadOutput
    {
        /// <summary>
        /// 文件地址
        /// </summary>
        public string FileUrl { get; set; }
        /// <summary>
        /// 附件信息
        /// </summary>
        public AttachmentDto Attachment { get; set; }
    }
}
