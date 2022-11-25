using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using SqlSugar;
using System.Linq.Expressions;

namespace Gardener.Base;

public interface IBaseService<TEntity>
    where TEntity : class, IPrivateEntity, new()
{
    #region ORM
    SqlSugarScope GetSugarContext();
    DbContext GetEFContext();
    IPrivateReadableRepository<TEntity> GetReadableRepository();
    #endregion

    #region Custom CRUD
    List<TEntity> GetList(PageRequest request);

    /// <summary>
    /// Get List
    /// In: Where + Order By
    /// Out: list
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<List<TEntity>> GetListAsync(PageRequest request);
    List<TEntity> GetList(Expression<Func<TEntity, bool>> whereExpression);
    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> whereExpression);
    PagedList<TEntity> GetPage(PageRequest req);
    Task<PagedList<TEntity>> GetPageAsync(PageRequest request);
    #endregion
    #region Gardener CRUD
    Task<bool> Delete(object id);
    Task<bool> Deletes<TKey>(TKey[] ids);
    Task<bool> FakeDelete(object id);
    Task<bool> FakeDeletes<TKey>(TKey[] ids);
    Task<TEntity> Get(object id);
    Task<List<TEntity>> GetAll();
    /// <summary>
    /// 查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)
    /// </summary>
    /// <returns></returns>
    Task<List<TEntity>> GetAllUsable();
    Task<PagedList<TEntity>> GetPage(int pageIndex = 1, int pageSize = 10);
    Task<TEntity> Insert(TEntity input);
    Task<bool> Update(TEntity input);
    Task<bool> Lock(object id, bool islocked = true);
    Task<PagedList<TEntity>> Search(PageRequest request);
    Task<string> GenerateSeedData(PageRequest request);
    #endregion
}
