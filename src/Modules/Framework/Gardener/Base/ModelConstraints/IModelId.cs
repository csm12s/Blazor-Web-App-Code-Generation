// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.Base
{
    /// <summary>
    /// 编号
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IModelId<TKey>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [DisplayName("Id")]
        public TKey Id { get; set; }
    }
}
