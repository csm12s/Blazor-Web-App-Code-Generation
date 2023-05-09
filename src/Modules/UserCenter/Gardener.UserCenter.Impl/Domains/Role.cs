// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base;
using Gardener.Base.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Impl.Domains
{
    /// <summary>
    /// 角色表
    /// </summary>
    [Description("角色信息")]
    public class Role : GardenerTenantEntityBase<int, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntitySeedData<Role, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<Role, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 角色表
        /// </summary>
        public Role()
        {
            Users = new List<User>();
            UserRoles = new List<UserRole>();
            RoleResources=new List<RoleResource>();
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        [MaxLength(100), Required]
        [DisplayName("名称")]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 角色描述
        /// </summary>
        [MaxLength(500), Required]
        [DisplayName("备注")]
        public string Remark { get; set; } = null!;

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
        public ICollection<User> Users { get; set; }= new List<User>();

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<RoleResource> RoleResources { get; set; }=new List<RoleResource>();


        /// <summary>
        /// 配置多对多关系
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<Role> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {

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
                    Id=1,Name="超级管理员",Remark="拥有所有权限",IsSuperAdministrator=true,CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311)
                },
                new Role
                {
                    Id=2,Name="浏览者",Remark="只能浏览",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311)
                }
            };
        }
    }
}