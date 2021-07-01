// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Enums;
using System;
using System.ComponentModel;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 刷新token
    /// </summary>
    [Description("用户Token信息")]
    public class UserToken : GardenerEntityBase<Guid>
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [DisplayName("用户编号")]
        public int UserId { get; set; }

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
