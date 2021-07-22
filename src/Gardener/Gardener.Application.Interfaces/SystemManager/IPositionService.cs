// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Application.Interfaces.SystemManager
{
    public interface IPositionService : IApplicationServiceBase<PositionDto, Guid>
    {
        Task<PagedList<PositionDto>> Search(string name, int pageIndex = 1, int pageSize = 10);
    }
}
