// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Dtos;
using System.Threading.Tasks;

namespace Gardener.Authorization.Core
{
    public interface IIdentityPermissionService
    {
        /// <summary>
        /// 检测是否有该功能点的使用权限
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        Task<bool> Check(Identity identity, FunctionDto function);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        object GetIdentityId(Identity identity);
    }
}
