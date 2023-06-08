// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attachment.Enums;
using Gardener.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Attachment.Dtos
{
    /// <summary>
    /// 附件
    /// </summary>
    public class AttachmentDto : TenantBaseDto<Guid>
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        [Required(ErrorMessage = "业务编号不能为空")]
        [DisplayName("BusinessId")]
        public string BusinessId { get; set; } = null!;
        /// <summary>
        /// 附件业务类型
        /// </summary>
        [Required(ErrorMessage = "附件业务类型不能为空")]
        [DisplayName("BusinessType")]
        public AttachmentBusinessType BusinessType { get; set; }
        /// <summary>
        /// 上传的文件类型
        /// </summary>
        [Required(ErrorMessage = "文件类型不能为空")]
        [DisplayName("FileType")]
        public AttachmentFileType FileType { get; set; }
        /// <summary>
        /// 原始类型
        /// </summary>
        [DisplayName("ContentType")]
        public string ContentType { get; set; } = null!;
        /// <summary>
        /// 文件大小 byte
        /// </summary>
        [DisplayName("Size")]
        public long Size { get; set; }
        /// <summary>
        /// 存储地址 无name
        /// </summary>
        [Required, MaxLength(200)]
        [DisplayName("Path")]
        public string Path { get; set; } = null!;

        /// <summary>
        /// 文件名称 随机生成
        /// </summary>
        [Required, MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; } = null!;
        /// <summary>
        /// 原始文件名
        /// </summary>
        [MaxLength(100)]
        [DisplayName("OriginalName")]
        public string OriginalName { get; set; } = null!;
        /// <summary>
        /// 访问地址
        /// </summary>
        [Required, MaxLength(200)]
        [DisplayName("Url")]
        public string Url { get; set; } = null!;
        /// <summary>
        /// 后缀
        /// .jpg
        /// </summary>
        [Required, MaxLength(20)]
        [DisplayName("Suffix")]
        public string Suffix { get; set; } = null!;
    }
}
