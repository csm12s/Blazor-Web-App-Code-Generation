// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gardener.Audit.Dtos;
using Gardener.Audit.Services;
using Gardener.Client.Base;

namespace Gardener.Audit.Client.Services
{
    [ScopedService]
    public class AuditOperationService : ClientServiceBase<AuditOperationDto, Guid>, IAuditOperationService
    {
        public AuditOperationService(IApiCaller apiCaller) : base(apiCaller, "audit-operation")
        {
        }

        public async Task<List<AuditEntityDto>> GetAuditEntity(Guid operationId)
        {
            return await apiCaller.GetAsync<List<AuditEntityDto>>($"{controller}/{operationId}/audit-entity");
        }
    }
}
