// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Audit.Dtos;
using Gardener.Audit.Services;
using Gardener.Client.Base;
using System;

namespace Gardener.Audit.Client.Services
{
    /// <summary>
    /// 审计数据服务
    /// </summary>
    [ScopedService]
    public class AuditEntityService : ClientServiceBase<AuditEntityDto, Guid>, IAuditEntityService
    {
        public AuditEntityService(IApiCaller apiCaller) : base(apiCaller, "audit-entity")
        {
        }
    }
}
