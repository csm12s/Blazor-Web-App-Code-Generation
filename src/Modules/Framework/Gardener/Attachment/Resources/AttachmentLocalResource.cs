// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;

namespace Gardener.Attachment.Resources
{
    /// <summary>
    /// 附件本地化资源
    /// </summary>
    public class AttachmentLocalResource : SharedLocalResourceKeys
    {
        /// <summary>
        /// 业务类型
        /// </summary>
        public const string BusinessType = nameof(BusinessType);
        /// <summary>
        /// 业务编号
        /// </summary>
        public const string BusinessId = nameof(BusinessId);
        /// <summary>
        /// 文件类型
        /// </summary>
        public const string FileType = nameof(FileType);
        /// <summary>
        /// 大小
        /// </summary>
        public const string Size = nameof(Size);
        /// <summary>
        /// 后缀
        /// </summary>
        public const string Suffix = nameof(Suffix);
        /// <summary>
        /// 原始名称
        /// </summary>
        public const string OriginalName = nameof(OriginalName);
        /// <summary>
        /// 访问地址
        /// </summary>
        public const string Url = nameof(Url);
        /// <summary>
        /// 原始类型
        /// </summary>
        public const string ContentType = nameof(ContentType);
    }
}
