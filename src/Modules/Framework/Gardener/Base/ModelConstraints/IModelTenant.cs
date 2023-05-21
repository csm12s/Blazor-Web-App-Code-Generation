// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.Base
{
    /// <summary>
    /// 租户
    /// </summary>
    public interface  IModelTenant: IModelTenantId
    {
        /// <summary>
        /// 租户
        /// </summary>
        [DisplayName("Tenant")]
        public ITenant? Tenant { get; set; }
    }
}
