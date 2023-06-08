﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Enums;
using Gardener.Base;
using Gardener.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Gardener.Audit.Dtos
{
    /// <summary>
    /// 实体审计信息
    /// </summary>
    [Description("实体审计信息")]
    public class AuditEntityDto : TenantBaseDto<Guid>
    {
        /// <summary>
        /// 数据编号
        /// </summary>
        [DisplayName("DataId")]
        public string DataId { get; set; } = null!;
        /// <summary>
        /// 实体名称
        /// </summary>
        [DisplayName("Name")]
        public string Name { get; set; } = null!;
        /// <summary>
        /// 实体类型名称
        /// </summary>
        [DisplayName("TypeName")]
        public string TypeName { get; set; } = null!;
        /// <summary>
        /// 操作类型
        /// </summary>
        [DisplayName("OperationType")]
        public EntityOperateType OperationType { get; set; }
        /// <summary>
        /// 操作者编号
        /// </summary>
        [DisplayName("OperaterId")]
        public string OperaterId { get; set; } = null!;
        /// <summary>
        /// 操作者名称
        /// </summary>
        [DisplayName("OperaterName")]
        public string OperaterName { get; set; } = null!;
        /// <summary>
        /// 操作者类型
        /// </summary>
        [DisplayName("OperaterType")]
        public IdentityType OperaterType { get; set; }
        /// <summary>
        /// 操作ID
        /// </summary>
        [DisplayName("OperationId")]
        public Guid OperationId { get; set; }
        /// <summary>
        /// 获取或设置 操作实体属性集合
        /// </summary>
        public ICollection<AuditPropertyDto>? AuditProperties { get; set; }
    }
}
