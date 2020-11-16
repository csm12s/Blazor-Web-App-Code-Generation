using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 用户和角色关系表
    /// </summary>
    public class RoleResource : IEntity
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Required]
        public int RoleId { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// 权限Id
        /// </summary>
        [Required]
        public int ResourceId { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public Resource Resource { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;


        /// <summary>
        /// 配置多对多关系
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<RoleResource> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {

            entityBuilder.HasComment("角色权限关系表");
            entityBuilder.Property(e => e.RoleId).HasComment("角色id").IsRequired();
            entityBuilder.Property(e => e.ResourceId).HasComment("权限id").IsRequired();
            entityBuilder.Property(e => e.CreatedTime).HasMaxLength(6).HasComment("创建时间").IsRequired();

        }
    }
}