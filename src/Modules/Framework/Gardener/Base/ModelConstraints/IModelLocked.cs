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
    /// 锁定
    /// </summary>
    public interface IModelLocked
    {
        /// <summary>
        /// 是否锁定
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.IsLocked), ResourceType = typeof(SharedLocalResource))]
        public bool IsLocked { get; set; }
    }
}
