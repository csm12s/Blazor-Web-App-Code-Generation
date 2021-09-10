// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel;

#nullable disable

namespace Gardener.Authorization.Domains
{
    /// <summary>
    /// 用户扩展信息表
    /// </summary>
    [Description("用户扩展信息")]
    public class UserExtension : IEntity<MasterDbContextLocator>, IEntityTypeBuilder<UserExtension, MasterDbContextLocator>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [DisplayName("用户编号")]
        public int UserId { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        [DisplayName("QQ")]
        public string QQ { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        [DisplayName("微信")]
        public string WeChat { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        [DisplayName("城市编号")]
        public int? CityId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [DisplayName("更新时间")]
        public DateTimeOffset? UpdatedTime { get; set; }
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
            entityBuilder.HasKey(e => e.UserId).HasName("PRIMARY");
        }

    }
}

