// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Entites;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Core.Audit
{
    /// <summary>
    /// 当前请求的审计数据管理
    /// </summary>
    public interface IAuditDataManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        public Task SavingChangesEvent(DbContextEventData eventData, InterceptionResult<int> result);
        /// <summary>
        /// 数据保存后
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        public Task SavedChangesEvent(SaveChangesCompletedEventData eventData, int result);

        /// <summary>
        /// 保存操作审计
        /// </summary>
        /// <param name="auditOperation"></param>
        public Task SaveAuditOperation(AuditOperation auditOperation);
    }
}