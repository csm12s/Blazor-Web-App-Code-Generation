// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 用户和角色关系表
    /// </summary>
    public class UserRole : IEntity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 角色信息
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<UserRole> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasComment("用户角色关系表");
            entityBuilder.Property(e => e.UserId).HasComment("用户id").IsRequired();
            entityBuilder.Property(e => e.RoleId).HasComment("角色id").IsRequired();
            entityBuilder.Property(e => e.CreatedTime).HasMaxLength(6).HasComment("创建时间").IsRequired();
        }
    }
}