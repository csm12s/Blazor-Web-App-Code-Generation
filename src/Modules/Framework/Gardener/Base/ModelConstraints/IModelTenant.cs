// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Base
{
    /// <summary>
    /// 租户
    /// </summary>
    public interface IModelTenant : IModelTenantId
    {
        /// <summary>
        /// 租户
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Tenant), ResourceType = typeof(SharedLocalResource))]
        public ITenant? Tenant { get; set; }
    }
}
