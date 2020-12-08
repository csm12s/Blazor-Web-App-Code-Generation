// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Enums;
using System;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 刷新token
    /// </summary>
    public class UserToken : Entity<Guid>
    {

        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 获取或设置 客户端Id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 登录的客户端类型
        /// </summary>
        public LoginClientType LoginClientType { get; set; }

        /// <summary>
        /// 获取或设置 标识值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 获取或设置 过期时间
        /// </summary>
        public DateTimeOffset EndTime { get; set; }
    }
}
