// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Gardener.Application.Dtos
{
    /// <summary>
    /// 操作审计信息
    /// </summary>
    public class AuditOperationDto : BaseDto<Guid>
    {
        /// <summary>
        /// 资源名
        /// </summary>
        [DisplayName("资源名")]
        public string ResourceName { get; set; }
        /// <summary>
        /// 资源编号
        /// </summary>
        [DisplayName("资源编号")]
        public Guid ResourceId { get; set; }
        /// <summary>
        /// 用户标识
        /// </summary>
        [DisplayName("操作人编号")]
        public string OperaterId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [DisplayName("操作人")]
        public string OperaterName { get; set; }
        /// <summary>
        /// 访问IP
        /// </summary>
        [DisplayName("IP")]
        public string Ip { get; set; }
        /// <summary>
        /// UserAgent
        /// </summary>
        [DisplayName("UserAgent")]
        public string UserAgent { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        [DisplayName("请求地址")]
        public string Path { get; set; }
        /// <summary>
        /// 请求方法
        /// </summary>
        [DisplayName("请求方法")]
        public HttpMethod Method { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        [DisplayName("请求参数")]
        public string Parameters { get; set; }
        /// <summary>
        /// 关联数据审计
        /// </summary>
        public List<AuditEntityDto> AuditEntitys { get; set; }
    }
}
