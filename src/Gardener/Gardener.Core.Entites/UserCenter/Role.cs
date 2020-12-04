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
        /// 是否是超级管理员
        /// 超级管理员拥有所有权限
        /// </summary>
        public bool IsSuperAdministrator { get; set; }
        /// <summary>
        /// 是否是默认权限
        /// 注册用户时默认设置
        /// </summary>
        public bool IsDefault { get; set; }
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
        /// 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }
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
                    u.HasData(new { RoleId = 2, ResourceId = Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 2, ResourceId = Guid.Parse("825e4fbd-c88c-4028-b864-a7d7363e9550"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 2, ResourceId = Guid.Parse("8910d2d6-784b-4331-a5bc-22e2a943aa9f"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 2, ResourceId = Guid.Parse("8b78e71e-bd7f-4264-80cd-0ec2964b4f63"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 2, ResourceId = Guid.Parse("ba6dc63f-dff8-4899-922c-38f2b4ce415d"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 2, ResourceId = Guid.Parse("c8540d46-fe88-4858-8ab7-f8b427695e77"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 2, ResourceId = Guid.Parse("de8658ed-e997-4c07-861e-721c6275ca38"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 3, ResourceId = Guid.Parse("1cba3770-9b4e-4c69-9973-07c4f8555a3f"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 3, ResourceId = Guid.Parse("25dcaaca-6f97-45f3-952d-05112f07c677"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 3, ResourceId = Guid.Parse("371b335b-29e5-4846-b6de-78c9cc691717"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 3, ResourceId = Guid.Parse("3c124d95-dd76-4903-b240-a4fe4df93868"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 3, ResourceId = Guid.Parse("7034e707-93fc-453c-9dfe-20a0cb58297d"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 3, ResourceId = Guid.Parse("825e4fbd-c88c-4028-b864-a7d7363e9550"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 3, ResourceId = Guid.Parse("8910d2d6-784b-4331-a5bc-22e2a943aa9f"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 3, ResourceId = Guid.Parse("8b78e71e-bd7f-4264-80cd-0ec2964b4f63"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 3, ResourceId = Guid.Parse("91517bf1-ef41-4ddb-8daa-5022c59d2c73"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 3, ResourceId = Guid.Parse("b8224935-fae6-4bbe-ad91-1d8969baabe8"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 3, ResourceId = Guid.Parse("ba6dc63f-dff8-4899-922c-38f2b4ce415d"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 3, ResourceId = Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 3, ResourceId = Guid.Parse("c8540d46-fe88-4858-8ab7-f8b427695e77"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 3, ResourceId = Guid.Parse("d56889de-4008-42ed-9166-b21cdc0c7fcf"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 3, ResourceId = Guid.Parse("d88fbd6d-00ce-4687-9440-a7ffd5aab2aa"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 3, ResourceId = Guid.Parse("dc7cf259-5c60-47c9-a02b-1fc9b04c9582"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 3, ResourceId = Guid.Parse("de8658ed-e997-4c07-861e-721c6275ca38"),CreatedTime=DateTimeOffset.Now});
                    u.HasData(new { RoleId = 3, ResourceId = Guid.Parse("e30bbf62-d6d3-4e72-ac1a-abb285587632"),CreatedTime=DateTimeOffset.Now});
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
                    Id=1,Name="超级管理员",Remark="拥有所有权限",IsSuperAdministrator=true,CreatedTime=DateTimeOffset.Now
                },
                new Role
                {
                    Id=2,Name="登录者",Remark="只能登录系统",CreatedTime=DateTimeOffset.Now,IsDefault=true
                },
                new Role
                {
                    Id=3,Name="浏览者",Remark="只能浏览",CreatedTime=DateTimeOffset.Now
                }
            };
        }
    }
}