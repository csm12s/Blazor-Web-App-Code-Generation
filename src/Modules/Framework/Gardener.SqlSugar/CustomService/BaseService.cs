using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using Gardener.Common;
using Gardener.Sugar;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SqlSugar;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;

namespace Gardener.Base;

#region BaseService
/// <summary>
/// BaseService，用于处理CRUD和常用业务
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TDbContextLocator"></typeparam>
public abstract class BaseService
    <TEntity, TDbContextLocator> :
    IDynamicApiController,
    IBaseService<TEntity> where TEntity : class, IPrivateEntity, new()
    where TDbContextLocator : class, IDbContextLocator
{

    #region Init
    protected readonly string[] _updateIgnoreColumns = new string[]
    {
        nameof(GardenerEntityBase.CreatedTime),
        nameof(GardenerEntityBase.CreateBy),
        nameof(GardenerEntityBase.CreateIdentityType),
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

    #region Custom CRUD

    #region Get first
    public virtual TEntity GetFirst(Expression<Func<TEntity, bool>> whereExpression)
    {
        return _repository.FirstOrDefault(whereExpression);
    }

    public virtual async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return await _repository.FirstOrDefaultAsync(whereExpression);
    }
    #endregion

    #region Get Page
    public virtual Base.PagedList<TEntity> GetPage(PageRequest request)
    {
        var list = GetList(request);
        return list.ToPageList(request);
    }

    public virtual async Task<Base.PagedList<TEntity>> GetPageAsync(PageRequest request)
    {
        var list = await this.GetListAsync(request);
        return list.ToPageList(request);
    }
    #endregion

    #region Get List by Where
    public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> whereExpression)
    {
        return _repository.AsQueryable().Where(whereExpression).ToList();
    }

    public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return await _repository.AsQueryable().Where(whereExpression).ToListAsync();
    }

    #endregion

    #region Get List by PageRequest
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

        // TODO: If null in DB, skip where expression?
        // 如果查询的字段在数据库中字段为null这里会不会报错

        // EF:
        IQueryable<TEntity> queryable = _repository.AsQueryable(false);
        return await queryable
            .Where(expression) // Where
            .OrderConditions(request.OrderConditions.ToArray()) // Order by
            .ToListAsync();
    }
    #endregion
    #endregion

    #region Gardener CRUD
    public virtual async Task<TEntity> InsertAsync(TEntity input)
    {
        TEntity entity = input;
        var newEntity = await _repository.InsertNowAsync(entity);

        //发送通知
        await EntityEventNotityUtil.NotifyInsertAsync(newEntity.Entity);
        return newEntity.Entity;
    }

    public virtual async Task<bool> UpdateAsync(TEntity input)
    {
        EntityEntry<TEntity> entityEntry = await _repository
            .UpdateExcludeAsync(input, _updateIgnoreColumns);

        //发送通知
        await EntityEventNotityUtil.NotifyUpdateAsync(entityEntry.Entity);
        return true;
    }

    public virtual async Task<bool> DeleteAsync(object id)
    {
        await _repository.DeleteAsync(id);

        //发送删除通知
        await EntityEventNotityUtil.NotifyDeleteAsync<TEntity, object>(id);
        return true;
    }

    public virtual bool Deletes<TKey>(TKey[] ids)
    {
        foreach (TKey id in ids)
        {
            _repository.Delete(id);

        }
        EntityEventNotityUtil.NotifyDeletesAsync<TEntity, TKey>(ids);
        return true;
    }

    public virtual async Task<bool> DeletesAsync<TKey>(TKey[] ids)
    {
        foreach (TKey id in ids)
        {
            await _repository.DeleteAsync(id);
        }
        await EntityEventNotityUtil.NotifyDeletesAsync<TEntity, TKey>(ids);
        return true;
    }

    public virtual async Task<bool> FakeDeleteAsync(object id)
    {
        await _repository.FakeDeleteByKeyAsync(id);
        await EntityEventNotityUtil.NotifyFakeDeleteAsync<TEntity, object>(id);
        return true;
    }

    public virtual async Task<bool> FakeDeletesAsync<TKey>(TKey[] ids)
    {
        foreach (TKey id in ids)
        {
            await _repository.FakeDeleteByKeyAsync(id);
        }
        await EntityEventNotityUtil.NotifyFakeDeletesAsync<TEntity, TKey>(ids);
        return true;
    }

    public virtual async Task<TEntity> GetByIdAsync(object id)
    {
        var person = await GetReadableRepository().FindAsync(id);
        return person;
    }

    public virtual List<TEntity> GetAll()
    {
        var persons = GetReadableRepository().AsQueryable().Select(x => x);
        return persons.ToList();
    }
    public virtual async Task<List<TEntity>> GetAllAsync()
    {
        var persons = GetReadableRepository().AsQueryable().Select(x => x);
        return await persons.ToListAsync();
    }

    /// <summary>
    /// 查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)
    /// </summary>
    /// <returns></returns>
    public virtual async Task<List<TEntity>> GetAllUsableAsync(Guid? tenantId = null)
    {
        var paramList = new List<object>();
        StringBuilder where = new();
        where.Append(" 1==1 ");
        //判断是否有IsDelete
        Type type = typeof(TEntity);
        if (type.IsAssignableTo(typeof(IModelDeleted)))
        {
            where.Append($" &&  {nameof(IModelDeleted.IsDeleted)} == @{paramList.Count}");
            paramList.Add(false);
        }
        //判断是否有IsLock
        if (type.IsAssignableTo(typeof(IModelLocked)))
        {
            where.Append($" &&  {nameof(IModelLocked.IsLocked)} == @{paramList.Count}");
            paramList.Add(false);
        }
        //租户
        if (type.IsAssignableTo(typeof(IModelTenant)) && tenantId != null)
        {
            where.Append($" &&  {nameof(IModelTenant.TenantId)}.Equals(@{paramList.Count})");
            paramList.Add(tenantId);
        }
        var persons = GetReadableRepository().AsQueryable().Where(where.ToString(), paramList.ToArray()).Select(x => x);
        return await persons.ToListAsync();
    }

    public virtual async Task<Base.PagedList<TEntity>> GetPageAsync(int pageIndex = 1, int pageSize = 10)
    {
        var request = new PageRequest() { PageIndex = pageIndex, PageSize = pageSize };
        return await this.SearchAsync(request);
    }

    public virtual async Task<bool> LockAsync(object id, bool isLocked = true)
    {
        var entity = await _repository.FindAsync(id);
        if (entity != null && entity.SetPropertyValue(nameof(GardenerEntityBase.IsLocked), isLocked))
        {
            await _repository.UpdateIncludeAsync
                (entity, new[] { nameof(GardenerEntityBase.IsLocked)});
            await EntityEventNotityUtil.NotifyLockAsync(entity);
            return true;
        }
        return false;
    }

    public virtual async Task<Base.PagedList<TEntity>> SearchAsync(PageRequest request)
    {
        var list = await this.GetListAsync(request);
        return list.ToPageList(request);
    }

    public virtual async Task<string> GenerateSeedDataAsync(PageRequest request)
    {
        var pagedList = await this.GetPageAsync(request);

        return SeedDataGenerateTool.Generate(pagedList.Items, typeof(TEntity).Name);
    }
    #endregion
}
#endregion

#region BaseService Constructor
public abstract class BaseService<TEntity> :
    BaseService<TEntity, MasterDbContextLocator>, IBaseService<TEntity>
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