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

namespace Gardener.Client.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class FunctionService : ApplicationServiceBase<FunctionDto, Guid>, IFunctionService
    {
        private readonly static string controller = "function";
        private readonly IApiCaller apiCaller;
        public FunctionService(IApiCaller apiCaller) : base(apiCaller, controller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<List<FunctionDto>> GetEffective()
        {
            return await apiCaller.GetAsync<List<FunctionDto>>($"{controller}/effective");
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

        public async Task<PagedList<FunctionDto>> Search(FunctionSearchInput searchInput)
        {
            return await apiCaller.PostAsync<FunctionSearchInput, PagedList<FunctionDto>>($"{controller}/search", searchInput);
        }
    }
}
