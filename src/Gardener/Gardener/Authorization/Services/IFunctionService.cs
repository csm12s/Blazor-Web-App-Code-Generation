// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authorization.Dtos;
using Gardener.Base;
using Gardener.Enums;
using System;
using System.Threading.Tasks;

namespace Gardener.Authorization.Services
{
    public interface IFunctionService : IApplicationServiceBase<FunctionDto, Guid>
    {
        /// <summary>
        /// 启用禁用审计
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enableAudit"></param>
        /// <returns></returns>
        Task<bool> EnableAudit(Guid id, bool enableAudit = true);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <remarks>
        /// 根据 HttpMethod 和 path 判断是否存在
        /// </remarks>
        /// <param name="method"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<bool> Exists(HttpMethod method, string path);

        /// <summary>
        /// 获取种子数据
        /// </summary>
        /// <remarks>
        /// 获取种子数据
        /// </remarks>
        /// <returns></returns>
        Task<string> GetSeedData();

        /// <summary>
        /// 根据path,method获取功能点
        /// </summary>
        /// <param name="path"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        Task<FunctionDto> Query(string path, HttpMethod method);

        /// <summary>
        /// 根据key获取功能点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<FunctionDto> Query(string key);
    }
}
