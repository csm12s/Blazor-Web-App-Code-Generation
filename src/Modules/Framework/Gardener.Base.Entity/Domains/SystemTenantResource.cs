// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Base.Entity
{
    /// <summary>
    /// 租户资源信息
    /// </summary>
    [Description("租户资源信息")]
    public class SystemTenantResource : GardenerTenantEntityBaseNoKey<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<SystemTenantResource, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [Required]
        [DisplayName("租户编号")]
        public new Guid TenantId { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        [DisplayName("Tenant")]
        [NotMapped]
        public new SystemTenant Tenant { get; set; } = default!;

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
        /// 配置
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Configure(EntityTypeBuilder<SystemTenantResource> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
                .HasKey(x => new { x.TenantId, x.ResourceId });

            entityBuilder
               .HasOne(pt => pt.Tenant)
               .WithMany(t => t.TenantResources)
               .HasForeignKey(pt => pt.TenantId);

            entityBuilder
               .HasOne(pt => pt.Resource)
               .WithMany(t => t.TenantResources)
               .HasForeignKey(pt => pt.ResourceId);
        }
    }
}
