// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.UserCenter.Dtos;
using Gardener.Base;

namespace Gardener.UserCenter.Services
{
    /// <summary>
    /// 岗位服务接口
    /// </summary>
    public interface IPositionService : IServiceBase<PositionDto, int>
    {
    }
}
