// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Audit.Dtos;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Audit.Services
{
    /// <summary>
    /// 审计操作服务
    /// </summary>
    [ApiDescriptionSettings("SystemManagerServices")]
    public class AuditOperationService : ServiceBase<AuditOperation, AuditOperationDto, Guid>, IAuditOperationService
    {
        private readonly IRepository<AuditOperation> _repository;
        private readonly IRepository<AuditEntity> _auditEntityRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="auditEntityRepository"></param>
        public AuditOperationService(IRepository<AuditOperation> repository, IRepository<AuditEntity> auditEntityRepository) : base(repository)
        {
            this._repository = repository;
            _auditEntityRepository = auditEntityRepository;
        }
        
        /// <summary>
        /// 根据操作审计ID获取数据审计
        /// </summary>
        /// <remarks>
        /// 根据操作审计ID获取数据审计
        /// </remarks>
        /// <param name="operationId"></param>
        /// <returns></returns>
        public async Task<List<AuditEntityDto>> GetAuditEntity([ApiSeat(ApiSeats.ActionStart)] Guid operationId)
        {
            return await _auditEntityRepository
                .Include(x => x.AuditProperties)
                .Where(x => x.IsDeleted == false && x.OperationId==operationId).Select(x => x.Adapt<AuditEntityDto>()).ToListAsync();
        }
    }
}
