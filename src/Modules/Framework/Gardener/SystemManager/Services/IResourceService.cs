// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.SystemManager.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.SystemManager.Services
{
    /// <summary>
    /// 资源服务
    /// </summary>
    public interface IResourceService : IServiceBase<ResourceDto, Guid>
    {
        /// <summary>
        /// 获取所有子资源
        /// </summary>
        /// <param name="id">父id</param>
        /// <returns></returns>
        Task<List<ResourceDto>> GetChildren(Guid id);

        /// <summary>
        /// 返回根节点
        /// </summary>
        /// <returns></returns>
        Task<List<ResourceDto>> GetRoot();

        /// <summary>
        /// 查询所有资源 按树形结构返回
        /// </summary>
        /// <param name="includLocked">是否包含锁定的资源</param>
        /// <param name="rootKey"></param>
        /// <returns></returns>
        Task<List<ResourceDto>> GetTree(bool includLocked = true,string rootKey = null);
        

        /// <summary>
        /// 根据资源id获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<FunctionDto>> GetFunctions(Guid id);

    }
}