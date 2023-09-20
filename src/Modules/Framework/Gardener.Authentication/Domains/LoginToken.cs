// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Attributes;
using Gardener.Authentication.Enums;
using Gardener.Base;
using Gardener.Base.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Authentication.Domains
{
    /// <summary>
    /// 登录Token信息
    /// </summary>
    [Description("登录Token信息")]
    [IgnoreAudit]
    public class LoginToken : GardenerTenantEntityBase<Guid,MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 身份编号
        /// </summary>
        [DisplayName("身份编号")]
        [Required]
        [MaxLength(100)]
        public string IdentityId { get; set; } = null!;

        /// <summary>
        /// 身份唯一名称
        /// </summary>
        [DisplayName("身份唯一名称")]
        [Required]
        [MaxLength(100)]
        public string IdentityName { get; set; } = null!;

        /// <summary>
        /// 身份昵称
        /// </summary>
        [DisplayName("身份昵称")]
        [MaxLength(100)]
        public string? IdentityNickName { get; set; }

        /// <summary>
        /// 身份类型
        /// </summary>
        [DisplayName("身份类型")]
        public IdentityType IdentityType { get; set; }

        /// <summary>
        /// 获取或设置 登录Id
        /// </summary>
        [DisplayName("登录编号")]
        [Required]
        [MaxLength(100)]
        public string LoginId { get; set; } = null!;

        /// <summary>
        /// 登录的客户端类型
        /// </summary>
        [DisplayName("客户端类型")]
        public LoginClientType LoginClientType { get; set; }

        /// <summary>
        /// 获取或设置 标识值
        /// </summary>
        [DisplayName("Token")]
        [MaxLength(2000)]
        public string? Value { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        [DisplayName("过期时间")]
        public DateTimeOffset EndTime { get; set; }

        /// <summary>
        /// 访问IP
        /// </summary>
        [DisplayName("IP")]
        [MaxLength(20)]
        public string? Ip { get; set; }
    }
}
