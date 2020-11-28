using Furion.DatabaseAccessor;
using Gardener.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class Resource : Entity, IEntitySeedData<Resource>
    {
        /// <summary>
        /// 权限唯一名
        /// GUID
        /// </summary>
        [Required,MaxLength(64)]
        public string ResourceId { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// 权限名称简写
        /// </summary>
        [Required, MaxLength(100)]
        public string SortName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
        /// <summary>
        /// 资源地址
        /// 菜单：页面路由地址
        /// API:接口地址
        /// </summary>
        [MaxLength(200)]
        public string Path { get; set; }
        /// <summary>
        /// 资源图标
        /// </summary>
        [MaxLength(50)]
        public string Icon { get; set; }
        /// <summary>
        /// 资源排序
        /// </summary>
        [Required,DefaultValue(0)]
        public int Order { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// 父级
        /// </summary>
        public Resource Parent { get; set; }
        /// <summary>
        /// 子集
        /// </summary>
        public ICollection<Resource> Children { get; set; }
        /// <summary>
        /// 权限类型
        /// </summary>
        [Required,DefaultValue(ResourceType.API)]
        public ResourceType Type { get; set; }
        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<Role> Roles { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<RoleResource> RoleResources { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLocked { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<Resource> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
               .HasMany(x => x.Children)
               .WithOne(x => x.Parent)
               .HasForeignKey(x => x.ParentId)
               .OnDelete(DeleteBehavior.ClientSetNull); // 必须设置这一行

            entityBuilder.HasComment("权限表");

            entityBuilder.Property(e => e.Id).HasComment("权限id");

            entityBuilder.Property(e => e.CreatedTime).IsRequired()
                .HasMaxLength(6)
                .HasComment("创建时间");

            entityBuilder.Property(e => e.IsDeleted).HasComment("是否删除").IsRequired();

            entityBuilder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasComment("权限名称");
                //.HasCharSet("utf8mb4")
                //.HasCollation("utf8mb4_0900_ai_ci");

            entityBuilder.Property(e => e.Remark)
                .HasColumnType("varchar(500)")
                .HasComment("备注");
            //.HasCharSet("utf8mb4")
            //.HasCollation("utf8mb4_0900_ai_ci");

            entityBuilder.Property(e => e.ResourceId).IsRequired()
                .HasColumnType("varchar(64)")
                .HasComment("权限唯一名称");
            //.HasCharSet("utf8mb4")
            //.HasCollation("utf8mb4_0900_ai_ci");

            entityBuilder.Property(e => e.Path)
                .HasColumnType("varchar(200)")
                .HasComment("资源地址");


            entityBuilder.Property(e => e.Icon)
                .HasColumnType("varchar(50)")
                .HasComment("资源图标");

            entityBuilder.Property(e => e.ParentId)
                .HasComment("父级id");

            entityBuilder.Property(e => e.Order)
                .HasComment("资源排序");

            entityBuilder.Property(e => e.UpdatedTime)
                .HasMaxLength(6)
                .HasComment("更新时间");

        }

        public IEnumerable<Resource> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]
            {
                new Resource
                {
                    Id=1,
                    ResourceId=Guid.NewGuid().ToString(),
                    Name="根节点",
                    SortName="root",
                    Remark="根节点，系统默认",
                    Icon="",
                    Order=0,
                    Type=ResourceType.ROOT,
                    CreatedTime=DateTimeOffset.Now
                },
                new Resource
                {
                    Id=2,
                    ResourceId=Guid.NewGuid().ToString(),
                    Name="用户权限",
                    SortName="user_auth",
                    Remark="用户权限",
                    Icon="verified",
                    Order=0,
                    Type=ResourceType.MENU,
                    CreatedTime=DateTimeOffset.Now,
                    ParentId=1
                },
                new Resource
                {
                    Id=3,
                    ResourceId=Guid.NewGuid().ToString(),
                    Name="用户管理",
                    SortName="user_manager",
                    Remark="用户管理",
                    Icon="user",
                    Order=0,
                    Type=ResourceType.MENU,
                    CreatedTime=DateTimeOffset.Now,
                    ParentId=2
                },
                new Resource
                {
                    Id=4,
                    ResourceId=Guid.NewGuid().ToString(),
                    Name="角色管理",
                    SortName="role_manager",
                    Remark="角色管理",
                    Icon="control",
                    Order=0,
                    Type=ResourceType.MENU,
                    CreatedTime=DateTimeOffset.Now,
                    ParentId=2
                }
                ,
                new Resource
                {
                    Id=5,
                    ResourceId=Guid.NewGuid().ToString(),
                    Name="资源管理",
                    SortName="resource_manager",
                    Remark="资源管理",
                    Icon="api",
                    Order=0,
                    Type=ResourceType.MENU,
                    CreatedTime=DateTimeOffset.Now,
                    ParentId=2
                }
            };
        }
    }
}