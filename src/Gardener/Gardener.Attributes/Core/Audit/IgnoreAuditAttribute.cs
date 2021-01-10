// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;

namespace Gardener.Attributes
{
    /// <summary>
    /// 忽略操作审计、数据审计
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public class IgnoreAuditAttribute : Attribute
    {
    }
}
