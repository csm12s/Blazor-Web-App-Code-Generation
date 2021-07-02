// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Gardener.Client.Core;

namespace Gardener.Client.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class NewsService : ApplicationServiceBase<NewsDto, long>, INewsService
    {
        private static readonly string controller = "news";
        public NewsService(IApiCaller apiCaller) : base(apiCaller, controller)
        {
        }
    }
}
