// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Furion.DatabaseAccessor;

namespace Gardener.Base.Entity
{
    /// <summary>
    /// 资源功能信息配置
    /// </summary>
    public class ResourceFunctionBuilder: IEntityTypeBuilder<ResourceFunction>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<ResourceFunction> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {

            entityBuilder
            .HasKey(t => new { t.ResourceId, t.FunctionId });

            entityBuilder
                .HasOne(pt => pt.Resource)
                .WithMany(p => p.ResourceFunctions)
                .HasForeignKey(pt => pt.ResourceId);

            entityBuilder
                .HasOne(pt => pt.Function)
                .WithMany(t => t.ResourceFunctions)
                .HasForeignKey(pt => pt.FunctionId);
        }
    }
}
