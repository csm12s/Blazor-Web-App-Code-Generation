// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur;
using Fur.DatabaseAccessor;
using Microsoft.Extensions.DependencyInjection;
using Gardener.EntityFramwork.Core.DbContexts;

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
                options.AddDbPool<GardenerDbContext>(DbProvider.MySql);
            }, "Gardener.Database.Migrations");
        }
    }
}
