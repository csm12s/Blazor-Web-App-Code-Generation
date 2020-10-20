// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace Gardener.EntityFramwork.Core.DbContexts
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    [AppDbContext("GardenerDbConnectionString")]
    public class GardenerDbContext : AppDbContext<GardenerDbContext>
    {
        public GardenerDbContext(DbContextOptions<GardenerDbContext> options) : base(options)
        {
        }
    }
}
