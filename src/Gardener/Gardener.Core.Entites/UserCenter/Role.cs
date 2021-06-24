// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 角色表
    /// </summary>
    [Description("角色信息")]
    public class Role : GardenerEntityBase, IEntitySeedData<Role>, IEntityTypeBuilder<Role>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [MaxLength(100), Required]
        [DisplayName("名称")]
        public string Name { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        [MaxLength(500), Required]
        [DisplayName("备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 是否是超级管理员
        /// 超级管理员拥有所有权限
        /// </summary>
        [DisplayName("是否是超级管理员")]
        public bool IsSuperAdministrator { get; set; }

        /// <summary>
        /// 是否是默认权限
        /// 注册用户时默认设置
        /// </summary>
        [DisplayName("是否是默认角色")]
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
        /// 配置多对多关系
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<Role> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
                .HasMany(x => x.Resources)
                .WithMany(x => x.Roles)
                .UsingEntity<RoleResource>(
                    x => x.HasOne(r => r.Resource).WithMany(r => r.RoleResources).HasForeignKey(r => r.ResourceId),
                    x => x.HasOne(r => r.Role).WithMany(r => r.RoleResources).HasForeignKey(r => r.RoleId),
                    x => x.HasKey(t => new { t.RoleId, t.ResourceId })
                );
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