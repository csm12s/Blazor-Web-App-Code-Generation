// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.Entites;
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
        /// 获取 实体审计数据
        /// </summary>
        /// <returns></returns>
        public List<AuditEntity> GetAuditEntities();
        /// <summary>
        /// 设置实体审计数据
        /// </summary>
        /// <param name="auditEntitys"></param>
        /// <returns></returns>
        public void SetAuditEntitys(List<AuditEntity> auditEntitys);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="auditEntitys"></param>
        /// <returns></returns>
        public Task SaveAuditEntitys(List<AuditEntity> auditEntitys);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="auditOperation"></param>
        /// <returns></returns>
        public Task SaveAuditOperation(AuditOperation auditOperation);
    }
}