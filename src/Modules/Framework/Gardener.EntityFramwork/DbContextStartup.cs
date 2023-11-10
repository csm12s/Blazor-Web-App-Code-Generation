// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Gardener.Base.Entity;
using Gardener.EntityFramwork.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Gardener.EntityFramwork
{
    /// <summary>
    /// Dbcontext默认启动项
    /// </summary>
    [AppStartup(601)]
    public class DbContextStartup : AppStartup
    {
        /// <summary>
        /// 初始化默认数据库
        /// </summary>
        /// <param name="services"></param>
        public virtual void ConfigureServices(IServiceCollection services)
        {
            // TODO: dbsettings.json里使用db type, 根据db type 自动设置dbProvider
            string? dbProvider = App.Configuration["DefaultDbSettings:DbProvider"];
            if (dbProvider == null)
            {
                throw new ArgumentNullException(nameof(dbProvider));
            }

            if (dbProvider == DbProvider.Npgsql)
            {
                //解决切换postgresql时可能出错 
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            }
            string? migrationAssemblyName = App.Configuration["DefaultDbSettings:MigrationAssemblyName"];
            services.AddDatabaseAccessor(options =>
                                        {
                                            //注入默认数据库上下文
                                            options.AddDbPool<GardenerDbContext>(dbProvider);
                                            //注入多租户数据库上下文
                                            options.AddDbPool<GardenerMultiTenantDbContext, GardenerMultiTenantDbContextLocator>(dbProvider);
                                            //注入审计数据库上下文
                                            options.AddDbPool<GardenerAuditDbContext, GardenerAuditDbContextLocator>(dbProvider);
                                        }, migrationAssemblyName);
            //要在使用db前注册上。
            services.AddHostedService<DbBackgroundService>();
        }

        /// <summary>
        /// 执行数据库初始化和迁移
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
