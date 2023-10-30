// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Attributes;
using Gardener.Audit.Dtos;
using Gardener.Audit.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Base.Entity.Domains
{
    /// <summary>
    /// 操作审计信息
    /// </summary>
    [IgnoreAudit]
    public class AuditOperation : AuditOperationDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator, GardenerAuditDbContextLocator>
    {

        /// <summary>
        /// 审计数据信息集合
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.AuditEntities), ResourceType = typeof(AuditLocalResource))]
        public new ICollection<AuditEntity>? AuditEntities { get; set; }
    }
}
