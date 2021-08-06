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
        /// <summary>
        /// 
        /// </summary>
        public readonly static TableFilter<OperationType>[] OperationTypeFilters = EnumHelper.EnumToList<OperationType>().Select(x => { return new TableFilter<OperationType>() { Text = EnumHelper.GetEnumDescription(x), Value = x }; }).ToArray();
        /// <summary>
        /// 
        /// </summary>
        public readonly static TableFilter<AttachmentBusinessType?>[] AttachmentBusinessTypeFilters = EnumHelper.EnumToList<AttachmentBusinessType>().Select(x => { return new TableFilter<AttachmentBusinessType?>() { Text = EnumHelper.GetEnumDescription(x), Value = x }; }).ToArray();
        /// <summary>
        /// 
        /// </summary>
        public readonly static TableFilter<AttachmentFileType?>[] AttachmentFileTypeFilters = EnumHelper.EnumToList<AttachmentFileType>().Select(x => { return new TableFilter<AttachmentFileType?>() { Text = EnumHelper.GetEnumDescription(x), Value = x }; }).ToArray();
        /// <summary>
        /// 
        /// </summary>
        public readonly static TableFilter<LoginClientType>[] LoginClientTypeFilters = EnumHelper.EnumToList<LoginClientType>().Select(x => { return new TableFilter<LoginClientType>() { Text = EnumHelper.GetEnumDescription(x), Value = x }; }).ToArray();
    }
}
