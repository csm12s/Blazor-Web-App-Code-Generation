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

    TEntity GetFirst(Expression<Func<TEntity, bool>> whereExpression);
    Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> whereExpression);

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
    Task<bool> DeleteAsync(object id);
    Task<bool> DeletesAsync<TKey>(TKey[] ids);
    Task<bool> FakeDeleteAsync(object id);
    Task<bool> FakeDeletesAsync<TKey>(TKey[] ids);
    Task<TEntity> GetByIdAsync(object id);
    List<TEntity> GetAll();
    Task<List<TEntity>> GetAllAsync();
    /// <summary>
    /// 查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)
    /// </summary>
    /// <returns></returns>
    Task<List<TEntity>> GetAllUsableAsync();
    Task<PagedList<TEntity>> GetPageAsync(int pageIndex = 1, int pageSize = 10);
    Task<TEntity> InsertAsync(TEntity input);
    Task<bool> UpdateAsync(TEntity input);
    Task<bool> LockAsync(object id, bool islocked = true);
    Task<PagedList<TEntity>> SearchAsync(PageRequest request);
    Task<string> GenerateSeedDataAsync(PageRequest request);
    #endregion
}
