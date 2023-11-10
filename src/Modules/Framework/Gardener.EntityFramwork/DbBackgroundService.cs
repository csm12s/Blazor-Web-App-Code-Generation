// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DependencyInjection;
using Gardener.EntityFramwork.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Gardener.EntityFramwork
{
    /// <summary>
    /// 利用BackgroundService，提高初始化数据库的优先级
    /// </summary>
    public class DbBackgroundService : BackgroundService
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            //由于注册较早，启动比其他BackgroundService早

            AotuInitDb();

            return base.StartAsync(cancellationToken);
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //不需要执行
            return Task.CompletedTask;
        }

        /// <summary>
        /// 自动初始化数据库
        /// </summary>
        private void AotuInitDb()
        {
            var logger = App.GetService<ILogger<DbContextStartup>>();
            var initDb = bool.Parse(App.Configuration["DefaultDbSettings:InitDb"] ?? "false");
            var autoMigration = bool.Parse(App.Configuration["DefaultDbSettings:AutoMigration"] ?? "false");
            // 判断开发环境！！！必须！！！！
            if (App.WebHostEnvironment.IsDevelopment())
            {
                Scoped.Create((_, scope) =>
                {
                    List<DbContext> defaultDbContexts = new List<DbContext>
                    {
                        scope.ServiceProvider.GetRequiredService<GardenerDbContext>(),
                        scope.ServiceProvider.GetRequiredService<GardenerMultiTenantDbContext>(),
                        scope.ServiceProvider.GetRequiredService<GardenerAuditDbContext>()
                    };
                    foreach (DbContext dbContext in defaultDbContexts)
                    {
                        if (autoMigration)
                        {
                            //需要有迁移文件，如果没有迁移文件不会迁移，迁移后会生成迁移记录在表中，如果迁移记录与实际表版本不一致，将异常。
                            dbContext.Database.Migrate();
                            logger.LogInformation($"数据库{dbContext.GetType().FullName}迁移完成");
                        }
                        if (initDb)
                        {
                            //不需要迁移文件，如果数据库已存在，不生成，如果不存在，生成数据库，生成的数据库没有迁移记录，无法再使用migrate进行迁移。
                            dbContext.Database.EnsureCreated();
                            logger.LogInformation($"数据库{dbContext.GetType().FullName}初始化完成");
                        }
                    }
                });
            }
        }
    }
}
