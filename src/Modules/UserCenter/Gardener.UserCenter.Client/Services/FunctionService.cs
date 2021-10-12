// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;
using System;
using System.Threading.Tasks;
using System.Web;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Gardener.Client.Base;

namespace Gardener.UserCenter.Client.Services
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

        public async Task<FunctionDto> GetByKey(string key)
        {
            return await apiCaller.GetAsync<FunctionDto>($"{controller}/by-key/{key}");
        }

        public async Task<string> GetSeedData()
        {
            return await apiCaller.GetAsync<string>($"{controller}/seed-data");
        }
    }
}
