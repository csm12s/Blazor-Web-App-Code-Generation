// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Email.Dtos
{
    /// <summary>
    /// 邮件模板信息
    /// </summary>
    [Description("邮件模板信息")]
    public class EmailTemplateDto : BaseDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("Name")]
        [MaxLength(30, ErrorMessage = "最大长度不能大于{1}"), Required(ErrorMessage = "不能为空")]
        public string Name { get; set; } = null!;
        /// <summary>
        /// 发件人
        /// </summary>
        [DisplayName("FromName")]
        [MaxLength(100, ErrorMessage = "最大长度不能大于{1}")]
        public string FromName { get; set; } = null!;
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("Remark")]
        [MaxLength(500, ErrorMessage = "最大长度不能大于{1}")]
        public string? Remark { get; set; }
        /// <summary>
        /// 主题模板
        /// </summary>
        [DisplayName("SubjectTemplate")]
        [MaxLength(1000, ErrorMessage = "最大长度不能大于{1}")]
        public string? SubjectTemplate { get; set; }
        /// <summary>
        /// 内容模板
        /// </summary>
        [DisplayName("ContentTemplate")]
        [Required(ErrorMessage = "不能为空"),MaxLength(5000, ErrorMessage = "最大长度不能大于{1}")]
        public string ContentTemplate { get; set; } = null!;
        /// <summary>
        /// 例子
        /// </summary>
        [DisplayName("Example")]
        [MaxLength(1000, ErrorMessage = "最大长度不能大于{1}")]
        public string? Example { get; set; }

        /// <summary>
        /// 是否是HTML内容
        /// </summary>
        [DisplayName("IsHtml")]
        [Required(ErrorMessage = "不能为空")]
        public bool IsHtml { get; set; }
    }
}
