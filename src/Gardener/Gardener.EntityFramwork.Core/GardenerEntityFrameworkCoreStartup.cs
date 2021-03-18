// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Microsoft.Extensions.DependencyInjection;
using Gardener.EntityFramwork.Core.DbContexts;
using Gardener.Core.Entites;

namespace Gardener.EntityFramwork.Core
{
    /// <summary>
    /// ef启动类
    /// </summary>
    [AppStartup(600)]
    public sealed class GardenerEntityFrameworkCoreStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseAccessor(options =>
            {
                //注入数据库上下文
                //options.AddDbPool<GardenerDbContext>($"{DbProvider.MySql}@8.0.22");
                //options.AddDbPool<GardenerDbContext>(DbProvider.SqlServer);
                options.AddDbPool<GardenerDbContext>(DbProvider.Sqlite);
                options.AddDbPool<GardenerAuditDbContext, GardenerAuditDbContextLocator>(DbProvider.Sqlite);
            }, "Gardener.Database.Migrations");
        }
    }
}
