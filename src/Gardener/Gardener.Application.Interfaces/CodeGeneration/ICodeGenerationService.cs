// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Application.Interfaces
{
    /// <summary>
    /// 代码生成服务
    /// </summary>
    public interface ICodeGenerationService
    {
        /// <summary>
        /// 获取所有实体定义
        /// </summary>
        /// <returns></returns>
        Task<List<EntityDefinitionDto>> GetEntityDefinitions();
    }
}
