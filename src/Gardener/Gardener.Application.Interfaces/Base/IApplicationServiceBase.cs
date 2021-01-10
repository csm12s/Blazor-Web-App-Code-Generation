// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Application.Interfaces
{
    /// <summary>
    /// 定义了基础方法
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IApplicationServiceBase<TEntityDto, TKey> where TEntityDto : class, new()
    {
        /// <summary>
        /// 删除一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(TKey id);
        /// <summary>
        /// 删除多条
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> Deletes(TKey [] ids);
        /// <summary>
        /// 删除一条(逻辑删除)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> FakeDelete(TKey id);
        /// <summary>
        /// 删除多条(逻辑删除)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> FakeDeletes(TKey[] ids);
        /// <summary>
        /// 查询一条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntityDto> Get(TKey id);
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<List<TEntityDto>> GetAll();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagedList<TEntityDto>> GetPage(int pageIndex = 1, int pageSize = 10);
        /// <summary>
        /// 新增一条
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TEntityDto> Insert(TEntityDto input);
        /// <summary>
        /// 更新一条
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> Update(TEntityDto input);
    }
}