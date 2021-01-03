// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;
using System;
using System.Collections.Generic;

namespace Gardener.Application.Dtos
{
    /// <summary>
    /// 实体审计信息
    /// </summary>
    public class AuditEntityDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 数据编号
        /// </summary>
        public string DataId { get; set; }
        /// <summary>
        /// 实体名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 实体类型名称
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public OperationType OperationType { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string OperaterId { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string OperaterName { get; set; }
        /// <summary>
        /// 操作ID
        /// </summary>
        public Guid OperationId { get; set; }
        /// <summary>
        /// 获取或设置 操作实体属性集合
        /// </summary>
        public ICollection<AuditPropertyDto> AuditProperties { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }
    }
}
