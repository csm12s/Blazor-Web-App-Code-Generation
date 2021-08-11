// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 用户和角色关系表
    /// </summary>
    [Description("用户角色信息")]
    public class UserRole : IEntity, IEntitySeedData<UserRole>, IEntityTypeBuilder<UserRole>
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [DisplayName("用户编号")]
        public int UserId { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        [DisplayName("用户")]
        public User User { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        [DisplayName("角色编号")]
        public int RoleId { get; set; }
        /// <summary>
        /// 角色信息
        /// </summary>
        [DisplayName("角色")]
        public Role Role { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<UserRole> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
        }
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<UserRole> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[] {
                new UserRole{ UserId = 1, RoleId = 1, CreatedTime = DateTimeOffset.FromUnixTimeSeconds(1628689311) },
                new UserRole{ UserId = 2, RoleId = 2, CreatedTime = DateTimeOffset.FromUnixTimeSeconds(1628689311) }
            };
        }
    }
}