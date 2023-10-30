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
    /// 资源表
    /// </summary>
    public class Resource : ResourceDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<Resource, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 资源信息
        /// </summary>
        public Resource()
        {
            ResourceFunctions = new List<ResourceFunction>();
        }

        /// <summary>
        /// 父级
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.Parent), ResourceType = typeof(SystemManagerResource))]
        public Resource? Parent { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        [Display(Name = nameof(SystemManagerResource.Children), ResourceType = typeof(SystemManagerResource))]
        public new ICollection<Resource>? Children { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<ResourceFunction> ResourceFunctions { get; set; } = new List<ResourceFunction>();

        /// <summary>
        /// 租户资源关系
        /// </summary>
        public List<SystemTenantResource> TenantResources { get; set; } = new List<SystemTenantResource>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<Resource> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasIndex(x => x.Key);
        }
    }
}