// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Gardener.SysTimer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Gardener.SysTimer.Impl
{
    /// <summary>
    /// 任务调度启动项
    /// </summary>
    [AppStartup(500)]
    public class SysTimerStartup : AppStartup
    {
        /// <summary>
        /// 启动调度任务
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 开启自启动定时任务
//TODO: 这里在项目最初打开的时候有时会报错
            App.GetService<ISysTimerService>().StartTimerJob();
        }
    }
}
