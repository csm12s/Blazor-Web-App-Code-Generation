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
using System.Threading.Tasks;

namespace Gardener.Application.SystemManager
{
    /// <summary>
    /// 审计数据服务
    /// </summary>
    [ApiDescriptionSettings("BaseDataServices")]
    public class AuditEntityService : ApplicationServiceBase<AuditEntity, AuditEntityDto, Guid>, IAuditEntityService
    {
        private readonly IRepository<AuditEntity> _repository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public AuditEntityService(IRepository<AuditEntity> repository) : base(repository)
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
        public async Task<PagedList<AuditEntityDto>> Search(AuditEntitySearchInput searchInput)
        {
            IQueryable<AuditEntity> queryable = _repository
                .Include(x=>x.AuditProperties)
                .Where(x => x.IsDeleted == false);
            AuditEntityDto search = searchInput.SearchData;
            return await queryable
                .OrderConditions(searchInput.OrderConditions)
                .Select(x => x.Adapt<AuditEntityDto>())
                .ToPagedListAsync(searchInput);
        }
       
    }
}
