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
    /// 邮件服务器配置信息
    /// </summary>
    [Description("邮件服务器配置信息")]
    public class EmailServerConfigDto :BaseDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [MaxLength(30, ErrorMessage = "最大长度不能大于{1}"), Required(ErrorMessage = "不能为空")]
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("备注")]
        [MaxLength(500, ErrorMessage = "最大长度不能大于{1}")]
        public string Remark { get; set; }
        /// <summary>
        /// 主机
        /// </summary>
        [DisplayName("主机")]
        [MaxLength(100, ErrorMessage = "最大长度不能大于{1}"), Required(ErrorMessage = "不能为空")]
        public string Host { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        [DisplayName("端口")]
        public int? Port { get; set; }
        /// <summary>
        /// 账户名
        /// </summary>
        [DisplayName("账户名")]
        [MaxLength(50, ErrorMessage = "最大长度不能大于{1}"), Required(ErrorMessage = "不能为空")]
        public string AccountName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        [MaxLength(50, ErrorMessage = "最大长度不能大于{1}"), Required(ErrorMessage = "不能为空")]
        public string AccountPassword { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        /// <remarks>多个标签，逗号隔开</remarks>
        [DisplayName("标签")]
        public string Tags { get; set; }
    }
}
