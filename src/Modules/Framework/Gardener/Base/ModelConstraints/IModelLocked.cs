// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

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
        [DisplayName("IsLocked")]
        public bool IsLocked { get; set; }
    }
}
