using Fur.DatabaseAccessor;
using Gardener.Core.Enums;
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
    public class Resource : Entity
    {
        /// <summary>
        /// 权限唯一名（每一个接口）
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
        public ICollection<Resource> Childrens { get; set; }
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
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<Resource> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
               .HasMany(x => x.Childrens)
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
    }
}