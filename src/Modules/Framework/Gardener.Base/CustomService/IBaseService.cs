using Furion.DatabaseAccessor;
using Gardener.Authentication.Dtos;
using Microsoft.EntityFrameworkCore;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Gardener.Base;

public interface IBaseService<TEntity> where TEntity : class, new()
{
    #region ORM
    SqlSugarScope GetSugarContext();
    DbContext GetEFContext();
    #endregion

    #region CRUD

    #region Get
    TEntity GetFirst(Expression<Func<TEntity, bool>> whereExpression);
    Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> whereExpression);

    bool Any(Expression<Func<TEntity, bool>> expression);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);

    PagedList<TEntity> GetPage(PageRequest req);
    Task<PagedList<TEntity>> GetPageAsync(PageRequest req);
    List<TEntity> GetList(PageRequest request);
    Task<List<TEntity>> GetListAsync(PageRequest request);
    List<TEntity> GetList(Expression<Func<TEntity, bool>> whereExpression);
    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> whereExpression);
    List<TEntity> GetAll();
    Task<List<TEntity>> GetAllAsync();

    TEntity GetById(object id);
    Task<TEntity> GetByIdAsync(object id);
    #endregion

    #region Insert
    int InsertReturnId(TEntity insertObj);

    long InsertReturnSnowId(TEntity insertObj);

    Task<int> InsertReturnIdAsync(TEntity insertObj);

    Task<long> InsertReturnSnowIdAsync(TEntity insertObj);

    Task<List<long>> InsertReturnSnowIdAsync(List<TEntity> insertObjs);

    TEntity InsertReturnEntity(TEntity insertObj);
    Task<TEntity> InsertReturnEntityAsync(TEntity insertObj);


    #endregion

    #region Update
    bool Update(TEntity item, bool ignoreNullColumn = true);
    Task<bool> UpdateAsync(TEntity item, bool ignoreNullColumn = true);
    bool Update(List<TEntity> list, bool ignoreNullColumn = true);
    Task<bool> UpdateAsync(List<TEntity> list, bool ignoreNullColumn = true);

    bool UpdateExclude(TEntity item, string[] ignoreColumns, bool ignoreNullColumn = true);
    Task<bool> UpdateExcludeAsync(TEntity item, string[] ignoreColumns, bool ignoreNullColumn = true);
    bool UpdateInclude(TEntity item, string[] updateColumns, bool ignoreNullColumn = true);
    Task<bool> UpdateIncludeAsync(TEntity item, string[] updateColumns, bool ignoreNullColumn = true);

    bool SaveOrUpdate(TEntity item, bool ignoreNullColumn = true);
    Task<bool> SaveOrUpdateAsync(TEntity item, bool ignoreNullColumn = true);
    bool SaveOrUpdate(List<TEntity> list, bool ignoreNullColumn = true);
    Task<bool> SaveOrUpdateAsync(List<TEntity> list, bool ignoreNullColumn = true);
    #endregion

    #region Delete
    bool Delete(TEntity item);
    Task<bool> DeleteAsync(TEntity item);
    bool Delete(List<TEntity> list);
    Task<bool> DeleteAsync(List<TEntity> list);

    bool DeleteById(dynamic id);
    Task<bool> DeleteByIdAsync(dynamic id);

    bool DeleteByIds(dynamic[] ids);
    Task<bool> DeleteByIdsAsync(dynamic[] ids);
    Task<bool> FakeDeleteByIdAsync(dynamic id);
    #endregion
    #endregion
}
