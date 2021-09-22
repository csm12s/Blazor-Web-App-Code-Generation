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
    /// 终结点存储服务
    /// </summary>
    public interface IApiEndpointStoreService
    {

        /// <summary>
        /// 根据path,method获取功能点
        /// </summary>
        /// <param name="path"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        Task<ApiEndpoint> Query(string path, HttpMethod method);

        /// <summary>
        /// 根据key获取功能点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<ApiEndpoint> Query(string key);
    }
}
