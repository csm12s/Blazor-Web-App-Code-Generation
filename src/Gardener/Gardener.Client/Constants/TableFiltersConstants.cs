// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Common;
using Gardener.Enums;
using System.Linq;

namespace Gardener.Client.Constants
{
    public class TableFiltersConstants
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly static TableFilter<HttpMethodType>[] FunctionMethodFilters = EnumHelper.EnumToList<HttpMethodType>().Select(x => { return new TableFilter<HttpMethodType>() { Text = x.ToString(), Value = x }; }).ToArray();
        /// <summary>
        /// 
        /// </summary>
        public readonly static TableFilter<Gender>[] GenderFilters = EnumHelper.EnumToList<Gender>().Select(x => { return new TableFilter<Gender>() { Text = EnumHelper.GetEnumDescription(x), Value = x }; }).ToArray();
    }
}
