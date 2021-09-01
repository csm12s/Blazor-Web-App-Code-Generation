// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Gardener.Client.Core
{
    /// <summary>
    /// 装配ApiSetting
    /// host 可以缺省，缺省后获取的是client运行的host
    /// port uploadUrl basePath 对应api进行配置
    /// </summary>
    public static class ApiSettingExtension
    {
        public static void AddApiSetting(this WebAssemblyHostBuilder builder)
        {
            builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
            string host = builder.Configuration.GetSection("ApiSettings:Host")?.Value;
            string port = builder.Configuration.GetSection("ApiSettings:Port")?.Value;
            string uploadUrl= builder.Configuration.GetSection("ApiSettings:UploadUrl")?.Value;
            string basePath = builder.Configuration.GetSection("ApiSettings:BasePath")?.Value;
            Uri baseUri = new Uri(builder.HostEnvironment.BaseAddress);
            if (string.IsNullOrEmpty(host))
            {
                host = baseUri.Host;
            }
            if (string.IsNullOrEmpty(port))
            {
                port = baseUri.Port.ToString();
            }
            builder.Services.AddSingleton(typeof(ApiSettings), sp => {
                return new ApiSettings { 
                    Host = host,
                    Port = port,
                    BasePath= basePath,
                    UploadPath = uploadUrl };
            });
        }
    }
}
