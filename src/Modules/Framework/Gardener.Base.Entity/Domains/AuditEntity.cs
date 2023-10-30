// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Attributes;
using Gardener.Audit.Dtos;
using Gardener.Audit.Resources;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Base.Entity.Domains
{
    /// <summary>
    /// 审计实体表
    /// </summary>
    [IgnoreAudit]
    public class AuditEntity : AuditEntityDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator, GardenerAuditDbContextLocator>
    {
        /// <summary>
        /// 审计实体表
        /// </summary>
        public AuditEntity()
        {
            this.AuditProperties = new List<AuditProperty>();
        }

        /// <summary>
        /// 操作实体属性集合
        /// </summary>
        [Display(Name = nameof(AuditLocalResource.AuditProperties), ResourceType = typeof(AuditLocalResource))]
        public new ICollection<AuditProperty>? AuditProperties { get; set; }

        /// <summary>
        /// 新值
        /// </summary>
        [NotMapped]
        public PropertyValues CurrentValues { get; set; } = null!;

        /// <summary>
        /// 老值
        /// </summary>
        [NotMapped]
        public PropertyValues? OldValues { get; set; }
    }
}
