// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attributes;
using Gardener.Authentication.Enums;
using Gardener.Base;
using System;
using System.ComponentModel;

namespace Gardener.Authentication.Domains
{
    /// <summary>
    /// 登录Token信息
    /// </summary>
    [Description("登录Token信息")]
    [IgnoreAudit]
    public class LoginToken : GardenerEntityBase<Guid>
    {
        /// <summary>
        /// 身份编号
        /// </summary>
        [DisplayName("身份编号")]
        public string IdentityId { get; set; }

        /// <summary>
        /// 身份唯一名称
        /// </summary>
        [DisplayName("身份唯一名称")]
        public string IdentityName { get; set; }

        /// <summary>
        /// 身份昵称
        /// </summary>
        [DisplayName("身份昵称")]
        public string IdentityGivenName { get; set; }

        /// <summary>
        /// 身份类型
        /// </summary>
        [DisplayName("身份类型")]
        public IdentityType IdentityType { get; set; }

        /// <summary>
        /// 获取或设置 登录Id
        /// </summary>
        [DisplayName("登录编号")]
        public string LoginId { get; set; }

        /// <summary>
        /// 登录的客户端类型
        /// </summary>
        [DisplayName("客户端类型")]
        public LoginClientType LoginClientType { get; set; }

        /// <summary>
        /// 获取或设置 标识值
        /// </summary>
        [DisplayName("Token")]
        public string Value { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        [DisplayName("过期时间")]
        public DateTimeOffset EndTime { get; set; }
    }
}
