// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Gardener.Client.Services
{
    public class SwaggerService : ISwaggerService
    {
        private readonly static string controller = "swagger";
        private readonly IApiCaller apiCaller;

        public SwaggerService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<SwaggerModel> Analysis(string url)
        {
            url = HttpUtility.UrlEncode(url);
            return await apiCaller.GetAsync<SwaggerModel>($"{controller}/analysis/{url}");
        }

        public async Task<List<SwaggerSpecificationOpenApiInfoDto>> GetApiGroup()
        {
            return await apiCaller.GetAsync<List<SwaggerSpecificationOpenApiInfoDto>>($"{controller}/api-group");
        }
    }
}
