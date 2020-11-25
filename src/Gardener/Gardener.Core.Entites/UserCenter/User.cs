using Furion.DatabaseAccessor;
using Furion.DataEncryption;
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
    /// 用户表
    /// </summary>
    public class User : Entity, IEntitySeedData<User>, IEntityTypeBuilder<User>
    {
        /// <summary>
        /// 
        /// </summary>
        public User()
        {
            this.CreatedTime = DateTimeOffset.Now;
        }
        /// <summary>
        /// 用户名
        /// </summary>
        [Required,StringLength(32)]
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(50)]
        public string NickName { get; set; }
        /// <summary>
        /// 密码加密后的
        /// </summary>
        [Required, StringLength(32)]
        public string Password { get; set; }
        /// <summary>
        /// 密码加密Key
        /// </summary>
        [Required, StringLength(32)]
        public string PasswordEncryptKey { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(100)]
        public string Avatar { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50)]
        public string Email { get; set; }
        /// <summary>
        /// 邮箱是否确认
        /// </summary>
        public bool EmailConfirmed { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 手机是否确认
        /// </summary>
        public bool PhoneNumberConfirmed { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required,DefaultValue(Gender.Male)]
        public Gender Gender { get; set; }
        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<Role> Roles { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<UserRole> UserRoles { get; set; }

        /// <summary>
        /// 用户扩展信息
        /// </summary>
        public UserExtension UserExtension { get; set; }
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
        public void Configure(EntityTypeBuilder<User> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasMany(p => p.Roles)
                 .WithMany(p => p.Users)
                 .UsingEntity<UserRole>(
                   u => u.HasOne(c => c.Role).WithMany(c => c.UserRoles).HasForeignKey(c => c.RoleId)
                 , u => u.HasOne(c => c.User).WithMany(c => c.UserRoles).HasForeignKey(c => c.UserId)
                 , u =>
                 {
                     u.HasKey(c => new { c.UserId, c.RoleId });
                     u.HasData(new { UserId = 1, RoleId = 1 ,CreatedTime=DateTimeOffset.Now});
                     u.HasData(new { UserId = 2, RoleId = 2 ,CreatedTime=DateTimeOffset.Now});
                 });

            entityBuilder.HasComment("用户表");

            entityBuilder.Property(e => e.Id).HasComment("用户id");

            entityBuilder.Property(e => e.Gender).HasComment("性别").IsRequired().HasDefaultValue(Gender.Male);

            entityBuilder.Property(e => e.UserName).IsRequired()
                .HasColumnType("varchar(32)")
                .HasComment("账号");
            //.HasCharSet("utf8mb4")
            //.HasCollation("utf8mb4_0900_ai_ci");

            entityBuilder.Property(e => e.Avatar)
                .HasColumnType("varchar(100)")
                .HasComment("头像");
                //.HasCharSet("utf8mb4")
                //.HasCollation("utf8mb4_0900_ai_ci");

            entityBuilder.Property(e => e.CreatedTime).IsRequired()
                .HasMaxLength(6)
                .HasComment("创建时间");

            entityBuilder.Property(e => e.Email)
                .HasColumnType("varchar(50)")
                .HasComment("邮箱");
                //.HasCharSet("utf8mb4")
                //.HasCollation("utf8mb4_0900_ai_ci");

            entityBuilder.Property(e => e.IsDeleted).HasComment("是否删除").IsRequired();

            entityBuilder.Property(e => e.PhoneNumber)
                .HasColumnType("varchar(20)")
                .HasComment("手机");
                //.HasCharSet("utf8mb4")
                //.HasCollation("utf8mb4_0900_ai_ci");

            entityBuilder.Property(e => e.Password).IsRequired()
                .HasColumnType("varchar(32)")
                .HasComment("密码");
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
        public IEnumerable<User> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]
            {
                new User
                {
                    Id=1,
                    UserName="admin",
                    PasswordEncryptKey="xxxxxx" ,
                    Password=MD5Encryption.Encrypt("xxxxxxadmin"),
                    Avatar="https://portrait.gitee.com/uploads/avatars/user/100/302533_hgflydream_1578919799.png"
                },
                new User
                {
                    Id=2,
                    UserName="testuser",
                    PasswordEncryptKey="xxxxxx",
                    Password=MD5Encryption.Encrypt("xxxxxxtestuser"),
                    Avatar="https://portrait.gitee.com/uploads/avatars/user/324/974299_monksoul_1578937227.png"
                }
            };
        }

    }
}