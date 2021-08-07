// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Core.VerifyCode;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class VerifyCodeAutoVerificationExtensions
    {
        /// <summary>
        /// 添加审计服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddVerifyCodeAutoVerification(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(options =>
            {
                //审计
                options.Filters.Add<VerifyCodeAutoVerificationFilter>();
            });
            return services;
        }
    }
}
