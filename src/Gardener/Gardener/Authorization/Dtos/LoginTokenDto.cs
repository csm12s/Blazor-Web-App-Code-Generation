// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Enums;
using Gardener.Base;
using Gardener.Enums;
using System;
using System.ComponentModel;

namespace Gardener.Authorization.Dtos
{
    /// <summary>
    /// 用户Token信息
    /// </summary>
    [Description("用户Token信息")]
    public class LoginTokenDto : BaseDto<Guid>
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [DisplayName("用户编号")]
        public int UserId { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        [DisplayName("用户信息")]
        public UserDto User { get; set; }

        /// <summary>
        /// 获取或设置 客户端Id
        /// </summary>
        [DisplayName("客户端编号")]
        public string ClientId { get; set; }

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
