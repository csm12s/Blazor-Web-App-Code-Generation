// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Gardener.EntityFramwork.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
                                            //注入数据库上下文
                                            options.AddDbPool<GardenerDbContext>(dbProvider);

                                            options.AddDbPool<GardenerAuditDbContext, GardenerAuditDbContextLocator>(dbProvider);
                                        },
                                            migrationAssemblyName);
        }

        /// <summary>
        /// 执行数据库初始化和迁移
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var logger = App.GetService<ILogger<DbContextStartup>>();
            var initDb = bool.Parse(App.Configuration["DefaultDbSettings:InitDb"] ?? "false");
            var autoMigration = bool.Parse(App.Configuration["DefaultDbSettings:AutoMigration"] ?? "false");
            // 判断开发环境！！！必须！！！！
            if (env.IsDevelopment())
            {
                Scoped.Create((_, scope) =>
                {
                    var defaultDbContext = scope.ServiceProvider.GetRequiredService<GardenerDbContext>();
                    var auditDbContext = scope.ServiceProvider.GetRequiredService<GardenerAuditDbContext>();
                    if (initDb)//TODO: 如果数据库已存在，这里不会建表
                    {
                        defaultDbContext.Database.EnsureCreated();
                        auditDbContext.Database.EnsureCreated();
                        logger.LogInformation("数据库初始化完成");
                    }
                    if (autoMigration)
                    {
                        defaultDbContext.Database.Migrate();
                        auditDbContext.Database.Migrate();
                        logger.LogInformation("数据库迁移完成");
                    }
                });
            }
        }
    }
}
