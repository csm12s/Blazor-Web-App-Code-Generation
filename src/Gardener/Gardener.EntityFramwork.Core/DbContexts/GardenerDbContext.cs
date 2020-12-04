// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;

namespace Gardener.EntityFramwork.Core.DbContexts
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    [AppDbContext("GardenerMysqlDbConnectionString")]
    public class GardenerDbContext : AppDbContext<GardenerDbContext>
    {
        public GardenerDbContext(DbContextOptions<GardenerDbContext> options) : base(options)
        {
        }
    }
}
