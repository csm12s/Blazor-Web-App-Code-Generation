// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Furion.RemoteRequest.Extensions;
using Furion.SpecificationDocument;
using Gardener.Authorization.Dtos;
using Gardener.Common;
using Gardener.Enums;
using Gardener.Swagger.Dtos;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Gardener.Swagger.Services
{
    /// <summary>
    /// Swagger服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class SwaggerService : ISwaggerService, IDynamicApiController
    {


        /// <summary>
        /// 解析api json
        /// </summary>
        /// <remarks> swagger json 文件解析功能</remarks>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<SwaggerModel?> Analysis(string url)
        {
            url = HttpUtility.UrlDecode(url);

            var swaggerInfo = await url.OnException((httpClient, response, msg) =>
            {

                if (!response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    throw Oops.Bah(ExceptionCode.Request_Url_Is_Invalid);
                }

            }).GetAsAsync<SwaggerModel>();

            return swaggerInfo;
        }
        /// <summary>
        /// 获取 swagger 配置
        /// </summary>
        /// <remarks>
        /// 获取api分组设置
        /// </remarks>
        /// <returns></returns>
        public Task<List<SwaggerSpecificationOpenApiInfoDto>> GetApiGroup()
        {
            // 载入配置
            SpecificationDocumentSettingsOptions options = App.GetOptions<SpecificationDocumentSettingsOptions>();
            if (options == null) return Task.FromResult(new List<SwaggerSpecificationOpenApiInfoDto>(0));
            return Task.FromResult(options.GroupOpenApiInfos.Select(x => x.Adapt<SwaggerSpecificationOpenApiInfoDto>()).ToList());
        }
        /// <summary>
        /// 从json中获取function
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<List<ApiEndpoint>> GetFunctionsFromJson(string url)
        {
            List<ApiEndpoint> _functionDtos = new List<ApiEndpoint>();
            SwaggerModel? swaggerModel = await Analysis(url);
            if (swaggerModel != null && swaggerModel.paths != null)
            {
                if (swaggerModel.tags == null)
                {
                    throw Oops.Bah(ExceptionCode.Controller_Need_Comment);
                }
                Dictionary<string, SwaggerTagInfo> tagMap = swaggerModel.tags.ToDictionary<SwaggerTagInfo, string>(x => x.name);
                foreach (var item in swaggerModel.paths)
                {
                    foreach (var m in item.Value)
                    {
                        string? tags = m.Value.tags == null ? null : string.Join("_", m.Value.tags.Select(x => tagMap.ContainsKey(x) ? tagMap[x].description : x));
                        ApiEndpoint function = new ApiEndpoint()
                        {
                            Path = item.Key,
                            Method = (HttpMethod)Enum.Parse(typeof(HttpMethod), m.Key.ToUpper()),
                            Key = MD5Encryption.Encrypt(item.Key + m.Key.ToUpper()),
                            Summary = m.Value.summary,
                            Description = m.Value.description,
                            //Group = _selectedGroup.Title,
                            Service = tags ?? "未知",
                            EnableAudit = true
                        };

                        // Get function name if not set in comment
                        var functionName = function.Path.Split("/").LastOrDefault();
                        if (function.Summary == null)
                        {
                            function.Summary = functionName;
                        }
                        if (function.Description == null)
                        {
                            function.Description = functionName;
                        }

                        if (HttpMethod.GET.Equals(function.Method))
                        {
                            function.EnableAudit = false;
                        }
                        _functionDtos.Add(function);
                    }
                }
            }
            return _functionDtos;
        }
    }
}
