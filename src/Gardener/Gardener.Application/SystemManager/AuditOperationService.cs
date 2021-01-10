// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Gardener.Core.Entites;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Application.SystemManager
{
    /// <summary>
    /// 审计操作服务
    /// </summary>
    [ApiDescriptionSettings("BaseDataServices")]
    public class AuditOperationService : ApplicationServiceBase<AuditOperation, AuditOperationDto, Guid>, IAuditOperationService
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
        /// 搜索
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        [HttpPost]
        [NonValidation]
        public async Task<Dtos.PagedList<AuditOperationDto>> Search(AuditOperationSearchInput searchInput)
        {
            IQueryable<AuditOperation> queryable = _repository.Where(x => x.IsDeleted == false);
            AuditOperationDto search = searchInput.SearchData;
            return await queryable.OrderConditions(searchInput.OrderConditions).Select(x => x.Adapt<AuditOperationDto>()).ToPagedListAsync(searchInput);
        }
        /// <summary>
        /// 根据操作审计ID获取数据审计数据
        /// </summary>
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
