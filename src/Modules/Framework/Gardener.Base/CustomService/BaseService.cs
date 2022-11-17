using Furion;
using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Gardener.Common;
using Gardener.Enums;
using Mapster;
using Microsoft.EntityFrameworkCore;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Gardener.Base;

#region BaseService with EF + Sugar Repository
public abstract class BaseService
    <TEntity, TDbContextLocator> :
    IDynamicApiController,
    IBaseService<TEntity> where TEntity : class, IPrivateEntity, new()
    where TDbContextLocator : class, IDbContextLocator
{

    #region Init

    private readonly string[] UpdateIgnoreColumns = new string[]
    {
        nameof(GardenerEntityBase.CreatedTime),
        nameof(GardenerEntityBase.CreatorId),
        nameof(GardenerEntityBase.CreatorIdentityType),
    };

    /// <summary>
    /// EF Repository
    /// </summary>
    public readonly IRepository<TEntity, TDbContextLocator> _repository;

    /// <summary>
    /// Sugar Repository
    /// </summary>
    public readonly SqlSugarRepository<TEntity> _sugarRepository;

    protected BaseService(IRepository<TEntity, TDbContextLocator> repository,
        SqlSugarRepository<TEntity> sugarRepository)
    {
        _repository = repository;
        _sugarRepository = sugarRepository;
    }

    protected BaseService(IRepository<TEntity> repository,
        SqlSugarRepository<TEntity> sugarRepository)
    {
        _repository = (IRepository<TEntity, TDbContextLocator>)repository;
        _sugarRepository = sugarRepository;
    }

    #endregion

    #region ORM
    public DbContext GetEFContext()
    {
        return _repository.Context;
    }
    public IPrivateReadableRepository<TEntity> GetReadableRepository()
    {
        return _repository;
    }


    public SqlSugarScope GetSugarContext()
    {
        return _sugarRepository.Context;
    }
    #endregion

    #region CRUD
    #region Get
    public virtual bool Any(Expression<Func<TEntity, bool>> whereExpression)
    {
        return _repository.Any(whereExpression);
        //return _sugarRepository.Context.Queryable<TEntity>().Where(whereExpression).Any();
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return await _repository.AnyAsync(whereExpression);
        //return await _sugarRepository.Context.Queryable<TEntity>().Where(whereExpression).AnyAsync();
    }

    public virtual TEntity GetById(object id)
    {
        return _repository.Find(id);
        //return _sugarRepository.Context.Queryable<TEntity>().InSingle(id);
    }

    public virtual async Task<TEntity> GetByIdAsync(object id)
    {
        return await _repository.FindAsync(id);
        //return await _sugarRepository.Context.Queryable<TEntity>().InSingleAsync(id);
    }

    public virtual TEntity GetFirst(Expression<Func<TEntity, bool>> whereExpression)
    {
        return _sugarRepository.Context.Queryable<TEntity>().First(whereExpression);
    }

    public virtual async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return await _sugarRepository.Context.Queryable<TEntity>().FirstAsync(whereExpression);
    }

    public virtual async Task<List<TEntity>> GetListAsync()
    {
        return await _repository.AsQueryable().ToListAsync();
        //return await _sugarRepository.Context.Queryable<TEntity>().ToListAsync();
    }

    public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> whereExpression)
    {
        return _repository.AsQueryable().Where(whereExpression).ToList();
        //return _sugarRepository.Context.Queryable<TEntity>().Where(whereExpression).ToList();
    }

    public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return await _repository.AsQueryable().Where(whereExpression).ToListAsync();
        //return await _sugarRepository.Context.Queryable<TEntity>().Where(whereExpression).ToListAsync();
    }

    public List<TEntity> GetAll()
    {
        return _repository.AsQueryable().ToList();
        //return _sugarRepository.Context.Queryable<TEntity>().ToList();
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _repository.AsQueryable().ToListAsync();
        //return await _sugarRepository.Context.Queryable<TEntity>().ToListAsync();
    }

    #region Get Page

    /// <summary>
    /// In: Where + Order By
    /// Out: list
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public List<TEntity> GetList(PageRequest request)
    {
        // Where - IsDeleted
        if (typeof(TEntity).ExistsProperty(nameof(GardenerEntityBase.IsDeleted)))
        {
            FilterGroup defaultFilterGroup = new FilterGroup();
            defaultFilterGroup.AddRule(new FilterRule(nameof(GardenerEntityBase.IsDeleted), false, FilterOperate.Equal));
            request.FilterGroups.Add(defaultFilterGroup);
        }

        // Where
        IDynamicFilterService filterService = Furion.App.GetService<IDynamicFilterService>();
        Expression<Func<TEntity, bool>> expression = filterService.GetExpression<TEntity>(request.FilterGroups);

        // EF:
        IQueryable<TEntity> queryable = _repository.AsQueryable(false);
        return queryable
            .Where(expression)
            .OrderConditions(request.OrderConditions.ToArray())
            .ToList();

        // Sugar:
        //IQueryable<TEntity> queryable = (IQueryable<TEntity>)_sugarRepository.Context.Queryable<TEntity>();
        //return queryable
        //    .Where(expression) // Where
        //    .OrderConditions(request.OrderConditions.ToArray()) // Order by
        //    .ToList();
    }

    /// <summary>
    /// In: Where + Order By
    /// Out: list
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<List<TEntity>> GetListAsync(PageRequest request)
    {
        // Where - IsDeleted
        if (typeof(TEntity).ExistsProperty(nameof(GardenerEntityBase.IsDeleted)))
        {
            FilterGroup defaultFilterGroup = new FilterGroup();
            defaultFilterGroup.AddRule(new FilterRule(nameof(GardenerEntityBase.IsDeleted), false, FilterOperate.Equal));
            request.FilterGroups.Add(defaultFilterGroup);
        }

        // Where
        IDynamicFilterService filterService = Furion.App.GetService<IDynamicFilterService>();
        Expression<Func<TEntity, bool>> expression = filterService.GetExpression<TEntity>(request.FilterGroups);

        // EF:
        IQueryable<TEntity> queryable = _repository.AsQueryable(false);
        return await queryable
            .Where(expression)
            .OrderConditions(request.OrderConditions.ToArray())
            .ToListAsync();

        // Sugar:
        //var listAll = await GetAllAsync();
        //IQueryable<TEntity> queryable = listAll.AsQueryable();
        //// TODO: If null in DB, skip where expression?
        //// 数据库字段为null这里会报错
        //try
        //{
        //    var list = queryable
        //        .Where(expression) // Where
        //        .OrderConditions(request.OrderConditions.ToArray()) // Order by
        //        .ToList();

        //    return list;
        //}
        //catch (Exception ex)
        //{
        //    // TODO: 这里不能抛出异常
        //    throw Oops.Oh(ExceptionCode.Search_Error_DB_Field_Is_Null);
        //}
    }
    #endregion

    public virtual Base.PagedList<TEntity> GetPage(PageRequest request)
    {
        var list = GetList(request);
        return list.ToPageList(request);
    }

    public virtual async Task<Base.PagedList<TEntity>> GetPageAsync(PageRequest request)
    {
        var list = await GetListAsync(request);
        return list.ToPageList(request);
    }
    #endregion

    //TODO:
    #region Delete
    public virtual bool Delete(TEntity item)
    {
        throw new NotImplementedException();
        //return _sugarRepository.Context.Deleteable(item).ExecuteCommand() > 0;
    }

    public virtual async Task<bool> DeleteAsync(TEntity item)
    {
        throw new NotImplementedException();
        //return await _sugarRepository.Context.Deleteable<TEntity>().Where(item).ExecuteCommandAsync() > 0;
    }

    public virtual bool Delete(List<TEntity> list)
    {
        return _sugarRepository.Context.Deleteable<TEntity>().Where(list).ExecuteCommand() > 0;
    }

    public virtual async Task<bool> DeleteAsync(List<TEntity> list)
    {
        return await _sugarRepository.Context.Deleteable<TEntity>().Where(list).ExecuteCommandAsync() > 0;
    }

    public virtual bool DeleteById(dynamic id)
    {
        var entry = _repository.Delete(id);
        return true;
        //return _sugarRepository.Context.Deleteable<TEntity>().In(id).ExecuteCommand() > 0;
    }
    public virtual async Task<bool> DeleteByIdAsync(dynamic id)
    {
        var item = await GetByIdAsync(id);
        await _repository.DeleteAsync(item);
        return true;
        // TODO: 无主键表，指定某个key作为主键，报错：
        //var entry = await _repository.DeleteAsync(id);
        //return true;
        
        // sugar
        //return await _sugarRepository.Context.Deleteable<TEntity>().In(id).ExecuteCommandAsync() > 0;
    }

    public virtual bool DeleteByIds(dynamic[] ids)
    {
        return _sugarRepository.Context.Deleteable<TEntity>().In(ids).ExecuteCommand() > 0;
    }

    public virtual async Task<bool> DeleteByIdsAsync(dynamic[] ids)
    {
        return await _sugarRepository.Context.Deleteable<TEntity>().In(ids).ExecuteCommandAsync() > 0;
    }

    public async Task<bool> FakeDeleteByIdAsync(dynamic id)
    {
        TEntity entity = await GetByIdAsync(id);
        if (entity != null && entity.SetPropertyValue(nameof(GardenerEntityBase.IsDeleted), true))
        {
            entity.SetPropertyValue(nameof(GardenerEntityBase.UpdatedTime), DateTimeOffset.Now);
            return await UpdateIncludeAsync(entity, new[] { nameof(GardenerEntityBase.IsDeleted), nameof(GardenerEntityBase.UpdatedTime) });
        }

        return false;
    }

    #endregion

    #region Insert
    public int InsertReturnId(TEntity insertObj)
    {
        return _sugarRepository.Context.Insertable(insertObj).ExecuteReturnIdentity();
    }

    public long InsertReturnSnowId(TEntity insertObj)
    {
        return _sugarRepository.Context.Insertable(insertObj).ExecuteReturnSnowflakeId();
    }

    public async Task<int> InsertReturnIdAsync(TEntity insertObj)
    {
        return await _sugarRepository.Context.Insertable(insertObj).ExecuteReturnIdentityAsync();
    }

    public async Task<long> InsertReturnSnowIdAsync(TEntity insertObj)
    {
        return await _sugarRepository.Context.Insertable(insertObj).ExecuteReturnSnowflakeIdAsync();
    }

    public async Task<List<long>> InsertReturnSnowIdAsync(List<TEntity> insertObjs)
    {
        return await _sugarRepository.Context.Insertable(insertObjs).ExecuteReturnSnowflakeIdListAsync();
    }

    public virtual TEntity InsertReturnEntity(TEntity entity)
    {
        return _sugarRepository.Context.Insertable(entity).ExecuteReturnEntity();
    }

    public virtual async Task<TEntity> InsertReturnEntityAsync(TEntity entity)
    {
        var entry = await _repository.InsertAsync(entity);
        return entry.Entity;

        //return await _sugarRepository.Context.Insertable(entity)
        //    .IgnoreColumns(ignoreNullColumn: true)
        //    .ExecuteReturnEntityAsync();
    }

    public async Task<bool> InsertAsync(TEntity insertObj, bool ignoreNullColumn = true)
    {
        var insertCount = await _sugarRepository.Context.Insertable(insertObj)
            .IgnoreColumns(ignoreNullColumn: ignoreNullColumn)
            .ExecuteCommandAsync();

        return insertCount > 0;
    }

    public async Task<bool> InsertAsync(List<TEntity> list, bool ignoreNullColumn = true)
    {
        var successCount = 0;
        foreach (var item in list)
        {
            var success = await InsertAsync(item, ignoreNullColumn);
            if (success)
            {
                successCount++;
            }
        }

        return successCount == list.Count;
    }

    #endregion

    #region Save or Update
    public virtual bool SaveOrUpdate(TEntity item, bool ignoreNullColumn = true)
    {
        // Update　
        var updateCount = _sugarRepository.Context.Updateable(item)
            .IgnoreColumns(ignoreAllNullColumns: ignoreNullColumn) // Not support list
            .ExecuteCommand();
        // Insert
        var insertCount = _sugarRepository.Context.Insertable(item)
            .IgnoreColumns(ignoreNullColumn: ignoreNullColumn) // Not support list
            .ExecuteCommand();

        return updateCount + insertCount > 0;
    }

    public virtual bool SaveOrUpdate(List<TEntity> list, bool ignoreNullColumn = true)
    {
        var successCount = 0;
        foreach (var item in list)
        {
            var success = SaveOrUpdate(item, ignoreNullColumn);
            if (success)
            {
                successCount++;
            }
        }

        return successCount == list.Count;
    }

    public virtual async Task<bool> SaveOrUpdateAsync(TEntity item, bool ignoreNullColumn = true)
    {
        // Update　
        var updateCount = await _sugarRepository.Context.Updateable(item)
            .IgnoreColumns(ignoreAllNullColumns: ignoreNullColumn) // Not support list
            .ExecuteCommandAsync();
        // Insert
        var insertCount = await _sugarRepository.Context.Insertable(item)
            .IgnoreColumns(ignoreNullColumn: ignoreNullColumn) // Not support list
            .ExecuteCommandAsync();

        return updateCount + insertCount > 0;
    }

    public virtual async Task<bool> SaveOrUpdateAsync(List<TEntity> list, bool ignoreNullColumn = true)
    {
        var successCount = 0;
        foreach (var item in list)
        {
            var success = await SaveOrUpdateAsync(item, ignoreNullColumn);
            if (success)
            {
                successCount++;
            }
        }

        return successCount == list.Count;
    }
    #endregion

    #region Update
    public virtual bool Update(TEntity item, bool ignoreNullColumn = true)
    {
        return _sugarRepository.Context.Updateable(item)
            .IgnoreColumns(UpdateIgnoreColumns)
            .IgnoreColumns(ignoreAllNullColumns: ignoreNullColumn) // Not support list
            .ExecuteCommandHasChange();
    }

    public virtual bool Update(List<TEntity> list, bool ignoreNullColumn = true)
    {
        var successCount = 0;
        foreach (var item in list)
        {
            var success = Update(item, ignoreNullColumn);
            if (success)
            {
                successCount++;
            }
        }

        return successCount == list.Count;
    }

    public virtual async Task<bool> UpdateAsync(TEntity item, bool ignoreNullColumn = true)
    {
        return await _sugarRepository.Context.Updateable(item)
            .IgnoreColumns(UpdateIgnoreColumns)
            .IgnoreColumns(ignoreAllNullColumns: ignoreNullColumn) // Not support list
            .ExecuteCommandHasChangeAsync();
    }

    public virtual async Task<bool> UpdateAsync(List<TEntity> list, bool ignoreNullColumn = true)
    {
        var successCount = 0;
        foreach (var item in list)
        {
            var success = await UpdateAsync(item, ignoreNullColumn);
            if (success)
            {
                successCount++;
            }
        }

        return successCount == list.Count;
    }

    public bool UpdateExclude(TEntity item, string[] ignoreColumns, bool ignoreNullColumn = true)
    {
        return _sugarRepository.Context.Updateable(item)
            .IgnoreColumns(ignoreColumns)
            .IgnoreColumns(UpdateIgnoreColumns)
            .IgnoreColumns(ignoreAllNullColumns: ignoreNullColumn) // Not support list
            .ExecuteCommandHasChange();
    }

    public async Task<bool> UpdateExcludeAsync(TEntity item, string[] ignoreColumns, bool ignoreNullColumn = true)
    {
        if (ignoreColumns == null || ignoreColumns.Length == 0)
        {
            ignoreColumns = UpdateIgnoreColumns;
        }

        var entry = await _repository.UpdateExcludeAsync(item, ignoreColumns, ignoreNullColumn);
        return true;

        //return await _sugarRepository.Context.Updateable(item)
        //    .IgnoreColumns(ignoreColumns)
        //    .IgnoreColumns(UpdateIgnoreColumns)
        //    .IgnoreColumns(ignoreAllNullColumns: ignoreNullColumn) // Not support list
        //    .ExecuteCommandHasChangeAsync();
    }

    public bool UpdateInclude(TEntity item, string[] updateColumns, bool ignoreNullColumn = true)
    {
        return _sugarRepository.Context.Updateable(item)
            .UpdateColumns(updateColumns)
            .IgnoreColumns(UpdateIgnoreColumns)
            .IgnoreColumns(ignoreAllNullColumns: ignoreNullColumn) // Not support list
            .ExecuteCommandHasChange();
    }

    public async Task<bool> UpdateIncludeAsync(TEntity item, string[] updateColumns, bool ignoreNullColumn = true)
    {
        var entry = await _repository
            .UpdateIncludeAsync(item, updateColumns, ignoreNullColumn);
        return true;

        //var res = await _sugarRepository.Context.Updateable(item)
        //    .UpdateColumns(updateColumns)
        //    .IgnoreColumns(UpdateIgnoreColumns)
        //    .IgnoreColumns(ignoreAllNullColumns: ignoreNullColumn) // Not support list
        //    .ExecuteCommandHasChangeAsync();

        //return res;
    }


    #endregion
    #endregion
}
#endregion

#region Contructor
public abstract class BaseService<TEntity> :
    BaseService<TEntity, MasterDbContextLocator>
    where TEntity : class, IPrivateEntity, new()
{
    protected BaseService(IRepository<TEntity, MasterDbContextLocator> repository,
        SqlSugarRepository<TEntity> sugarRepository)
        : base(repository, sugarRepository)
    {
    }

    protected BaseService(IRepository<TEntity> repository,
        SqlSugarRepository<TEntity> sugarRepository)
        : base(repository, sugarRepository)
    {
    }
}
#endregion