// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Attributes
{
    /// <summary>
    /// 禁用搜索字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public  class DisabledSearchFieldAttribute : Attribute
    {
    }
}
