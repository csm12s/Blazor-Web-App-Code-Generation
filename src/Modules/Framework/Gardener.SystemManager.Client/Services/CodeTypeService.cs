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

        public Task<Dictionary<int, List<CodeDto>>> GetCodeDic(params int[] codeTypeIds)
        {
            IDictionary<string, object?> queryString=new Dictionary<string, object?>();
            queryString.Add("codeTypeIds", codeTypeIds);
            return base.apiCaller.GetAsync<Dictionary<int, List<CodeDto>>>($"{controller}/code-dic", queryString);
        }

        public Task<List<CodeDto>> GetCodes(int codeTypeId)
        {
            return base.apiCaller.GetAsync<List<CodeDto>>($"{controller}/{codeTypeId}/codes");
        }
    }
}
