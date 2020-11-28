// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    /// <summary>
    /// 资源服务
    /// </summary>
    public interface IResourceService:IServiceBase<ResourceDto>
    {
        /// <summary>
        /// 获取所有子资源
        /// </summary>
        /// <param name="id">父id</param>
        /// <returns></returns>
        Task<ApiResult<List<ResourceDto>>> GetChildren(int id);
        /// <summary>
        /// 返回根节点
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<ResourceDto>>> GetRoot();

        /// <summary>
        /// 查询所有资源 按树形结构返回
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<ResourceDto>>> GetTree();
    }
}