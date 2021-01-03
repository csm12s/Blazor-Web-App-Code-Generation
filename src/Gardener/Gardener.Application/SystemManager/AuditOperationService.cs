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
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Application.SystemManager
{
    /// <summary>
    /// 审计操作服务
    /// </summary>
    [ApiDescriptionSettings("BaseDataServices")]
    public class AuditOperationService : ServiceBase<AuditOperation, AuditOperationDto, Guid>, IAuditOperationService
    {
        private readonly IRepository<AuditOperation> _repository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public AuditOperationService(IRepository<AuditOperation> repository) : base(repository)
        {
            this._repository = repository;
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        [HttpPost]
        [NonValidation]
        public async Task<PagedList<AuditOperationDto>> Search(AuditOperationSearchInput searchInput)
        {
            IQueryable<AuditOperation> queryable = _repository.Where(x => x.IsDeleted == false);
            AuditOperationDto search = searchInput.SearchData;
            return await queryable.OrderConditions(searchInput.OrderConditions).Select(x => x.Adapt<AuditOperationDto>()).ToPagedListAsync(searchInput);
        }
    }
}
