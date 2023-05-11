// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Base
{
    /// <summary>
    /// BaseController，用于API转发
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IBaseController<TDto, TKey> 
        where TDto : class, new()
    {
        #region Gardener API
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
        Task<bool> Deletes(TKey[] ids);
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
        Task<TDto> Get(TKey id);
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<List<TDto>> GetAll();
        /// <summary>
        /// 查询所有可以用的
        /// </summary>
        /// <param name="tenantId">租户编号</param>
        /// <remarks>
        /// 查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)
        /// </remarks>
        /// <returns></returns>
        Task<List<TDto>> GetAllUsable(Guid? tenantId = null);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagedList<TDto>> GetPage(int pageIndex = 1, int pageSize = 10);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TDto> Insert(TDto input);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> Update(TDto input);
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
        Task<PagedList<TDto>> Search(PageRequest request);

        /// <summary>
        /// 此方法用来补充 PagedList 不包含的 TotalItems
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<TDto>> GetList(PageRequest request);

        /// <summary>
        /// 生成种子数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>
        /// 根据搜索条叫生成种子数据
        /// </remarks>
        Task<string> GenerateSeedData(PageRequest request);

        /// <summary>
        /// 导出
        /// </summary>
        /// <remarks>
        /// 导出数据
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> Export(PageRequest request);
        #endregion
    }
}