// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Gardener.SystemManager.Services;
using Gardener.SystemManager.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Gardener.SystemManager
{
    /// <summary>
    /// 配置启动时操作
    /// </summary>
    [AppStartup(500)]
    public sealed class SystemManagerStartup : AppStartup
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ILogger<SystemManagerStartup> logger = app.ApplicationServices.GetRequiredService<ILogger<SystemManagerStartup>>();
            try
            {
                IServiceScopeFactory serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
                using var serviceScope = serviceScopeFactory.CreateScope();
                //启动时初始化 CodeUtil 所有code 缓存
                var codeTypeService = serviceScope.ServiceProvider.GetRequiredService<ICodeTypeService>();
                var codes = await codeTypeService.GetCodeDicByValues();
                CodeUtil.InitAllCode(codes);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "初始化Code异常");
            }
        }
    }
}
