// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using System;
using System.Threading.Tasks;
using Gardener.Client.Core;

namespace Gardener.Client.Services
{
    /// <summary>
    /// 审计数据服务
    /// </summary>
    [ScopedService]
    public class AuditEntityService : ApplicationServiceBase<AuditEntityDto, Guid>, IAuditEntityService
    {
        private readonly static string controller = "audit-entity";
        private readonly IApiCaller apiCaller;
        public AuditEntityService(IApiCaller apiCaller) : base(apiCaller, controller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<PagedList<AuditEntityDto>> Search(AuditEntitySearchInput searchInput)
        {
            return await apiCaller.PostAsync<AuditEntitySearchInput, PagedList<AuditEntityDto>>($"{controller}/search", searchInput);
        }
    }
}
