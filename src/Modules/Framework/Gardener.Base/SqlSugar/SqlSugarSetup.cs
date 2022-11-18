using Furion;
using Furion.Logging.Extensions;
using Gardener.Authentication.Dtos;
using Gardener.Common;
using Gardener.Enums;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace Gardener.Base;

public static class SqlSugarSetup
{
    /// <summary>
    /// SqlsugarScope的配置
    /// Scope必须用单例注入
    /// 不可以用Action委托注入
    /// </summary>
    /// <param name="services"></param>
    public static void SqlSugarScopeConfigure(this IServiceCollection services)
    {
        //数据库序号从0开始,默认数据库为0
        var config = App.GetOptions<ConnectionStringsOptions>();
        var defaultDbSetting = App.GetOptions<DefaultDbSettingsOptions>();

        #region DB
        //默认数据库
        List<DbConfig> dbList = new List<DbConfig>();

        //Microsoft.EntityFrameworkCore.SqlServer
        var efDbProvider = defaultDbSetting.DbProvider;
        var defaultDbNumber = "0";
        if (config.DefaultDbNumber != null)
        {
            defaultDbNumber = config.DefaultDbNumber;
        }
        DbConfig defaultdb = new DbConfig()
        {
            DbNumber = defaultDbNumber,
            DbString = config.Default, //DefaultDbString,
            DbType = efDbProvider.Substring(efDbProvider.LastIndexOf(".") + 1) //config.DefaultDbType
        };
        dbList.Add(defaultdb);

        // SqlSugar多库设置, 业务数据库集合
        if (config.DbConfigs != null)
        {
            foreach (var item in config.DbConfigs)
            {
                dbList.Add(item);
            }
        }

        List<ConnectionConfig> connectConfigList = new List<ConnectionConfig>();

        foreach (var item in dbList)
        {
            //防止数据库重复，导致的事务异常
            if (connectConfigList.Any(a => a.ConfigId == (dynamic)item.DbNumber || a.ConnectionString == item.DbString))
            {
                continue;
            }
            connectConfigList.Add(new ConnectionConfig()
            {
                ConnectionString = item.DbString,
                DbType = (DbType)Convert.ToInt32(Enum.Parse(typeof(DbType), item.DbType)),
                IsAutoCloseConnection = true,
                ConfigId = item.DbNumber,
                InitKeyType = InitKeyType.Attribute,
                MoreSettings = new ConnMoreSettings()
                {
                    IsAutoRemoveDataCache = true//自动清理缓存

                },
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    //DataInfoCacheService = new SqlSugarCache(),
                    EntityNameService = (type, entity) =>
                    {
                        var attributes = type.GetCustomAttributes(true);
                        if (attributes.Any(it => it is TableAttribute))
                        {
                            entity.DbTableName = (attributes.First(it => it is TableAttribute) as TableAttribute).Name;
                        }
                    },
                    EntityService = (type, column) =>
                    {
                        var attributes = type.GetCustomAttributes(true);
                        if (attributes.Any(it => it is KeyAttribute))// by attribute set primarykey
                        {
                            column.IsPrimarykey = true; //有哪些特性可以看 1.2 特性明细
                        }
                        if (attributes.Any(it => it is ColumnAttribute))
                        {
                            column.DbColumnName = (attributes.First(it => it is ColumnAttribute) as ColumnAttribute).Name;
                        }

                        // Nullable ? / [SugarColumn(IsNullable = true)]
                        // C# x.x or above
                        if (new NullabilityInfoContext()
                            .Create(type).WriteState is NullabilityState.Nullable)
                        {
                            column.IsNullable = true;
                        }

                        // Length
                        if (attributes.Any(it => it is MaxLengthAttribute))
                        {
                            column.Length = (attributes.First(it => it is MaxLengthAttribute) as MaxLengthAttribute).Length;
                        }
                    }
                }
            });
        }
        #endregion

        // Get all tables via model
        var entityTypes = App.EffectiveTypes
            .Where(a => !a.IsAbstract 
                && a.IsClass
                && a.GetCustomAttributes(typeof(SugarTable), true)?
            .FirstOrDefault() != null)
            .ToArray();

        #region InitDB
#if DEBUG
        // TODO: 这个应该在EF初始化建库之后执行
        if (defaultDbSetting.InitDb) //bool.Parse(App.Configuration["DefaultDbSettings:InitSugarDb"]);
        {
            using (var db = new SqlSugarClient(connectConfigList.FirstOrDefault()))
            {
                // Drop table
                try
                {
                    //db.DbMaintenance.DropTable("Sys_Code_Gen");
                    //db.DbMaintenance.DropTable("Sys_Code_Gen_Config");
                }
                catch (Exception ex)
                {
                }

                // Init DB
                db.CodeFirst
                    .SetStringDefaultLength(DbConstants.StringDefaultLength)
                    .InitTables(entityTypes);//根据types创建表
            }
        }
#endif
        #endregion

        #region Filters
        SqlSugarScope sqlSugarScope = new SqlSugarScope(connectConfigList,
            //全局上下文生效
            db =>
            {
                /*
                 * 默认配置到第一个数据库，这里按照官方文档进行多数据库/多租户文档的说明进行循环配置
                 */
                foreach (var connect in connectConfigList)
                {
                    var dbProvider = db.GetConnectionScope((string)connect.ConfigId);
                    //执行超时时间
                    dbProvider.Ado.CommandTimeOut = DbConstants.SqlCommandTimeoutSeconds;//30

                    dbProvider.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        if (sql.StartsWith("SELECT"))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        if (sql.StartsWith("UPDATE") || sql.StartsWith("INSERT"))
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        if (sql.StartsWith("DELETE"))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        Console.WriteLine("Sql:" + "\r\n\r\n" + UtilMethods.GetSqlString(db.CurrentConnectionConfig.DbType, sql, pars));
                        App.PrintToMiniProfiler("SqlSugar", "Info", UtilMethods.GetSqlString(db.CurrentConnectionConfig.DbType, sql, pars));
                        $"Sql:\r\n\r\n { UtilMethods.GetSqlString(db.CurrentConnectionConfig.DbType, sql, pars)}".LogInformation();
                    };

                    #region Insert & Update
                    dbProvider.Aop.DataExecuting = (oldValue, entityInfo) =>
                    {
                        // On Insert
                        if (entityInfo.OperationType == DataFilterType.InsertByObject)
                        {
                            // Key
                            if (entityInfo.EntityColumnInfo.IsPrimarykey)
                            {
                                var type = entityInfo.EntityColumnInfo.PropertyInfo.PropertyType;
                                // Long SnowFlake Id
                                if (type == typeof(long))
                                {
                                    var id = ((dynamic)entityInfo.EntityValue).Id;
                                    if (id == null || id == 0)
                                        entityInfo.SetValue(IdUtil.GetNextId());
                                }
                                // String SnowFlake Id
                                else if (type == typeof(string))
                                {
                                    var id = ((dynamic)entityInfo.EntityValue).Id;
                                    if (id == null || id == "")
                                        entityInfo.SetValue(IdUtil.GetNextId().ToString());
                                }
                            }

                            // "CreatedTime"
                            if (entityInfo.PropertyName == nameof(GardenerEntityBase.CreatedTime))
                                entityInfo.SetValue(DateTimeOffset.Now);
                            if (App.User != null)
                            {
                                // TenantId
                                if (entityInfo.PropertyName == nameof(GardenerTenantEntityBase.TenantId))
                                {
                                    var tenantId = ((dynamic)entityInfo.EntityValue).TenantId; // .TenantId
                                    if (tenantId == null || tenantId == 0)
                                        entityInfo.SetValue(App.User.FindFirst("TenantId")?.Value);//nameof(AuthKeyConstants.TenantId)
                                }
                                // CreateBy
                                if (entityInfo.PropertyName == nameof(GardenerEntityBase.CreatorId))
                                {
                                    var createUserId = ((dynamic)entityInfo.EntityValue).CreatorId; // "CreatorId" here
                                    if (string.IsNullOrEmpty(createUserId))
                                        entityInfo.SetValue(App.User.FindFirst("UserId")?.Value);//nameof(AuthKeyConstants.UserId)
                                }
                            }
                        }
                        
                        // On Update
                        if (entityInfo.OperationType == DataFilterType.UpdateByObject)
                        {
                            if (entityInfo.PropertyName == nameof(GardenerEntityBase.UpdatedTime))
                                entityInfo.SetValue(DateTimeOffset.Now);

                            if (entityInfo.PropertyName == nameof(GardenerEntityBase.UpdateUserId))
                                entityInfo.SetValue(App.User?.FindFirst("UserId")?.Value); //nameof(Identity.UserId)
                        }
                    };
                    #endregion

                    /* 
                     * 使用 SqlSugarScope 循环配置此项的时候会覆盖整个 ConfigureExternalServices，
                     * 移动到 New ConnectionConfig中配置
                     */
                    //db.CurrentConnectionConfig.ConfigureExternalServices = new ConfigureExternalServices()
                    //{
                    //    DataInfoCacheService = new SqlSugarCache()//配置我们创建的缓存类
                    //};

                    #region Tenant & Delete
                    var superAdminViewAllData = true;
                    //Convert.ToBoolean(App.GetOptions<SystemSettingsOptions>().SuperAdminViewAllData);
                    foreach (var entityType in entityTypes)
                    {
                        // 多租户全局过滤器
                        if (!entityType.GetProperty(nameof(Identity.TenantId)).IsEmpty())
                        { //判断实体类中包含TenantId属性
                          //构建动态Lambda
                            var lambda = DynamicExpressionParser.ParseLambda
                            (new[] { Expression.Parameter(entityType, "it") },
                             typeof(bool), $"{nameof(GardenerTenantEntityBase.TenantId)} ==  @0 or (@1 and @2)",
                              GetTenantId(), IsSuperAdmin(), superAdminViewAllData);
                            dbProvider.QueryFilter.Add(new TableFilterItem<object>(entityType, lambda)); //将Lambda传入过滤器
                        }

                        // 软删除全局过滤器
                        if (!entityType.GetProperty(nameof(GardenerEntityBase.IsDeleted)).IsEmpty())
                        { //判断实体类中包含IsDeleted属性
                          //构建动态Lambda
                            var lambda = DynamicExpressionParser.ParseLambda
                            (new[] { Expression.Parameter(entityType, "it") },
                             typeof(bool), $"{nameof(GardenerEntityBase.IsDeleted)} ==  @0",
                              false);
                            dbProvider.QueryFilter.Add(new TableFilterItem<object>(entityType, lambda)
                            {
                                IsJoinQuery = true
                            }); //将Lambda传入过滤器
                        }
                    }
                    #endregion
                }
            });
        #endregion

        services.AddSingleton<ISqlSugarClient>(sqlSugarScope);
        // 注册 SqlSugar 仓储
        services.AddScoped(typeof(SqlSugarRepository<>));
        services.AddScoped(typeof(SqlSugarRepository));
    }

    /// <summary>
    /// 获取当前租户id
    /// </summary>
    /// <returns></returns>
    private static object GetTenantId()
    {
        if (App.User == null) return null;
        return App.User.FindFirst(nameof(GardenerTenantEntityBase.TenantId))?.Value;
    }

    /// <summary>
    /// 判断是不是超级管理员
    /// </summary>
    /// <returns></returns>
    private static bool IsSuperAdmin()
    {
        if (App.User == null) return false;
        return App.User.FindFirst("AdminType")?.Value == AdminType.SuperAdmin.GetCode();//nameof(AuthKeyConstants.AdminType)
    }
    /// <summary>
    /// 添加 SqlSugar 拓展
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config"></param>
    /// <param name="buildAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddSqlSugar(this IServiceCollection services, ConnectionConfig config, Action<ISqlSugarClient> buildAction = default)
    {
        var list = new List<ConnectionConfig>();
        list.Add(config);
        return services.AddSqlSugar(list, buildAction);
    }

    /// <summary>
    /// 添加 SqlSugar 拓展
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configs"></param>
    /// <param name="buildAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddSqlSugar(this IServiceCollection services, List<ConnectionConfig> configs, Action<ISqlSugarClient> buildAction = default)
    {
        // 注册 SqlSugar 客户端
        services.AddScoped<ISqlSugarClient>(u =>
        {
            var db = new SqlSugarClient(configs);
            buildAction?.Invoke(db);
            return db;
        });

        // 注册 SqlSugar 仓储
        services.AddScoped(typeof(SqlSugarRepository<>));
        services.AddScoped(typeof(SqlSugarRepository));

        return services;
    }

}
