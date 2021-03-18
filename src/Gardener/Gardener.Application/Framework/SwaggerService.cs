// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DynamicApiController;
using Furion.RemoteRequest.Extensions;
using Furion.SpecificationDocument;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Gardener.Application
{
    /// <summary>
    /// Swagger服务
    /// </summary>
    [ApiDescriptionSettings("BaseServices")]
    public class SwaggerService : ISwaggerService, IDynamicApiController
    {


        /// <summary>
        /// 解析api json
        /// </summary>
        /// <remarks> swagger json 文件解析功能</remarks>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<SwaggerModel> Analysis(string url)
        {
            url = HttpUtility.UrlDecode(url);

            var swaggerInfo = await url.GetAsAsync<SwaggerModel>();

            return swaggerInfo;
        }
        /// <summary>
        /// 获取哦 swagger 配置
        /// </summary>
        /// <remarks>
        /// 获取api分组设置
        /// </remarks>
        /// <returns></returns>
        public async Task<List<SwaggerSpecificationOpenApiInfoDto>> GetApiGroup()
        {
            // 载入配置
            SpecificationDocumentSettingsOptions options= App.GetOptions<SpecificationDocumentSettingsOptions>();
            if (options == null) return null;
            return options.GroupOpenApiInfos.Select(x => x.Adapt<SwaggerSpecificationOpenApiInfoDto>()).ToList();
        }
    }
}
