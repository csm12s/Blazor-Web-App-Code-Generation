// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Audit.Dtos;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Audit.Core
{
    public interface IAuditService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        public Task SavingChangesEvent(DbContextEventData eventData);
        /// <summary>
        /// 数据保存后
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        public Task SavedChangesEvent(SaveChangesCompletedEventData eventData);

        /// <summary>
        /// 保存操作审计
        /// </summary>
        /// <param name="auditOperation"></param>
        public Task SaveAuditOperation(AuditOperation auditOperation);

    }
}
