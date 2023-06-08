// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Email.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Email.Dtos
{
    /// <summary>
    /// 邮件服务器配置信息
    /// </summary>
    [Description("邮件服务器配置信息")]
    public class EmailServerConfigDto :BaseDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("Name")]
        [MaxLength(30, ErrorMessage = "最大长度不能大于{1}"), Required(ErrorMessage = "不能为空")]
        public string Name { get; set; } = null!;
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("Remark")]
        [MaxLength(500, ErrorMessage = "最大长度不能大于{1}")]
        public string? Remark { get; set; }
        /// <summary>
        /// 主机
        /// </summary>
        [DisplayName("Host")]
        [MaxLength(100, ErrorMessage = "最大长度不能大于{1}"), Required(ErrorMessage = "不能为空")]
        public string Host { get; set; } = null!;
        /// <summary>
        /// 端口
        /// </summary>
        [DisplayName("Port")]
        [Required(ErrorMessage = "不能为空")]
        public int Port { get; set; }
        /// <summary>
        /// 发件人邮箱
        /// </summary>
        [DisplayName("FromEmail")]
        [MaxLength(100, ErrorMessage = "最大长度不能大于{1}"), Required(ErrorMessage = "不能为空")]
        [RegularExpression("^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", ErrorMessage = "请输入正确的邮件地址")]
        public string FromEmail { get; set; } = null!;
        /// <summary>
        /// 账户名
        /// </summary>
        [DisplayName("AccountName")]
        [MaxLength(50, ErrorMessage = "最大长度不能大于{1}"), Required(ErrorMessage = "不能为空")]
        public string AccountName { get; set; } = null!;
        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("AccountPassword")]
        [MaxLength(50, ErrorMessage = "最大长度不能大于{1}"), Required(ErrorMessage = "不能为空")]
        public string AccountPassword { get; set; } = null!;
        /// <summary>
        /// 标签
        /// </summary>
        /// <remarks>多个标签，逗号隔开</remarks>
        [DisplayName("Tags")]
        public string? Tags { get; set; }


        /// <summary>
        /// 是否启用SSL
        /// </summary>
        [DisplayName("EnableSsl")]
        public bool EnableSsl { get; set; }

        /// <summary>
        /// 获取标签集合
        /// </summary>
        /// <returns></returns>
        public List<EmailServerTag> GetEmailServerTagEnums() 
        {
            List<EmailServerTag> tags=new List<EmailServerTag>();
            if (!string.IsNullOrEmpty(this.Tags))
            {
                foreach (string tag in this.Tags.Split(","))
                {
                    tags.Add(Enum.Parse<EmailServerTag>(tag));
                }
            }
            return tags;
        }
    }
}
