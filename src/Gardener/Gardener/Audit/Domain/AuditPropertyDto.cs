// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System;
using System.ComponentModel;

namespace Gardener.Audit.Domain
{
    /// <summary>
    /// 属性审计信息
    /// </summary>
    public class AuditPropertyDto : BaseDto<Guid>
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
    }
}
