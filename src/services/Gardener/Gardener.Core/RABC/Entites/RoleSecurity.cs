using Fur.DatabaseAccessor;
using System;

namespace Gardener.Core
{
    /// <summary>
    /// 用户和角色关系表
    /// </summary>
    public class RoleSecurity : IEntity
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }
        public Role Role { get; set; }
        /// <summary>
        /// 权限Id
        /// </summary>
        public int SecurityId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Security Security { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>        
        public DateTimeOffset CreatedTime { get; set; }
    }
}