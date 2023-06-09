// -----------------------------------------------------------------------------
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
    /// 操作审计信息
    /// </summary>
    [Description("操作审计信息")]
    public class AuditOperationDto : TenantBaseDto<Guid>
    {
        /// <summary>
        /// 资源名
        /// </summary>
        [DisplayName("ResourceName")]
        public string ResourceName { get; set; } = null!;
        /// <summary>
        /// 资源编号
        /// </summary>
        [DisplayName("ResourceId")]
        public Guid ResourceId { get; set; }
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
        /// 访问IP
        /// </summary>
        [DisplayName("IP")]
        public string? Ip { get; set; }
        /// <summary>
        /// UserAgent
        /// </summary>
        [DisplayName("UserAgent")]
        public string? UserAgent { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        [DisplayName("Path")]
        public string? Path { get; set; }
        /// <summary>
        /// 请求方法
        /// </summary>
        [DisplayName("Method")]
        public HttpMethod Method { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        [DisplayName("Parameters")]
        public string? Parameters { get; set; }
        /// <summary>
        /// 关联数据审计
        /// </summary>
        public List<AuditEntityDto>? AuditEntitys { get; set; }
    }
}
