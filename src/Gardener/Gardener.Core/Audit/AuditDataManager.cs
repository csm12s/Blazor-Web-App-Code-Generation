// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Gardener.Core.Entites;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Core.Audit
{
    /// <summary>
    /// 当前请求的审计数据管理
    /// </summary>
    public class AuditDataManager : IAuditDataManager, IScoped
    {
        private readonly ILogger<AuditDataManager> _logger;
        private readonly IRepository<AuditOperation> _auditOperationRepository;
        private readonly IRepository<AuditEntity> _auditEntityRepository;
        private AuditOperation _auditOperation;
        private List<AuditEntity> _auditEntitys;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AuditOperation GetAuditOperation()
        {
            return this._auditOperation;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="auditOperationRepository"></param>
        /// <param name="auditEntityRepository"></param>
        public AuditDataManager(ILogger<AuditDataManager> logger,
            IRepository<AuditOperation> auditOperationRepository,
            IRepository<AuditEntity> auditEntityRepository)
        {
            _logger = logger;
            _auditOperationRepository = auditOperationRepository;
            _auditEntityRepository = auditEntityRepository;
        }
        /// <summary>
        /// 保存操作审计
        /// </summary>
        /// <param name="auditOperation"></param>
        public async Task SaveAuditOperation(AuditOperation auditOperation)
        {
            if (auditOperation == null) return;
            _logger.LogDebug($"写入操作审计信息 {auditOperation.OperaterName} {auditOperation.ResourceName}");
            _auditOperation = auditOperation;
            try
            {
               await _auditOperationRepository.InsertNowAsync(auditOperation);
            }
            catch (Exception ex)
            {
               _logger.LogError(ex, "操作审计写入数据库异常");
            }
        }
        /// <summary>
        /// 获取 实体审计数据
        /// </summary>
        /// <returns></returns>
        public List<AuditEntity> GetAuditEntities()
        {
            return this._auditEntitys;
        }
        /// <summary>
        /// 设置实体审计数据
        /// </summary>
        /// <param name="auditEntitys"></param>
        /// <returns></returns>
        public void SetAuditEntitys(List<AuditEntity> auditEntitys)
        {
            this._auditEntitys = auditEntitys;
        }
        /// <summary>
        /// 保存实体审计数据
        /// </summary>
        /// <param name="auditEntitys"></param>
        /// <returns></returns>
        public async Task SaveAuditEntitys(List<AuditEntity> auditEntitys)
        {
            if (auditEntitys == null) return;
            if (this._auditOperation != null) 
            {
                auditEntitys.ForEach(x => {
                    x.OperationId = _auditOperation.Id;
                    _logger.LogDebug($"写入操作审计信息 {x.Name} {x.OperationType.ToString()}");
                });
            }
            try
            {
               await _auditEntityRepository.InsertNowAsync(auditEntitys);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "实体审计写入数据库异常");
            }
        }
    }
}
