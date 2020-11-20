// -----------------------------------------------------------------------------
// 以下代码由 Furion Tools v1.0.0 生成                                          
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

#nullable disable

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 用户扩展信息表
    /// </summary>
    public class UserExtension : IEntity<MasterDbContextLocator>, IEntityTypeBuilder<UserExtension, MasterDbContextLocator>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChat { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<UserExtension> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasKey(e => e.UserId)
                .HasName("PRIMARY");

            entityBuilder.HasComment("用户扩展表");

            entityBuilder.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasComment("用户id");

            entityBuilder.Property(e => e.CityId).HasComment("城市id");

            entityBuilder.Property(e => e.CreatedTime)
                .HasMaxLength(6)
                .HasComment("创建时间").IsRequired();

            entityBuilder.Property(e => e.QQ)
                .HasColumnType("varchar(15)")
                .HasComment("QQ");
            //.HasCharSet("utf8mb4")
            //.HasCollation("utf8mb4_0900_ai_ci");

            entityBuilder.Property(e => e.WeChat)
                    .HasColumnType("varchar(20)")
                    .HasComment("微信");
            //.HasCharSet("utf8mb4")
            //.HasCollation("utf8mb4_0900_ai_ci");

            entityBuilder.HasOne(d => d.User)
                .WithOne(p => p.UserExtension)
                .HasForeignKey<UserExtension>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }

    }
}

