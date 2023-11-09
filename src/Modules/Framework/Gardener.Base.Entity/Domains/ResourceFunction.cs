// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Base.Entity
{
    /// <summary>
    /// 资源功能信息
    /// </summary>
    public class ResourceFunction : ResourceFunctionDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<ResourceFunction, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 权限
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.Resource), ResourceType = typeof(SystemManagerResource))]
        public Resource Resource { get; set; } = default!;

        /// <summary>
        /// 功能
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.Function), ResourceType = typeof(SystemManagerResource))]
        public Function Function { get; set; } = default!;

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
