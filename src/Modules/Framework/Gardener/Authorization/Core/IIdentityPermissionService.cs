// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Dtos;
using Gardener.Authorization.Dtos;
using System.Threading.Tasks;

namespace Gardener.Authorization.Core
{
    /// <summary>
    /// 身份权限服务
    /// </summary>
    public interface IIdentityPermissionService
    {
        /// <summary>
        /// 检测是否有该功能点的使用权限
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="api"></param>
        /// <returns></returns>
        Task<bool> Check(Identity identity, ApiEndpoint api);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        object GetIdentityId(Identity identity);
    }
}
