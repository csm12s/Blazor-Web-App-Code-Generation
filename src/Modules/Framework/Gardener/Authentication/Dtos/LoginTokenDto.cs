// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Enums;
using Gardener.Base;
using System;
using System.ComponentModel;

namespace Gardener.Authentication.Dtos
{
    /// <summary>
    /// 用户Token信息
    /// </summary>
    [Description("用户Token信息")]
    public class LoginTokenDto : TenantBaseDto<Guid>
    {
        /// <summary>
        /// 身份编号
        /// </summary>
        [DisplayName("IdentityId")]
        public string IdentityId { get; set; } = null!;

        /// <summary>
        /// 身份唯一名称
        /// </summary>
        [DisplayName("IdentityName")]
        public string IdentityName { get; set; } = null!;

        /// <summary>
        /// 身份昵称
        /// </summary>
        [DisplayName("IdentityNickName")]
        public string? IdentityNickName { get; set; }

        /// <summary>
        /// 身份类型
        /// </summary>
        [DisplayName("IdentityType")]
        public IdentityType IdentityType { get; set; }

        /// <summary>
        /// 获取或设置 登录Id
        /// </summary>
        [DisplayName("LoginId")]
        public string LoginId { get; set; } = null!;

        /// <summary>
        /// 登录的客户端类型
        /// </summary>
        [DisplayName("LoginClientType")]
        public LoginClientType LoginClientType { get; set; }

        /// <summary>
        /// 获取或设置 标识值
        /// </summary>
        [DisplayName("Value")]
        public string Value { get; set; } = null!;

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        [DisplayName("EndTime")]
        public DateTimeOffset EndTime { get; set; } = default!;

        /// <summary>
        /// 访问IP
        /// </summary>
        [DisplayName("Ip")]
        public string? Ip { get; set; }
    }
}
