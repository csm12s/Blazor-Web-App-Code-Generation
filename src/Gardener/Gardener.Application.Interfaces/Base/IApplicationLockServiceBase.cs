// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Gardener.Application.Interfaces
{
    /// <summary>
    /// 定义了基础方法
    /// 方法包括：锁定 
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationLockServiceBase<TKey>
    {
        /// <summary>
        ///  锁定、解锁
        /// </summary>
        /// <param name="id"></param>
        /// <param name="islocked"></param>
        /// <returns></returns>
        Task<bool> Lock(TKey id, bool islocked = true);
    }
}
