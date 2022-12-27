// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Attachment.Enums;
using Gardener.Authentication.Enums;
using Gardener.Common;
using Gardener.Enums;
using System.Linq;

namespace Gardener.Client.Base.Constants
{
    public class TableFiltersConstants
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly static TableFilter<HttpMethod>[] FunctionMethodFilters = EnumHelper.EnumToList<HttpMethod>().Select(x => { return new TableFilter<HttpMethod>() { Text = x.ToString(), Value = x }; }).ToArray();
        /// <summary>
        /// 
        /// </summary>
        public readonly static TableFilter<Gender>[] GenderFilters = EnumHelper.EnumToList<Gender>().Select(x => { return new TableFilter<Gender>() { Text = EnumHelper.GetEnumDescription(x), Value = x }; }).ToArray();
        /// <summary>
        /// 
        /// </summary>
        public readonly static TableFilter<EntityOperateType>[] OperationTypeFilters = EnumHelper.EnumToList<EntityOperateType>().Select(x => { return new TableFilter<EntityOperateType>() { Text = EnumHelper.GetEnumDescription(x), Value = x }; }).ToArray();
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
        public readonly static TableFilter<LoginClientType>[] LoginClientTypeFilters = EnumHelper.EnumToList<LoginClientType>().Select(x => { return new TableFilter<LoginClientType>() { Text = EnumHelper.GetEnumDescription(x), Value = x }; }).ToArray();/// <summary>
        /// <summary>
        /// 
        /// </summary>
        public readonly static TableFilter<IdentityType>[] IdentityTypeFilters = EnumHelper.EnumToList<IdentityType>().Select(x => { return new TableFilter<IdentityType>() { Text = EnumHelper.GetEnumDescription(x), Value = x }; }).ToArray();
    }
}
