using Furion;
using Furion.DatabaseAccessor;
using Furion.FriendlyException;
using Gardener.Common;
using Gardener.Enums;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Gardener.Base;

/// <summary>
/// SqlSugar 仓储实现类
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public partial class SqlSugarRepository<TEntity> :ISqlSugarRepository<TEntity> where TEntity : class, new()
{
    #region Init

    private readonly SqlSugarScope db;


    /// <summary>
    /// 数据库上下文
    /// </summary>
    public virtual SqlSugarScope Context { get; }

    public SqlSugarRepository(ISqlSugarClient db)
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

    
}
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
