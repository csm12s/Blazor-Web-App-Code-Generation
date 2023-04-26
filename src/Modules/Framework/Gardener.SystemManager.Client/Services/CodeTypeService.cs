// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.SystemManager.Client.Pages.CodeView;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Services;
using Gardener.SystemManager.Utils;

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

        public Task<Dictionary<int, IEnumerable<CodeDto>>> GetCodeDic(params int[] codeTypeIds)
        {
            List<KeyValuePair<string, object?>> queryString = new List<KeyValuePair<string, object?>>();
            foreach (var item in codeTypeIds)
            {
                queryString.Add(new KeyValuePair<string, object?>("codeTypeIds", item));
            }
            return base.apiCaller.GetAsync<Dictionary<int, IEnumerable<CodeDto>>>($"{controller}/code-dic", queryString);
        }

        public Task<Dictionary<string, IEnumerable<CodeDto>>> GetCodeDicByValues(params string[] codeTypeValues)
        {
            List<KeyValuePair<string, object?>> queryString = new List<KeyValuePair<string, object?>>();
            foreach (var item in codeTypeValues)
            {
                queryString.Add(new KeyValuePair<string, object?>("codeTypeValues", item));
            }
            return base.apiCaller.GetAsync<Dictionary<string, IEnumerable<CodeDto>>>($"{controller}/code-dic-by-values", queryString);
        }

        public Task<IEnumerable<CodeDto>> GetCodes(int codeTypeId)
        {
            return base.apiCaller.GetAsync<IEnumerable<CodeDto>>($"{controller}/{codeTypeId}/codes");
        }

        public Task<IEnumerable<CodeDto>> GetCodesByValue(string codeTypeValue)
        {
            return base.apiCaller.GetAsync<IEnumerable<CodeDto>>($"{controller}/{codeTypeValue}/codes-by-value");
        }

        public async Task<bool> RefreshCodeUtilCache()
        {
            //服务端刷新
            var task1 = base.apiCaller.PostWithoutBodyAsync<bool>($"{controller}/refresh-code-util-cache");
            //客户端刷新
            var task2 = CodeUtil.InitAllCode();
            await Task.WhenAll(task1, task2);
            return true;
        }
    }
}
