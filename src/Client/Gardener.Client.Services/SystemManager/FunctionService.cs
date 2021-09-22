// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Gardener.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Gardener.Client.Core;

namespace Gardener.Client.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class FunctionService : ApplicationServiceBase<FunctionDto, Guid>, IFunctionService
    {
        private readonly static string controller = "function";
        private readonly IApiCaller apiCaller;
        public FunctionService(IApiCaller apiCaller) : base(apiCaller, controller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<bool> EnableAudit(Guid id, bool enableAudit=true)
        {
            return await apiCaller.PutAsync<bool, bool>($"{controller}/{id}/enable-audit/{enableAudit}");
        }

        public async Task<bool> Exists(HttpMethodType method, string path)
        {
            path=HttpUtility.UrlEncode(path);
            return await apiCaller.GetAsync<bool>($"{controller}/exists/{method}/{path}");
        }

        public async Task<string> GetSeedData()
        {
            return await apiCaller.GetAsync<string>($"{controller}/seed-data");
        }
    }
}
