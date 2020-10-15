// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur;
using Fur.DatabaseAccessor;
using Microsoft.Extensions.DependencyInjection;
using YiPaiKe.EntityFramwork.Core.DbContexts;

namespace YiPaiKe.EntityFramwork.Core
{
    /// <summary>
    /// ef启动类
    /// </summary>
    [AppStartup(600)]
    public sealed class YiPaiKeEntityFrameworkCoreStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseAccessor(options =>
            {
                //注入数据库上下文
                options.AddDbPool<YiPaiKeDbContext>(DbProvider.Sqlite);
            }, "YiPaiKe.Database.Migrations");
        }
    }
}
