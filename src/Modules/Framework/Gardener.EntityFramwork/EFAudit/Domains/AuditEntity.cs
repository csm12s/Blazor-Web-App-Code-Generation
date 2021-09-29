// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Attributes;
using Gardener.EntityFramwork.DbContexts;
using Gardener.EntityFramwork.Domains;
using Gardener.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.EntityFramwork.Audit.Domains
{
    /// <summary>
    /// 审计实体表
    /// </summary>
    [Description("实体审计信息")]
    [IgnoreAudit]
    public class AuditEntity : GardenerEntityBase<Guid, MasterDbContextLocator, GardenerAuditDbContextLocator>
    {
        /// <summary>
        /// 审计实体表
        /// </summary>
        public AuditEntity() 
        {
            this.AuditProperties = new List<AuditProperty>();
        }
        /// <summary>
        /// 数据编号
        /// </summary>
        [DisplayName("数据编号")]
        public string DataId { get; set; }
        /// <summary>
        /// 实体名称
        /// </summary>
        [DisplayName("实体名称")]
        public string Name { get; set; }
        /// <summary>
        /// 实体类型名称
        /// </summary>
        [DisplayName("实体类型名称")]
        public string TypeName { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        [DisplayName("操作类型")]
        public EntityOperationType OperationType { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        [DisplayName("操作人编号")]
        public string OperaterId { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        [DisplayName("操作人")]
        public string OperaterName { get; set; }
        /// <summary>
        /// 操作ID
        /// </summary>
        [DisplayName("操作审计编号")]
        public Guid OperationId { get; set; }
        /// <summary>
        /// 操作实体属性集合
        /// </summary>
        public ICollection<AuditProperty> AuditProperties { get; set; }
        /// <summary>
        /// 新值
        /// </summary>
        [NotMapped]
        public PropertyValues CurrentValues { get; set; }
        /// <summary>
        /// 老值
        /// </summary>
        [NotMapped]
        public PropertyValues OldValues { get; set; }
    }
}
