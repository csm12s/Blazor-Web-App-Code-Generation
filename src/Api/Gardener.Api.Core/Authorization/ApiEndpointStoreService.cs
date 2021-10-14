// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authorization.Dtos;
using Gardener.Enums;
using Gardener.UserCenter.Impl.Domains;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Authorization.Core
{
    public class ApiEndpointStoreService : IApiEndpointStoreService
    {
        /// <summary>
        /// 根据key获取功能点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<ApiEndpoint> Query(string key)
        {
            Function function = await Db.GetRepository<Function>().AsQueryable(false).Where(x => x.Key.Equals(key) && x.IsDeleted==false && x.IsLocked==false).FirstOrDefaultAsync();
            return function?.Adapt<ApiEndpoint>();
        }

        /// <summary>
        /// 根据path,method获取功能点
        /// </summary>
        /// <param name="path"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public async Task<ApiEndpoint> Query(string path, HttpMethod method)
        {
            Function function = await Db.GetRepository<Function>().AsQueryable(false).Where(x => x.Method.Equals(method) && x.Path.Equals(path) && x.IsDeleted == false && x.IsLocked == false).FirstOrDefaultAsync();
            return function?.Adapt<ApiEndpoint>();
        }
    }
}
