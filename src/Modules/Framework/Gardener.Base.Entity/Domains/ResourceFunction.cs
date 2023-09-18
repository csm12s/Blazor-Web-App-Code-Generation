﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Base.Entity
{
    /// <summary>
    /// 资源功能信息
    /// </summary>
    [Description("资源功能信息")]
    public class ResourceFunction : GardenerEntityBaseNoKeyAndEmpty<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<ResourceFunction, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 资源编号
        /// </summary>
        [Required]
        [DisplayName("资源编号")]
        public Guid ResourceId { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        [DisplayName("资源")]
        public Resource Resource { get; set; } = default!;

        /// <summary>
        /// 功能Id
        /// </summary>
        [Required]
        [DisplayName("功能编号")]
        public Guid FunctionId { get; set; }

        /// <summary>
        /// 功能
        /// </summary>
        [DisplayName("功能")]
        public Function Function { get; set; } = default!;

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.Now;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<ResourceFunction> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
                .HasKey(x => new { x.ResourceId, x.FunctionId });

            entityBuilder
               .HasOne(pt => pt.Resource)
               .WithMany(t => t.ResourceFunctions)
               .HasForeignKey(pt => pt.ResourceId);

            entityBuilder
               .HasOne(pt => pt.Function)
               .WithMany(t => t.ResourceFunctions)
               .HasForeignKey(pt => pt.FunctionId);
        }
    }
}