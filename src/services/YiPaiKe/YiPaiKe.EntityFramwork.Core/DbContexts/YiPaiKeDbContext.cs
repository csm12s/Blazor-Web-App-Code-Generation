// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace YiPaiKe.EntityFramwork.Core.DbContexts
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    [AppDbContext("YiPaiKeDbConnectionString")]
    public class YiPaiKeDbContext : AppDbContext<YiPaiKeDbContext>
    {
        public YiPaiKeDbContext(DbContextOptions<YiPaiKeDbContext> options) : base(options)
        {
        }
    }
}
