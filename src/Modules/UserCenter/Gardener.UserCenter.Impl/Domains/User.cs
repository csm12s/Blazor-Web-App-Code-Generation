// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DataEncryption;
using Gardener.Base.Entity;
using Gardener.Base.Resources;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Impl.Domains
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class User : UserDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntitySeedData<User, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<User, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 密码加密Key
        /// </summary>
        [Display(Name = nameof(UserCenterResource.PasswordEncryptKey), ResourceType = typeof(UserCenterResource))]
        [Required(ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.RequiredValidationError))]
        [MaxLength(64, ErrorMessageResourceType = typeof(ValidateErrorMessagesResource), ErrorMessageResourceName = nameof(ValidateErrorMessagesResource.StringMaxValidationError))]
        public string PasswordEncryptKey { get; set; } = null!;
       
        /// <summary>
        /// 多对多
        /// </summary>
        public new ICollection<Role> Roles { get; set; } = new List<Role>();

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();

        /// <summary>
        /// 用户扩展信息
        /// </summary>
        public new UserExtension? UserExtension { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public new Dept? Dept { get; set; }
     
        /// <summary>
        /// 岗位
        /// </summary>
        public new Position? Position;

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
                },
                new User
                {
                    Id=9,
                    UserName="dongfangcaifu",
                    NickName="东方财富播报",
                    PasswordEncryptKey=passwordEncryptKey,
                    Password=MD5Encryption.Encrypt("dongfangcaifu"+passwordEncryptKey),
                    Avatar="./avatars/8.jpeg",
                    CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311),
                    DeptId=2,
                    PositionId=1
                },
                new User
                {
                    Id=10,
                    UserName="zuhu1_user1",
                    NickName="租户1用户1",
                    PasswordEncryptKey=passwordEncryptKey,
                    Password=MD5Encryption.Encrypt("zuhu1_user1"+passwordEncryptKey),
                    Avatar="./avatars/9.jpeg",
                    CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311),
                    DeptId=2,
                    PositionId=1,
                    TenantId=Guid.Parse("710148B3-0C80-48A2-8F57-4B863BE9859F")
                },
                new User
                {
                    Id=11,
                    UserName="zuhu2_user1",
                    NickName="租户2用户1",
                    PasswordEncryptKey=passwordEncryptKey,
                    Password=MD5Encryption.Encrypt("zuhu2_user1"+passwordEncryptKey),
                    Avatar="./avatars/10.jpeg",
                    CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311),
                    DeptId=2,
                    PositionId=1,
                    TenantId=Guid.Parse("F416B514-04C8-40CA-91A4-07C5BBF9C8C6")
                },
                new User
                {
                    Id=12,
                    UserName="zuhu2_user2",
                    NickName="租户2用户2",
                    PasswordEncryptKey=passwordEncryptKey,
                    Password=MD5Encryption.Encrypt("zuhu2_user2"+passwordEncryptKey),
                    Avatar="./avatars/11.jpeg",
                    CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311),
                    DeptId=2,
                    PositionId=1,
                    TenantId=Guid.Parse("F416B514-04C8-40CA-91A4-07C5BBF9C8C6")
                }
            };
        }

    }
}