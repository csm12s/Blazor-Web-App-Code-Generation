// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Domain;
using System.Threading.Tasks;

namespace Gardener.Authorization
{
    /// <summary>
    /// 权限管理器
    /// </summary>
    public interface IAuthorizationManager
    {
        /// <summary>
        /// 获取身份
        /// </summary>
        /// <returns></returns>
        Identity GetIdentity();
        /// <summary>
        /// 获取当前请求的功能
        /// </summary>
        /// <returns></returns>
        Task<FunctionDto> GetContenxtFunction();
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <returns></returns>
        Task<bool> ChecktContenxtFunction();
    }
}