// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System;
using System.ComponentModel;

namespace Gardener.Audit.Dtos
{
    /// <summary>
    /// 属性审计信息
    /// </summary>
    public class AuditPropertyDto : BaseDto<Guid>
    {

        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("DisplayName")]
        public string DisplayName { get; set; } = null!;

        /// <summary>
        /// 字段名称
        /// </summary>
        [DisplayName("FieldName")]
        public string FieldName { get; set; } = null!;

        /// <summary>
        /// 旧值
        /// </summary>
        [DisplayName("OriginalValue")]
        public string? OriginalValue { get; set; }

        /// <summary>
        /// 新值
        /// </summary>
        [DisplayName("NewValue")]
        public string? NewValue { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [DisplayName("DataType")]
        public string DataType { get; set; } = null!;

        /// <summary>
        /// 实体审计编号  
        /// </summary>
        [DisplayName("AuditEntityid")]
        public Guid AuditEntityid { get; set; }
    }
}
