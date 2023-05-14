// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;

namespace Gardener.UserCenter.Client.Services
{
    /// <summary>
    /// 
    /// </summary>
    [ScopedService]
    public class ClientFunctionService : IClientFunctionService
    {
        private readonly static string controller = "client-function";
        private readonly IApiCaller apiCaller;
        public ClientFunctionService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }
        public async Task<bool> Add(List<ClientFunctionDto> clientFunctionDtos)
        {
            return await apiCaller.PostAsync<List<ClientFunctionDto>, bool>($"{controller}", clientFunctionDtos);
        }

        public async Task<bool> Delete(Guid clientId, Guid functionId)
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/{clientId}/{functionId}");
        }
    }
}
