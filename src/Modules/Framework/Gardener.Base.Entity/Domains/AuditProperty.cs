// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Attributes;
using Gardener.Audit.Dtos;

namespace Gardener.Base.Entity.Domains
{
    /// <summary>
    /// 实体属性审计信息
    /// </summary>
    [IgnoreAudit]
    public class AuditProperty : AuditPropertyDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator, GardenerAuditDbContextLocator>
    {
        /// <summary>
        /// 审计实体
        /// </summary>
        public AuditEntity? AuditEntity { get; set; }
    }
}
