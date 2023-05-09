// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Impl.Domains
{
    /// <summary>
    /// 用户扩展信息表
    /// </summary>
    [Description("用户扩展信息")]
    public class UserExtension : GardenerTenantEntityBaseNoKey, IEntityTypeBuilder<UserExtension>, IEntitySeedData<UserExtension>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [DisplayName("用户编号")]
        [Key]
        public int UserId { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        [DisplayName("QQ")]
        public string? QQ { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        [DisplayName("微信")]
        public string? WeChat { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        [DisplayName("城市编号")]
        public int? CityId { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public User User { get; set; } = null!;

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<UserExtension> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            //entityBuilder.HasKey(e => e.UserId).HasName("PRIMARY");
            entityBuilder
                .HasOne(x => x.User)
                .WithOne(x => x.UserExtension)
                .HasForeignKey<UserExtension>(x => x.UserId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<UserExtension> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
                new UserExtension()
                    {
                        UserId=8,
                        QQ="123456"
                    }
            };
        }
    }
}

