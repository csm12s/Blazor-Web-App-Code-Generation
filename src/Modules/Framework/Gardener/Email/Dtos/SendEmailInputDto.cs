// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Email.Enums;
using System;
using System.ComponentModel;

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
        public Guid TemplateId { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        [DisplayName("数据")]
        public dynamic Data { get; set; }
        /// <summary>
        /// 接收方邮箱地址
        /// </summary>
        [DisplayName("接收方邮箱地址")]
        public string ToEmail { get; set; }
        /// <summary>
        /// 邮件服务器标签
        /// </summary>
        [DisplayName("邮件服务器标签")]
        public EmailServerTag[] ServerTags { get; set; } = new[] { EmailServerTag.Base };
    }
}
