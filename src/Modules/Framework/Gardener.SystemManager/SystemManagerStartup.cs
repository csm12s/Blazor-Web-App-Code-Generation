// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DependencyInjection;
using Gardener.EntityFramwork.DbContexts;
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
    [AppStartup(900)]
    public sealed class SystemManagerStartup : AppStartup
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Scoped.Create(async (_, scope) =>
            {
                //启动时初始化 CodeUtil 所有code 缓存
                var codeTypeService = scope.ServiceProvider.GetRequiredService<ICodeTypeService>();
                await CodeUtil.InitAllCode(codeTypeService);
            });
        }
    }
}
