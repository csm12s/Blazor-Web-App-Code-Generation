// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;

namespace Gardener.Application.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface INewsService : IApplicationServiceBase<NewsDto, long>
    {

    }
}
