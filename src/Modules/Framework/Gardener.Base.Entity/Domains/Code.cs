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
using System.ComponentModel.DataAnnotations;

namespace Gardener.Base.Entity
{
    /// <summary>
    /// 字典信息
    /// </summary>
    public class Code : CodeDto, IEntityBase, IEntityTypeBuilder<Code>
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.CodeType), ResourceType = typeof(SystemManagerResource))]
        public new CodeType CodeType { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<Code> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasOne(x => x.CodeType).WithMany(x => x.Codes).HasForeignKey(x => x.CodeTypeId);
        }
    }
}
