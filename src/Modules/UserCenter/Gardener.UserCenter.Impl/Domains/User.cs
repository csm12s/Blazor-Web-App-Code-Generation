// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DataEncryption;
using Gardener.Base;
using Gardener.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Impl.Domains
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Description("用户信息")]
    public class User : GardenerEntityBase, IEntitySeedData<User>, IEntityTypeBuilder<User>
    {
        /// <summary>
        /// 用户表
        /// </summary>
        public User()
        {
            Roles = new List<Role>();
            UserRoles = new List<UserRole>();
        }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required, StringLength(32)]
        [DisplayName("用户名")]
        public string UserName { get; set; } = null!;
        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(50)]
        [DisplayName("昵称")]
        public string? NickName { get; set; }
        /// <summary>
        /// 密码加密后的
        /// </summary>
        [Required, StringLength(64)]
        [DisplayName("密码")]
        public string Password { get; set; } = null!;
        /// <summary>
        /// 密码加密Key
        /// </summary>
        [Required, StringLength(64)]
        [DisplayName("密码加密KEY")]
        public string PasswordEncryptKey { get; set; } = null!;
        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(100)]
        [DisplayName("头像")]
        public string? Avatar { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50)]
        [DisplayName("邮箱")]
        public string? Email { get; set; }
        /// <summary>
        /// 邮箱是否确认
        /// </summary>
        [DisplayName("邮箱是否确认")]
        public bool EmailConfirmed { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [MaxLength(20)]
        [DisplayName("手机")]
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// 手机是否确认
        /// </summary>
        [DisplayName("手机是否确认")]
        public bool PhoneNumberConfirmed { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required, DefaultValue(Gender.Male)]
        [DisplayName("性别")]
        public Gender Gender { get; set; }
        /// <summary>
        /// 多对多
        /// </summary>
        [DisplayName("角色")]
        public ICollection<Role> Roles { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<UserRole> UserRoles { get; set; }

        /// <summary>
        /// 用户扩展信息
        /// </summary>
        public UserExtension? UserExtension { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        [DisplayName("部门编号")]
        public int? DeptId { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public Dept? Dept { get; set; }
        /// <summary>
        /// 岗位编号
        /// </summary>
        [DisplayName("岗位编号")]
        public int? PositionId { get; set; }
        /// <summary>
        /// 岗位
        /// </summary>
        public Position? Position;

        /// <summary>
        /// 配置多对多关系
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<User> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
                .HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<UserRole>(
                    x => x.HasOne(r => r.Role).WithMany(r => r.UserRoles).HasForeignKey(r => r.RoleId),
                    x => x.HasOne(r => r.User).WithMany(r => r.UserRoles).HasForeignKey(r => r.UserId),
                    x => x.HasKey(t => new { t.UserId, t.RoleId })
                );

            entityBuilder
                .HasOne(x => x.Dept)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            entityBuilder
                .HasOne(x => x.Position)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entityBuilder
                .HasOne(x => x.UserExtension)
                .WithOne(x => x.User).HasForeignKey<User>(x => x.Id);
        }

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<User> HasData(DbContext dbContext, Type dbContextLocator)
        {
            string passwordEncryptKey = "032854df-332d-4c60-905a-fb9487b711e4";
            return new[]
            {
                new User
                {
                    Id=1,
                    UserName="admin",
                    NickName="管理员",
                    PasswordEncryptKey=passwordEncryptKey ,
                    Password=MD5Encryption.Encrypt("admin"+passwordEncryptKey),
                    Avatar="./assets/logo.png",
                    CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311),
                    DeptId=2,
                    PositionId=1
                },
                new User
                {
                    Id=2,
                    UserName="admin2",
                    NickName="管理员2",
                    PasswordEncryptKey=passwordEncryptKey ,
                    Password=MD5Encryption.Encrypt("admin"+passwordEncryptKey),
                    Avatar="./avatars/2.jpeg",
                    CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311),
                    DeptId=2,
                    PositionId=1
                },
                new User
                {
                    Id=3,
                    UserName="admin3",
                    NickName="管理员3",
                    PasswordEncryptKey=passwordEncryptKey ,
                    Password=MD5Encryption.Encrypt("admin"+passwordEncryptKey),
                    Avatar="./avatars/3.jpeg",
                    CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311),
                    DeptId=2,
                    PositionId=1
                },new User
                {
                    Id=4,
                    UserName="admin4",
                    NickName="管理员4",
                    PasswordEncryptKey=passwordEncryptKey ,
                    Password=MD5Encryption.Encrypt("admin"+passwordEncryptKey),
                    Avatar="./avatars/4.jpeg",
                    CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311),
                    DeptId=2,
                    PositionId=1
                },new User
                {
                    Id=5,
                    UserName="admin5",
                    NickName="管理员5",
                    PasswordEncryptKey=passwordEncryptKey ,
                    Password=MD5Encryption.Encrypt("admin"+passwordEncryptKey),
                    Avatar="./avatars/5.jpeg",
                    CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311),
                    DeptId=2,
                    PositionId=1
                },new User
                {
                    Id=6,
                    UserName="admin6",
                    NickName="管理员6",
                    PasswordEncryptKey=passwordEncryptKey ,
                    Password=MD5Encryption.Encrypt("admin"+passwordEncryptKey),
                    Avatar="./avatars/6.jpg",
                    CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311),
                    DeptId=2,
                    PositionId=1
                },new User
                {
                    Id=7,
                    UserName="admin1",
                    NickName="管理员1",
                    PasswordEncryptKey=passwordEncryptKey ,
                    Password=MD5Encryption.Encrypt("admin"+passwordEncryptKey),
                    Avatar="./avatars/1.jpeg",
                    CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311),
                    DeptId=2,
                    PositionId=1
                },
                new User
                {
                    Id=8,
                    UserName="testuser",
                    NickName="测试员",
                    PasswordEncryptKey=passwordEncryptKey,
                    Password=MD5Encryption.Encrypt("testuser"+passwordEncryptKey),
                    Avatar="./avatars/7.jpeg",
                    CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311),
                    DeptId=3,
                    PositionId=2
                }
            };
        }

    }
}