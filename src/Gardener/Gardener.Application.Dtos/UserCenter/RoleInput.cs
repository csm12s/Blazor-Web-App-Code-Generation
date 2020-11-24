﻿namespace Gardener.Core.Dtos
{
    /// <summary>
    /// 角色输入
    /// </summary>
    public class RoleInput
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }
    }
}