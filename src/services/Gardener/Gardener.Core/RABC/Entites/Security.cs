using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Gardener.Core
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class Security : Entity, IEntitySeedData<Security>
    {
        /// <summary>
        /// 权限唯一名（每一个接口）
        /// </summary>
        [Required,MaxLength(64)]
        public string UniqueName { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        [Required, MaxLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<Role> Roles { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<RoleSecurity> RoleSecurities { get; set; }

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Security> HasData(DbContext dbContext, Type dbContextLocator)
        {
            var securities = typeof(SecurityConst).GetFields().Select(u => u.GetRawConstantValue().ToString()).ToArray();
            var list = new List<Security>();
            for (var i = 1; i < securities.Length + 1; i++)
            {
                list.Add(new Security { Id = i, UniqueName = securities[i - 1] });
            }

            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<Security> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
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

            entityBuilder.Property(e => e.UniqueName).IsRequired()
                .HasColumnType("varchar(64)")
                .HasComment("权限唯一名称");
                //.HasCharSet("utf8mb4")
                //.HasCollation("utf8mb4_0900_ai_ci");

            entityBuilder.Property(e => e.UpdatedTime)
                .HasMaxLength(6)
                .HasComment("更新时间");

        }
    }
}