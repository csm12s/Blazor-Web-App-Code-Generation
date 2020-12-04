// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;

namespace Gardener.Common.Attributes
{
    /// <summary>
    /// Enum 转换集合时忽略
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class IgnoreConvertAttribute : Attribute
    {
    }
}
