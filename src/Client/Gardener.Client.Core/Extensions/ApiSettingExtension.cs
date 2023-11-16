// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Microsoft.Extensions.Configuration;
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
        /// <summary>
        /// 设置Api信息
        /// </summary>
        /// <param name="services"></param>
        /// <param name="hostBaseAddress"></param>
        public static void AddApiSetting(this IServiceCollection services, string? hostBaseAddress = null)
        {
            var serviceProvider = services.BuildServiceProvider();
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            services.AddOptions<ApiSettings>().Bind(configuration.GetSection("ApiSettings"));
            services.PostConfigure<ApiSettings>(setting => 
            {
                ConfigureApiSettings(setting, hostBaseAddress);
            });
        }
        /// <summary>
        /// 根据运行环境进行配置
        /// </summary>
        /// <returns></returns>
        private static void ConfigureApiSettings(ApiSettings apiSettings, string? hostBaseAddress = null)
        {
            string? host = apiSettings.Host;
            string? port = apiSettings.Port;
            string? uploadUrl = apiSettings.UploadPath;
            string? basePath = apiSettings.BasePath;
            if (!string.IsNullOrEmpty(hostBaseAddress))
            {
                Uri baseUri = new Uri(hostBaseAddress);
                if (string.IsNullOrEmpty(host))
                {
                    host = baseUri.Host;
                }
                if (string.IsNullOrEmpty(port))
                {
                    port = baseUri.Port.ToString();
                }
                if (host.IndexOf("http://") < 0 && host.IndexOf("https://") < 0)
                {
                    host = baseUri.Scheme + "://" + host;
                }
            }
            apiSettings.Host = host;
            apiSettings.Port = port;
            apiSettings.BasePath = basePath;
            apiSettings.UploadPath = uploadUrl;
        }
    }
}
