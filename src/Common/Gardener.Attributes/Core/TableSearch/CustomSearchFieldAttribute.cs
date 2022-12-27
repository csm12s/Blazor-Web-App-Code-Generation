// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;

namespace Gardener.Attributes
{
    /// <summary>
    /// 自定义搜索字段, 例如API Select
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public  class CustomSearchFieldAttribute : Attribute
    {
    }
}
