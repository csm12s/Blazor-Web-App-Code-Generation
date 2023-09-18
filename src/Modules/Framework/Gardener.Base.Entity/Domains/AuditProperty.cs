﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Base.Entity.Domains
{
    /// <summary>
    /// 实体属性审计信息
    /// </summary>
    [Description("属性审计信息")]
    [IgnoreAudit]
    public class AuditProperty : GardenerTenantEntityBase<Guid, MasterDbContextLocator, GardenerMultiTenantDbContextLocator, GardenerAuditDbContextLocator>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Required, MaxLength(100)]
        public string DisplayName { get; set; } = null!;

        /// <summary>
        /// 字段名称
        /// </summary>
        [DisplayName("字段名称")]
        [Required, MaxLength(100)]
        public string FieldName { get; set; } = null!;

        /// <summary>
        /// 旧值
        /// </summary>
        [DisplayName("旧值")]
        public string? OriginalValue { get; set; }

        /// <summary>
        /// 新值
        /// </summary>
        [DisplayName("新值")]
        public string? NewValue { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [DisplayName("数据类型")]
        [MaxLength(100)]
        public string? DataType { get; set; }

        /// <summary>
        /// 实体审计编号  
        /// </summary>
        [DisplayName("实体审计编号")]
        public Guid AuditEntityId { get; set; }

        /// <summary>
        /// 审计实体
        /// </summary>
        public AuditEntity? AuditEntity { get; set; }
    }
}