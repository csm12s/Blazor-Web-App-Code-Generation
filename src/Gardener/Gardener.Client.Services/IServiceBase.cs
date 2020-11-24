// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    /// <summary>
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IServiceBase<TEntityDto, TKey> where TEntityDto : class, new()
    {
        /// <summary>
        /// 删除一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResult<bool?>> Delete(TKey id);
        /// <summary>
        /// 删除一条(逻辑删除)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResult<bool?>> FakeDelete(TKey id);
        /// <summary>
        /// 删除多条
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<ApiResult<bool?>> Deletes(TKey[] ids);
        /// <summary>
        /// 删除多条(逻辑删除)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<ApiResult<bool?>> FakeDeletes(TKey[] ids);
        /// <summary>
        /// 查询一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResult<TEntityDto>> Get(TKey id);
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<TEntityDto>>> GetAll();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<ApiResult<PagedList<TEntityDto>>> GetPage(int pageIndex = 1, int pageSize = 10);
        /// <summary>
        /// 新增一条
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ApiResult<TEntityDto>> Insert(TEntityDto input);
        /// <summary>
        /// 更新一条
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ApiResult<bool?>> Update(TEntityDto input);
        /// <summary>
        ///  锁定、解锁
        /// </summary>
        /// <param name="id"></param>
        /// <param name="islocked"></param>
        /// <returns></returns>
        Task<ApiResult<bool?>> Lock(TKey id, bool islocked = true);
    }

    /// <summary>
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    public interface IServiceBase<TEntityDto> : IServiceBase<TEntityDto, int> where TEntityDto : class, new()
    {

    }
}