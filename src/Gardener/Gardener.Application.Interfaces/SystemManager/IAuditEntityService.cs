// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace Gardener.Application.Interfaces
{
    /// <summary>
    /// 审计数据服务接口
    /// </summary>
    public interface IAuditEntityService : IApplicationServiceBase<AuditEntityDto, Guid>
    {
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="searchInput"></param>
        /// <returns></returns>
        public Task<PagedList<AuditEntityDto>> Search(AuditEntitySearchInput searchInput);
    }
}
