// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Application.UserCenter
{
    /// <summary>
    /// 资源服务
    /// </summary>
    public interface IResourceService
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
        /// <returns></returns>
        Task<List<ResourceDto>> GetTree();
    }
}