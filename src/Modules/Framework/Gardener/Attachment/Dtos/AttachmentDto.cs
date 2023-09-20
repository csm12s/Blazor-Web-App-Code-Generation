// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attachment.Enums;
using Gardener.Attachment.Resources;
using Gardener.Base;
using Gardener.Base.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Attachment.Dtos
{
    /// <summary>
    /// 附件
    /// </summary>
    [Display(Name = nameof(AttachmentLocalResource.Attachment), ResourceType = typeof(AttachmentLocalResource))]
    public class AttachmentDto : TenantBaseDto<Guid>
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        [Display(Name = nameof(AttachmentLocalResource.BusinessId), ResourceType = typeof(AttachmentLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(64, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string BusinessId { get; set; } = null!;
        /// <summary>
        /// 附件业务类型
        /// </summary>
        [Display(Name = nameof(AttachmentLocalResource.BusinessType), ResourceType = typeof(AttachmentLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public AttachmentBusinessType BusinessType { get; set; }
        /// <summary>
        /// 上传的文件类型
        /// </summary>
        [Display(Name = nameof(AttachmentLocalResource.FileType), ResourceType = typeof(AttachmentLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        public AttachmentFileType FileType { get; set; }
        /// <summary>
        /// 原始类型
        /// </summary>
        [Display(Name = nameof(AttachmentLocalResource.ContentType), ResourceType = typeof(AttachmentLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string ContentType { get; set; } = null!;
        /// <summary>
        /// 文件大小 byte
        /// </summary>
        [Display(Name = nameof(AttachmentLocalResource.Size), ResourceType = typeof(AttachmentLocalResource))]
        public long Size { get; set; }
        /// <summary>
        /// 存储地址 无name
        /// </summary>
        [Display(Name = nameof(AttachmentLocalResource.Path), ResourceType = typeof(AttachmentLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Path { get; set; } = null!;

        /// <summary>
        /// 文件名称 随机生成
        /// </summary>
        [Display(Name = nameof(AttachmentLocalResource.Name), ResourceType = typeof(AttachmentLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Name { get; set; } = null!;
        /// <summary>
        /// 原始文件名
        /// </summary>
        [Display(Name = nameof(AttachmentLocalResource.OriginalName), ResourceType = typeof(AttachmentLocalResource))]
        [MaxLength(100, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string? OriginalName { get; set; } = null!;
        /// <summary>
        /// 访问地址
        /// </summary>
        [Display(Name = nameof(AttachmentLocalResource.Url), ResourceType = typeof(AttachmentLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(200, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Url { get; set; } = null!;
        /// <summary>
        /// 后缀
        /// .jpg
        /// </summary>
        [Display(Name = nameof(AttachmentLocalResource.Suffix), ResourceType = typeof(AttachmentLocalResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string Suffix { get; set; } = null!;
    }
}
