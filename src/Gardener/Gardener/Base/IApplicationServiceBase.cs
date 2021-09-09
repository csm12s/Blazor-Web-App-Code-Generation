// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.EntityFramwork.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Base
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
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(TKey id);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> Deletes(TKey [] ids);
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> FakeDelete(TKey id);
        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> FakeDeletes(TKey[] ids);
        /// <summary>
        /// 查询
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
        /// 查询所有可以用的
        /// </summary>
        /// <remarks>
        /// 查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)
        /// </remarks>
        /// <returns></returns>
        Task<List<TEntityDto>> GetAllUsable();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagedList<TEntityDto>> GetPage(int pageIndex = 1, int pageSize = 10);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TEntityDto> Insert(TEntityDto input);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> Update(TEntityDto input);
        /// <summary>
        ///  锁定、解锁
        /// </summary>
        /// <param name="id"></param>
        /// <param name="islocked"></param>
        /// <returns></returns>
        Task<bool> Lock(TKey id, bool islocked = true);

        /// <summary>
        /// 搜索
        /// </summary>
        /// <remarks>
        /// 搜索功能数据
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedList<TEntityDto>> Search(PageRequest request);
    }
}