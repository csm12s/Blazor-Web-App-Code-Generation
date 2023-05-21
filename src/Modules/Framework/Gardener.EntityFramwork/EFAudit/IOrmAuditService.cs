// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Entity.Domains;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.EntityFramwork.EFAudit
{
    /// <summary>
    /// Orm审计服务接口
    /// </summary>
    public interface IOrmAuditService
    {
        /// <summary>
        /// 数据保存前
        /// </summary>
        /// <param name="entitys"></param>
        public void SavingChangesEvent(IEnumerable<EntityEntry> entitys);
        /// <summary>
        /// 数据保存后
        /// </summary>
        public Task SavedChangesEvent();

        /// <summary>
        /// 保存操作审计
        /// </summary>
        /// <param name="auditOperation"></param>
        public Task SaveAuditOperation(AuditOperation auditOperation);

    }
}
