// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.UserCenter.Dtos;
using Gardener.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Services
{
    /// <summary>
    /// 部门服务
    /// </summary>
    public interface IDeptService : IServiceBase<DeptDto, int>
    {
        /// <summary>
        /// 查询所有部门 按树形结构返回
        /// </summary>
        /// <returns></returns>
        Task<List<DeptDto>> GetTree(bool includeLocked = false);

        /// <summary>
        /// 获取资源的种子数据
        /// </summary>
        /// <returns></returns>
        Task<string> GetSeedData();
    }
}
