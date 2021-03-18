// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Interfaces;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class LockExtendServiceBase<TEntityDto, TKey> : ApplicationServiceBase<TEntityDto, TKey>, IApplicationServiceBase<TEntityDto, TKey>, IApplicationLockServiceBase<TKey> where TEntityDto : class, new()
    {
        private readonly string controller;
        private readonly IApiCaller apiCaller;
        public LockExtendServiceBase(IApiCaller apiCaller, string controller) : base(apiCaller, controller)
        {
            this.controller = controller;
            this.apiCaller = apiCaller;
        }
        /// <summary>
        /// 锁定
        /// </summary>
        /// <param name="id"></param>
        /// <param name="islocked"></param>
        /// <returns></returns>
        public async Task<bool> Lock(TKey id, bool islocked = true)
        {
            return await apiCaller.PutAsync<object, bool>($"{controller}/{id}/lock/{islocked}");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class LockExtendServiceBase<TEntityDto> : LockExtendServiceBase<TEntityDto, int> where TEntityDto : class, new()
    {
        public LockExtendServiceBase(IApiCaller apiCaller, string controller) : base(apiCaller, controller)
        {
        }
    }
}
