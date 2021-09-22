// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Attributes;
using Gardener.Audit.Core;
using Gardener.EntityFramwork.DbContexts;
using Gardener.EntityFramwork.Domains;
using System;
using System.ComponentModel;

namespace Gardener.Audit.Dtos
{
    /// <summary>
    /// 实体属性审计信息
    /// </summary>
    [Description("属性审计信息")]
    [IgnoreAudit]
    public class AuditProperty : GardenerEntityBase<Guid, MasterDbContextLocator, GardenerAuditDbContextLocator>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        [DisplayName("字段名称")]
        public string FieldName { get; set; }

        /// <summary>
        /// 旧值
        /// </summary>
        [DisplayName("旧值")]
        public string OriginalValue { get; set; }

        /// <summary>
        /// 新值
        /// </summary>
        [DisplayName("新值")]
        public string NewValue { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [DisplayName("数据类型")]
        public string DataType { get; set; }

        /// <summary>
        /// 实体审计编号  
        /// </summary>
        [DisplayName("实体审计编号")]
        public Guid AuditEntityid { get; set; }

        /// <summary>
        /// 审计实体
        /// </summary>
        public AuditEntity AuditEntity { get; set; }
    }
}
