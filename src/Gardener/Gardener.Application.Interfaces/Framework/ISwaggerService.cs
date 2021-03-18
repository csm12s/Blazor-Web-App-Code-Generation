// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Application.Interfaces
{
    /// <summary>
    /// swagger 服务
    /// </summary>
    public interface ISwaggerService
    {
        /// <summary>
        /// 解析open api json
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<SwaggerModel> Analysis(string url);

        /// <summary>
        /// 获取哦 swagger 配置
        /// </summary>
        /// <returns></returns>
        Task<List<SwaggerSpecificationOpenApiInfoDto>> GetApiGroup();
    }
}
