// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Email.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Email.Dtos
{
    /// <summary>
    /// 发送邮件输入信息
    /// </summary>
    [Description("发送邮件输入信息")]
    public class SendEmailInputDto
    {
        /// <summary>
        /// 模板编号
        /// </summary>
        [DisplayName("模板编号")]
        [Required(ErrorMessage = "不能为空")]
        public Guid TemplateId { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        [DisplayName("数据")]
        [Required(ErrorMessage = "不能为空")]
        public object Data { get; set; }
        /// <summary>
        /// 接收方邮箱地址
        /// </summary>
        [DisplayName("接收方邮箱地址")]
        [MaxLength(100, ErrorMessage = "最大长度不能大于{1}"), Required(ErrorMessage = "不能为空")]
        [RegularExpression("^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$",ErrorMessage ="请输入正确的邮件地址")]
        public string ToEmail { get; set; }
        /// <summary>
        /// 邮件服务器标签
        /// </summary>
        [DisplayName("邮件服务器标签")]
        public EmailServerTag ServerTag { get; set; } = EmailServerTag.Base;
        /// <summary>
        /// 邮件服务器配置编号
        /// </summary>
        /// <remarks>
        /// 优先使用，为空时使用ServerTag
        /// </remarks>
        [DisplayName("邮件服务器配置编号")]
        public Guid EmailServerConfigId { get; set; }
    }
}
