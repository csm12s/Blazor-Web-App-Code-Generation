// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Web;
using Gardener.Client.Base;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Services;
using HttpMethod = Gardener.Enums.HttpMethod;

namespace Gardener.SystemManager.Client.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class FunctionService : ClientServiceBase<FunctionDto, Guid>, IFunctionService
    {
        public FunctionService(IApiCaller apiCaller) : base(apiCaller, "function")
        {
        }

        public async Task<bool> EnableAudit(Guid id, bool enableAudit=true)
        {
            return await apiCaller.PutAsync<bool, bool>($"{controller}/{id}/enable-audit/{enableAudit}");
        }

        public async Task<bool> Exists(HttpMethod method, string path)
        {
            path=HttpUtility.UrlEncode(path);
            return await apiCaller.GetAsync<bool>($"{controller}/exists/{method}/{path}");
        }

        public async Task<FunctionDto?> GetByKey(string key)
        {
            return await apiCaller.GetAsync<FunctionDto>($"{controller}/by-key/{key}");
        }
    }
}
