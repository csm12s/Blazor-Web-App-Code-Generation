// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Gardener.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    [SkipScan]
    public static class AuthorizeControllerExtension
    {
        /// <summary>
        /// 添加动态接口控制器服务 
        /// </summary>
        /// <param name="mvcBuilder">Mvc构建器</param>
        /// <returns>Mvc构建器</returns>
        public static IMvcBuilder AddGlobalApiAuthorizeAttribute(this IMvcBuilder mvcBuilder)
        {
            // 配置 Mvc 选项
            mvcBuilder.AddMvcOptions(options =>
            {
                options.Conventions.Add(new AuthorizeControllerApplicationModelConvention());
            });
            return mvcBuilder;
        }
    }
}
