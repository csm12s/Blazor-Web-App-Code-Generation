using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class Role : Entity, IEntitySeedData<Role>, IEntityTypeBuilder<Role>
    {
        /// <summary>
        /// 
        /// </summary>
        public Role()
        {
            this.CreatedTime = DateTimeOffset.Now;
        }
        /// <summary>
        /// 角色名称
        /// </summary>
        [MaxLength(100), Required]
        public string Name { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        [MaxLength(500), Required]
        public string Remark { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<User> Users { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<UserRole> UserRoles { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<Resource> Resources { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<RoleResource> RoleResources { get; set; }

        /// <summary>
        /// 配置多对多关系
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<Role> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasMany(p => p.Resources)
                .WithMany(p => p.Roles)
                .UsingEntity<RoleResource>(
                  u => u.HasOne(c => c.Resource).WithMany(c => c.RoleResources).HasForeignKey(c => c.ResourceId)
                , u => u.HasOne(c => c.Role).WithMany(c => c.RoleResources).HasForeignKey(c => c.RoleId)
                , u =>
                {
                    u.HasKey(c => new { c.RoleId, c.ResourceId });
                  
                });


            entityBuilder.HasComment("角色表");

            entityBuilder.Property(e => e.Id).HasComment("角色id");

            entityBuilder.Property(e => e.CreatedTime).IsRequired()
                .HasMaxLength(6)
                .HasComment("创建时间");

            entityBuilder.Property(e => e.IsDeleted).IsRequired().HasComment("是否删除");

            entityBuilder.Property(e => e.Name)
                .HasColumnType("varchar(100)")
                .IsRequired()
                .HasComment("名称");
            //.HasCharSet("utf8mb4")
            //.HasCollation("utf8mb4_0900_ai_ci");

            entityBuilder.Property(e => e.Remark)
                .HasColumnType("varchar(500)")
                .HasComment("备注");
                //.HasCharSet("utf8mb4")
                //.HasCollation("utf8mb4_0900_ai_ci");

            entityBuilder.Property(e => e.UpdatedTime)
                .HasMaxLength(6)
                .HasComment("更新时间");
        }

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Role> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]
            {
                new Role
                {
                    Id=1,Name="超级管理员",Remark="拥有所有权限"
                },
                new Role
                {
                    Id=2,Name="普通人",Remark="没有关联权限"
                }
            };
        }
    }
}