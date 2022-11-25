using Furion;
using Furion.FriendlyException;
using Gardener.Base;
using Gardener.Common;
using Gardener.Enums;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System.Linq.Expressions;
using System.Reflection;

namespace Gardener.SqlSugar;

/// <summary>
/// SqlSugar 仓储实现类
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public partial class SqlSugarRepository<TEntity> : SqlSugarRepository where TEntity : class, new()
{
    #region Init
    /// <summary>
    /// 
    /// </summary>
    private readonly SqlSugarScope db;
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public virtual SqlSugarScope Context { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="db"></param>
    public SqlSugarRepository(IServiceProvider serviceProvider, ISqlSugarClient db) : base(serviceProvider)
    {
        try
        {
            #region SqlSugarRepository
            Context = this.db = (SqlSugarScope)db;
            // 数据库上下文根据实体切换,业务分库(使用环境例如微服务)
            var entityType = typeof(TEntity);
            if (entityType.IsDefined(typeof(TenantAttribute), false))
            {
                var tenantAttribute = entityType.GetCustomAttribute<TenantAttribute>(false)!;
                Context.ChangeDatabase(tenantAttribute.configId);
            }
            else
            {
                var defaultDbNumber = "0";
                var config = App.GetOptions<ConnectionStringsOptions>();
                if (config.DefaultDbNumber != null)
                {
                    defaultDbNumber = config.DefaultDbNumber;
                }

                Context.ChangeDatabase(defaultDbNumber);
            }
            Ado = this.db.Ado;
            #endregion
        }
        catch (Exception ex)
        {
            throw Oops.Bah(ExceptionCode.Sugar_Repository_Init_Fail);
        }
    }

    /// <summary>
    /// 实体集合
    /// </summary>
    public virtual ISugarQueryable<TEntity> Entities => db.Queryable<TEntity>();

    /// <summary>
    /// 原生 Ado 对象
    /// </summary>
    public virtual IAdo Ado { get; }
    #endregion


    #region CRUD
    #region Get
    public virtual bool Any(Expression<Func<TEntity, bool>> whereExpression)
    {
        return Context.Queryable<TEntity>().Where(whereExpression).Any();
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return await Context.Queryable<TEntity>().Where(whereExpression).AnyAsync();
    }

    public virtual TEntity GetById(object id)
    {
        return Context.Queryable<TEntity>().InSingle(id);
    }

    public virtual async Task<TEntity> GetByIdAsync(object id)
    {
        return await Context.Queryable<TEntity>().InSingleAsync(id);
    }

    public virtual TEntity GetFirst(Expression<Func<TEntity, bool>> whereExpression)
    {
        return Context.Queryable<TEntity>().First(whereExpression);
    }

    public virtual async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return await Context.Queryable<TEntity>().FirstAsync(whereExpression);
    }

    public virtual async Task<List<TEntity>> GetListAsync()
    {
        return await Context.Queryable<TEntity>().ToListAsync();
    }

    public virtual List<TEntity> GetList(Expression<Func<TEntity, bool>> whereExpression)
    {
        return Context.Queryable<TEntity>().Where(whereExpression).ToList();
    }

    public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> whereExpression)
    {
        return await Context.Queryable<TEntity>().Where(whereExpression).ToListAsync();
    }

    public List<TEntity> GetAll()
    {
        return Context.Queryable<TEntity>().ToList();
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await Context.Queryable<TEntity>().ToListAsync();
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


        // Sugar:
        IQueryable<TEntity> queryable = (IQueryable<TEntity>)Context.Queryable<TEntity>();
        return queryable
            .Where(expression) // Where
            .OrderConditions(request.OrderConditions.ToArray()) // Order by
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

        //// TODO: If null in DB, skip where expression?
        //// 如果查询的字段在数据库中字段为null这里会不会报错


        // Sugar:
        var listAll = await GetAllAsync();
        IQueryable<TEntity> queryable = listAll.AsQueryable();
        try
        {
            var list = queryable
                .Where(expression) // Where
                .OrderConditions(request.OrderConditions.ToArray()) // Order by
                .ToList();

            return list;
        }
        catch (Exception ex)
        {
            // TODO: 这里不能抛出异常
            throw Oops.Oh(ExceptionCode.Search_Error_DB_Field_Is_Null);
        }
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

    #region Delete
    public virtual bool Delete(TEntity item)
    {
        return Context.Deleteable(item).ExecuteCommand() > 0;
    }

    public virtual async Task<bool> DeleteAsync(TEntity item)
    {
        return await Context.Deleteable<TEntity>().Where(item).ExecuteCommandAsync() > 0;
    }

    public virtual bool Delete(List<TEntity> list)
    {
        return Context.Deleteable<TEntity>().Where(list).ExecuteCommand() > 0;
    }

    public virtual async Task<bool> DeleteAsync(List<TEntity> list)
    {
        return await Context.Deleteable<TEntity>().Where(list).ExecuteCommandAsync() > 0;
    }

    public virtual bool DeleteById(dynamic id)
    {
        return Context.Deleteable<TEntity>().In(id).ExecuteCommand() > 0;
    }
    public virtual async Task<bool> DeleteByIdAsync(dynamic id)
    {
        // sugar
        return await Context.Deleteable<TEntity>().In(id).ExecuteCommandAsync() > 0;
    }

    public virtual bool DeleteByIds(dynamic[] ids)
    {
        return Context.Deleteable<TEntity>().In(ids).ExecuteCommand() > 0;
    }

    public virtual async Task<bool> DeleteByIdsAsync(dynamic[] ids)
    {
        return await Context.Deleteable<TEntity>().In(ids).ExecuteCommandAsync() > 0;
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
        return Context.Insertable(insertObj).ExecuteReturnIdentity();
    }

    public long InsertReturnSnowId(TEntity insertObj)
    {
        return Context.Insertable(insertObj).ExecuteReturnSnowflakeId();
    }

    public async Task<int> InsertReturnIdAsync(TEntity insertObj)
    {
        return await Context.Insertable(insertObj).ExecuteReturnIdentityAsync();
    }

    public async Task<long> InsertReturnSnowIdAsync(TEntity insertObj)
    {
        return await Context.Insertable(insertObj).ExecuteReturnSnowflakeIdAsync();
    }

    public async Task<List<long>> InsertReturnSnowIdAsync(List<TEntity> insertObjs)
    {
        return await Context.Insertable(insertObjs).ExecuteReturnSnowflakeIdListAsync();
    }

    public virtual TEntity InsertReturnEntity(TEntity entity)
    {
        return Context.Insertable(entity).ExecuteReturnEntity();
    }

    public virtual async Task<TEntity> InsertReturnEntityAsync(TEntity entity)
    {

        return await Context.Insertable(entity)
            .IgnoreColumns(ignoreNullColumn: true)
            .ExecuteReturnEntityAsync();
    }

    public async Task<bool> InsertAsync(TEntity insertObj, bool ignoreNullColumn = true)
    {
        var insertCount = await Context.Insertable(insertObj)
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
        var updateCount = Context.Updateable(item)
            .IgnoreColumns(ignoreAllNullColumns: ignoreNullColumn) // Not support list
            .ExecuteCommand();
        // Insert
        var insertCount = Context.Insertable(item)
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
        var updateCount = await Context.Updateable(item)
            .IgnoreColumns(ignoreAllNullColumns: ignoreNullColumn) // Not support list
            .ExecuteCommandAsync();
        // Insert
        var insertCount = await Context.Insertable(item)
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
        return Context.Updateable(item)
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
        return await Context.Updateable(item)
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
        return Context.Updateable(item)
            .IgnoreColumns(ignoreColumns)
            .IgnoreColumns(ignoreAllNullColumns: ignoreNullColumn) // Not support list
            .ExecuteCommandHasChange();
    }

    public async Task<bool> UpdateExcludeAsync(TEntity item, string[] ignoreColumns, bool ignoreNullColumn = true)
    {
        return await Context.Updateable(item)
            .IgnoreColumns(ignoreColumns)
            .IgnoreColumns(ignoreAllNullColumns: ignoreNullColumn) // Not support list
            .ExecuteCommandHasChangeAsync();
    }

    public bool UpdateInclude(TEntity item, string[] updateColumns, bool ignoreNullColumn = true)
    {
        return Context.Updateable(item)
            .UpdateColumns(updateColumns)
            .IgnoreColumns(ignoreAllNullColumns: ignoreNullColumn) // Not support list
            .ExecuteCommandHasChange();
    }

    public async Task<bool> UpdateIncludeAsync(TEntity item, string[] updateColumns, bool ignoreNullColumn = true)
    {
        var res = await Context.Updateable(item)
            .UpdateColumns(updateColumns)
            .IgnoreColumns(ignoreAllNullColumns: ignoreNullColumn) // Not support list
            .ExecuteCommandHasChangeAsync();

        return res;
    }


    #endregion
    #endregion

}
/// <summary>
/// SqlSugar 仓储实现类
/// </summary>
public class SqlSugarRepository
{
    /// <summary>
    /// 服务提供器
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    public SqlSugarRepository(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 切换仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>仓储</returns>
    public virtual SqlSugarRepository<TEntity> Change<TEntity>()
        where TEntity : class, new()
    {
        return _serviceProvider.GetService<SqlSugarRepository<TEntity>>();
    }
}
