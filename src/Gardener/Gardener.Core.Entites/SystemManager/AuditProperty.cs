// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using System;
using System.ComponentModel;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 实体属性审计信息
    /// </summary>
    [Description("属性审计信息")]
    public class AuditProperty : Entity<Guid>
    {
        /// <summary>
        /// 获取或设置 名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 获取或设置 字段
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 获取或设置 旧值
        /// </summary>
        public string OriginalValue { get; set; }

        /// <summary>
        /// 获取或设置 新值
        /// </summary>
        public string NewValue { get; set; }
        
        /// <summary>
        /// 获取或设置 数据类型
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 实体审计编号  
        /// </summary>
        public Guid AuditEntityid { get; set; }

        /// <summary>
        /// 审计实体
        /// </summary>
        public AuditEntity AuditEntity { get; set; }
    }
}
