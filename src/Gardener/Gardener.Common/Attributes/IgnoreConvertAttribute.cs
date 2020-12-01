// -----------------------------------------------------------------------------
// 文件头
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
