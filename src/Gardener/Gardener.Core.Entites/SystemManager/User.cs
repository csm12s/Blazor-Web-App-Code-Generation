// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DataEncryption;
using Gardener.Attributes;
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
    [Description("用户信息")]
    public class User : GardenerEntityBase, IEntitySeedData<User>, IEntityTypeBuilder<User>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required,StringLength(32)]
        [DisplayName("用户名")]
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(50)]
        [DisplayName("昵称")]
        public string NickName { get; set; }
        /// <summary>
        /// 密码加密后的
        /// </summary>
        [Required, StringLength(64)]
        [DisplayName("密码")]
        public string Password { get; set; }
        /// <summary>
        /// 密码加密Key
        /// </summary>
        [Required, StringLength(64)]
        [DisplayName("密码加密KEY")]
        public string PasswordEncryptKey { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(100)]
        [DisplayName("头像")]
        public string Avatar { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(50)]
        [DisplayName("邮箱")]
        public string Email { get; set; }
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
        public string PhoneNumber { get; set; }
        /// <summary>
        /// 手机是否确认
        /// </summary>
        [DisplayName("手机是否确认")]
        public bool PhoneNumberConfirmed { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required,DefaultValue(Gender.Male)]
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
        public UserExtension UserExtension { get; set; }

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
        }

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<User> HasData(DbContext dbContext, Type dbContextLocator)
        {
            string passwordEncryptKey = Guid.NewGuid().ToString();
            return new[]
            {
                new User
                {
                    Id=1,
                    UserName="admin",
                    NickName="管理员",
                    PasswordEncryptKey=passwordEncryptKey ,
                    Password=MD5Encryption.Encrypt("admin"+passwordEncryptKey),
                    Avatar="https://portrait.gitee.com/uploads/avatars/user/100/302533_hgflydream_1578919799.png",
                    CreatedTime=DateTimeOffset.Now
                },
                new User
                {
                    Id=2,
                    UserName="testuser",
                    NickName="测试员",
                    PasswordEncryptKey=passwordEncryptKey,
                    Password=MD5Encryption.Encrypt("testuser"+passwordEncryptKey),
                    Avatar="https://www.baidu.com/img/PCtm_d9c8750bed0b3c7d089fa7d55720d6cf.png",
                    CreatedTime=DateTimeOffset.Now
                }
            };
        }

    }
}