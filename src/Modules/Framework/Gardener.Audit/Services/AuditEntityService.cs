// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Gardener.Audit.Dtos;
using Gardener.Base;
using Gardener.Base.Entity;
using Gardener.Base.Entity.Domains;
using Gardener.EntityFramwork;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Gardener.Audit.Services
{
    /// <summary>
    /// 审计数据服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class AuditEntityService : ServiceBase<AuditEntity, AuditEntityDto, Guid, GardenerMultiTenantDbContextLocator>, IAuditEntityService
    {
        private readonly IRepository<AuditEntity, GardenerMultiTenantDbContextLocator> _auditRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public AuditEntityService(IRepository<AuditEntity, GardenerMultiTenantDbContextLocator> repository) : base(repository)
        {
            this._auditRepository = repository;
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <remarks>
        /// 搜索数据
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<PagedList<AuditEntityDto>> Search(PageRequest request)
        {
            IDynamicFilterService filterService = App.GetService<IDynamicFilterService>();

            Expression<Func<AuditEntity, bool>> expression = filterService.GetExpression<AuditEntity>(request.FilterGroups);

            IQueryable<AuditEntity> queryable = _auditRepository
                .AsQueryable(false)
                .Include(x=>x.AuditProperties)
                .Where(expression);
            return await queryable
                .OrderConditions(request.OrderConditions)
                .Select(x => x.Adapt<AuditEntityDto>())
                .ToPageAsync(request.PageIndex, request.PageSize);
        }
    }
}
