﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 审计实体表
    /// </summary>
    [Description("实体审计信息")]
    public class AuditEntity : Entity<Guid>
    {
        /// <summary>
        /// 审计实体表
        /// </summary>
        public AuditEntity() 
        {
            this.CreatedTime = DateTimeOffset.Now;
            this.AuditProperties = new List<AuditProperty>();
        }
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