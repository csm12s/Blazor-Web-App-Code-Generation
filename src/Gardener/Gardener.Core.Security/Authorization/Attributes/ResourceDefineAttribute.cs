using Furion.DependencyInjection;
using System;

namespace Microsoft.AspNetCore.Authorization
{
    /// <summary>
    /// 资源定义特性
    /// </summary>
    [SkipScan, AttributeUsage(AttributeTargets.Method)]
    public class ResourceDefineAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="resourceId"></param>
        public ResourceDefineAttribute(string resourceId)
        {
            ResourceId = resourceId;
        }

        /// <summary>
        /// 资源Id，必须是唯一的
        /// </summary>
        public string ResourceId { get; set; }
    }
}