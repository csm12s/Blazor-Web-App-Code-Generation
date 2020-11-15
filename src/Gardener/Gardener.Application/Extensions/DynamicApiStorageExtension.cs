// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Application.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    [SkipScan]
    public static class DynamicApiStorageExtension
    {
        /// <summary>
        /// 添加动态接口控制器服务 
        /// </summary>
        /// <param name="mvcBuilder">Mvc构建器</param>
        /// <returns>Mvc构建器</returns>
        public static IMvcBuilder AddDynamicApiStorage(this IMvcBuilder mvcBuilder)
        {
            // 配置 Mvc 选项
            mvcBuilder.AddMvcOptions(options =>
            {
                options.Conventions.Add(new DynamicApiResourceCollectApplicationModelConvention());
            });
            return mvcBuilder;
        }
    }
}
