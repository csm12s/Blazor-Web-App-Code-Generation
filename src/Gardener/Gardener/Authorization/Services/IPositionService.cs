// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Dtos;
using Gardener.Base;

namespace Gardener.Application.Interfaces
{
    public interface IPositionService : IApplicationServiceBase<PositionDto, int>
    {
    }
}
