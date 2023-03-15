﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Dtos
{
    /// <summary>
    /// 客户端信息
    /// </summary>
    public class ClientDto:BaseDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Required(ErrorMessage = "不能为空"), MaxLength(30, ErrorMessage = "最大长度不能大于{1}")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(500, ErrorMessage = "最大长度不能大于{1}"), Required(ErrorMessage = "不能为空")]
        [DisplayName("备注")]
        public string Remark { get; set; } = null!;

        /// <summary>
        /// 联系人
        /// </summary>
        [DisplayName("联系人")]
        [MaxLength(20, ErrorMessage = "最大长度不能大于{1}")]
        public string? Contacts { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [DisplayName("电话")]
        [MaxLength(20, ErrorMessage = "最大长度不能大于{1}")]
        public string? Tel { get; set; }

        /// <summary>
        /// 私钥
        /// </summary>
        [Required(ErrorMessage = "不能为空"), StringLength(64, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("私钥")]
        public string? SecretKey { get; set; }
        
        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50, ErrorMessage = "最大长度不能大于{1}")]
        [DisplayName("邮箱")]
        public string? Email { get; set; }
    }
}
