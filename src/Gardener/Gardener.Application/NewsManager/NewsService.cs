// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Gardener.Core.Entites;
using Microsoft.AspNetCore.Mvc;

namespace Gardener.Application
{
    /// <summary>
    /// 新闻管理
    /// </summary>
    [ApiDescriptionSettings("NewsManagerServices")]
    public class NewsService : ApplicationServiceBase<News, NewsDto, long>, INewsService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public NewsService(IRepository<News> repository) : base(repository)
        {
        }
    }
}
