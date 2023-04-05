// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;
using System;

namespace Gardener.Authorization.Dtos
{
    /// <summary>
    /// api终结点
    /// </summary>
    public class ApiEndpoint
    {
        
        /// <summary>
        /// 唯一键
        /// </summary>
        public string Key { get; set; } = null!;

        /// <summary>
        /// API路由地址
        /// </summary>
        public string Path { get; set; } = null!;

        /// <summary>
        /// 接口请求方法
        /// </summary>
        public HttpMethod Method { get; set; }

        /// <summary>
        /// 唯一id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 启用审计
        /// </summary>
        public bool EnableAudit { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string Group { get; set; } = null!;

        /// <summary>
        /// 服务
        /// </summary>
        public string Service { get; set; } = null!;

        /// <summary>
        /// 概要
        /// </summary>
        public string? Summary { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }

    }
}
