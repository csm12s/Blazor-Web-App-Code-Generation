// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Services;

namespace Gardener.SystemManager.Client.Services
{
    /// <summary>
    /// 字典类型
    /// </summary>
    [ScopedService]
    public class CodeTypeService : ClientServiceBase<CodeTypeDto>, ICodeTypeService
    {
        public CodeTypeService(IApiCaller apiCaller) : base(apiCaller, "code-type")
        {
        }
    }
}
