// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.ImageVerifyCode;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// 添加验证码服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="enableAutoVerification">是否启用自动验证</param>
        /// <returns></returns>
        public static IServiceCollection AddImageVerifyCode(this IServiceCollection services,bool enableAutoVerification=true)
        {
            if (enableAutoVerification) 
            {
                services.Configure<MvcOptions>(options =>
                {
                    //自动验证
                    options.Filters.Add<VerifyCodeAutoVerificationFilter>();
                });
            }
            services.AddScoped<IImageVerifyCodeService, ImageVerifyCodeService>();
            services.AddConfigurableOptions<ImageVerifyCodeSettings>();
            return services;
        }
        /// <summary>
        /// 添加验证码服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="enableAutoVerification">是否启用自动验证</param>
        /// <returns></returns>
        public static IServiceCollection AddImageVerifyCode<TImageVerifyCodeStoreService>(this IServiceCollection services, bool enableAutoVerification = true) where TImageVerifyCodeStoreService  :class, IImageVerifyCodeStoreService
        {
            services.AddImageVerifyCode(enableAutoVerification);
            services.AddScoped<IImageVerifyCodeStoreService, TImageVerifyCodeStoreService>();
            return services;
        }
    }
}
