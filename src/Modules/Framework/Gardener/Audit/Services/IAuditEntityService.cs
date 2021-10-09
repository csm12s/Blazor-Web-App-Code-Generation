// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Audit.Dtos;
using Gardener.Base;
using System;

namespace Gardener.Audit.Services
{
    /// <summary>
    /// 审计数据服务接口
    /// </summary>
    public interface IAuditEntityService : IServiceBase<AuditEntityDto, Guid>
    {
        
    }
}
