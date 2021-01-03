// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;
using System;

namespace Gardener.Application.Dtos
{
    /// <summary>
    /// 操作审计信息
    /// </summary>
    public class AuditOperationDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 资源名
        /// </summary>
        public string ResourceName { get; set; }
        /// <summary>
        /// 资源编号
        /// </summary>
        public Guid ResourceId { get; set; }
        /// <summary>
        /// 用户标识
        /// </summary>
        public string OperaterId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string OperaterName { get; set; }
        /// <summary>
        /// 访问IP
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// UserAgent
        /// </summary>
        public string UserAgent { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 请求方法
        /// </summary>
        public HttpMethodType Method { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        public string Parameters { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }
    }
}
