// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Dtos;
using Gardener.Enums;
using System.Threading.Tasks;

namespace Gardener.Authorization.Core
{
    /// <summary>
    /// 接口查询服务
    /// </summary>
    public interface IApiEndpointQueryService
    {
        /// <summary>
        /// 清除缓存key
        /// </summary>
        /// <param name="apiEndpoint"></param>
        /// <returns></returns>
        Task ClearApiEndpointCacheKey(ApiEndpoint apiEndpoint);
        /// <summary>
        /// 根据path,method获取功能点
        /// </summary>
        /// <param name="path"></param>
        /// <param name="method"></param>
        /// <param name="enableCache"></param>
        /// <returns></returns>
        Task<ApiEndpoint> Query(string path, HttpMethod method,bool enableCache=true);
        /// <summary>
        /// 根据key获取功能点
        /// </summary>
        /// <param name="key"></param>
        /// <param name="enableCache"></param>
        /// <returns></returns>
        Task<ApiEndpoint> Query(string key, bool enableCache = true);
    }
}
