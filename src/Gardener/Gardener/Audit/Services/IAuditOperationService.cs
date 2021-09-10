// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Audit.Dtos;
using Gardener.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Audit.Services
{
    /// <summary>
    /// 审计操作服务
    /// </summary>
    public interface IAuditOperationService : IApplicationServiceBase<AuditOperationDto, Guid>
    {
        /// <summary>
        /// 根据操作审计ID获取数据审计数据
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        Task<List<AuditEntityDto>> GetAuditEntity(Guid operationId);
    }
}
