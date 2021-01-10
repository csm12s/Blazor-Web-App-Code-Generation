// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 需要锁定功能的实体继承该接口
    /// </summary>
    public interface ILockEntity
    {
        /// <summary>
        /// 是否锁定
        /// </summary>
        [DisplayName("是否锁定")]
        public bool IsLocked { get; set; }
    }
}
