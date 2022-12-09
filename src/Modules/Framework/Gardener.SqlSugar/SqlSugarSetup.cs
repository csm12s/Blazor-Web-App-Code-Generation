using Furion;
using Furion.Logging.Extensions;
using Gardener.Authentication.Dtos;
using Gardener.Authorization.Core;
using Gardener.Base;
using Gardener.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace Gardener.Sugar;
/// <summary>
/// SqlSugar
/// </summary>
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

        #region DB List
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
            var dbType = (DbType)Convert.ToInt32(Enum.Parse(typeof(DbType), item.DbType));
            connectConfigList.Add(new ConnectionConfig()
            {
                ConnectionString = item.DbString,
                DbType = dbType,
                IsAutoCloseConnection = true,
                ConfigId = item.DbNumber,
                InitKeyType = InitKeyType.Attribute,
                MoreSettings = new ConnMoreSettings()
                {
                    DisableNvarchar = false,
                    IsAutoRemoveDataCache = true
                },
                ConfigureExternalServices = GetConfigureServicesInfo(dbType)
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
        // 不再使用Sugar的CodeFirst，如果要使用，需要完善:
        // 1: GetConfigureServicesInfo里完善兼容多库
        // 2: 这个应该在EF初始化建库之后执行
        var useSqlSugarDbFirst = true;
        if (useSqlSugarDbFirst && defaultDbSetting.InitDb) //bool.Parse(App.Configuration["DefaultDbSettings:InitSugarDb"]);
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
                db.DbMaintenance.CreateDatabase();
                // Init tables
                db.CodeFirst
                    //.SetStringDefaultLength(DbConstants.StringDefaultLength)
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
                        $"Sql:\r\n\r\n {UtilMethods.GetSqlString(db.CurrentConnectionConfig.DbType, sql, pars)}".LogInformation();
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

                            // TenantId
                            if (entityInfo.PropertyName == nameof(GardenerTenantEntityBase.TenantId))
                            {
                                // 这里需要判断一下非空
                                var tenantId = ((dynamic)entityInfo.EntityValue).TenantId;
                                if (tenantId == null || tenantId == 0)
                                    entityInfo.SetValue(App.User?.FindFirst(nameof(Identity.TenantId))?.Value);
                            }
                            // "CreatedTime"
                            else if (entityInfo.PropertyName == nameof(GardenerEntityBase.CreatedTime))
                                entityInfo.SetValue(DateTimeOffset.Now);
                            // CreateBy
                            else if (entityInfo.PropertyName == nameof(GardenerEntityBase.CreateBy))
                                entityInfo.SetValue(IdentityUtil.GetIdentityId());
                            // CreateIdentityType
                            else if (entityInfo.PropertyName == nameof(GardenerEntityBase.CreateIdentityType))
                                entityInfo.SetValue(IdentityUtil.GetIdentityType());
                        }

                        // On Update
                        if (entityInfo.OperationType == DataFilterType.UpdateByObject)
                        {
                            if (entityInfo.PropertyName == nameof(GardenerEntityBase.UpdatedTime))
                                entityInfo.SetValue(DateTimeOffset.Now);
                            else if (entityInfo.PropertyName == nameof(GardenerEntityBase.UpdateBy))
                                entityInfo.SetValue(IdentityUtil.GetIdentityId());
                            else if (entityInfo.PropertyName == nameof(GardenerEntityBase.UpdateIdentityType))
                                entityInfo.SetValue(IdentityUtil.GetIdentityType());
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
    /// 兼容 EF Core
    /// (CodeFirst待完善，请查看下面的"Unicode (多库兼容)"部分)
    /// CodeFirst: https://www.donet5.com/Home/Doc?typeId=1206
    /// 默认配置 https://www.donet5.com/Home/Doc?typeId=1182
    /// 兼容配置 https://www.donet5.com/Ask/9/11065
    /// </summary>
    /// <returns></returns>
    private static ConfigureExternalServices GetConfigureServicesInfo(DbType dbType)
    {
        ConfigureExternalServices externalServices = new ConfigureExternalServices();

        // Table:
        externalServices.EntityNameService = (type, tableInfo) =>
        {
            var tableAttr = type.GetCustomAttribute<TableAttribute>(false);
            if (tableAttr != null)
            {
                tableInfo.DbTableName = tableAttr.Name;
            }
        };

        // Fields:
        externalServices.EntityService = (propInfo, columnInfo) =>
        {
            // Key
            var keyAttr = propInfo.GetCustomAttribute<KeyAttribute>();
            if (keyAttr != null)
            { 
                columnInfo.IsPrimarykey = true;
            }

            // Column: [Column("PrdRef", TypeName = "Nvarchar(20)")]
            var columnAttr = propInfo.GetCustomAttribute<ColumnAttribute>();
            if (columnAttr != null)
            {
                columnInfo.DbColumnName = columnAttr.Name;

                #region TypeName
                // 这里不再设置TypeName，因为TypeName不支持多库，
                //if (!string.IsNullOrEmpty(columnAttr.TypeName))
                //{
                //    // 和官方确认过，columnInfo.ColumnDataType对应DataType
                //    columnInfo.DataType = columnAttr.TypeName;
                //    //columnInfo.DataType = columnAttr.TypeName.Split("(").FirstOrDefault();
                //}
                #endregion
            }

            // Comment
            var commentAttr = propInfo.GetCustomAttribute<CommentAttribute>();
            if (commentAttr != null)
            { 
                columnInfo.ColumnDescription = commentAttr.Comment;
            }

            // Nullable ? / [SugarColumn(IsNullable = true)]
            if (new NullabilityInfoContext()
                .Create(propInfo).WriteState is NullabilityState.Nullable)
            {
                columnInfo.IsNullable = true;
            }

            // Length
            var maxlengthAttr = propInfo.GetCustomAttribute<MaxLengthAttribute>();
            if (maxlengthAttr != null)
            { 
                columnInfo.Length = maxlengthAttr.Length;
            }

            // StringLength
            var stringlengthAttr = propInfo.GetCustomAttribute<StringLengthAttribute>();
            if (stringlengthAttr != null)
            { 
                columnInfo.Length = stringlengthAttr.MaximumLength;
            }

            // NotMapped
            var notmappedAttr = propInfo.GetCustomAttribute<NotMappedAttribute>();
            if (notmappedAttr != null)
            { 
                columnInfo.IsIgnore = true;
            }

            // 如果想使用Sugar的CodeFirst
            // 需要参考https://www.donet5.com/Home/Doc?typeId=1206
            // "9、自定义类型多库兼容" 进行完善
            // Unicode (多库兼容)
            var unicodeAttr = propInfo.GetCustomAttribute<UnicodeAttribute>();
            if (unicodeAttr != null)
            {
                if (dbType == DbType.SqlServer)
                {
                    if (maxlengthAttr == null)
                    {
                        columnInfo.DataType = "Nvarchar(max)";
                    }
                    else
                    {
                        columnInfo.DataType = "Nvarchar";//待验证
                    }
                }
                else if (dbType == DbType.MySql)// 待验证
                {
                    if (columnInfo.DataType == "varchar(max)")
                    {
                        columnInfo.DataType = "longtext";
                    }
                    //...
                }
                // else if...
            }
        };

        return externalServices;
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
        IAuthorizationService authorization= App.GetService<IAuthorizationService>();
        return authorization.IsSuperAdministrator().Result;
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
