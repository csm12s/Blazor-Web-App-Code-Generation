// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Gardener.VerifyCode.CacheStore;
using Gardener.VerifyCode.Core;
using Gardener.VerifyCode.DbStore;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class ImageVerifyCodeExtensions
    {
        /// <summary>
        /// 添加验证码服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="enableAutoVerification">是否启用自动验证</param>
        /// <returns></returns>
        public static IServiceCollection AddImageVerifyCode<TImageVerifyCodeStoreService>(this IServiceCollection services, bool enableAutoVerification = true) where TImageVerifyCodeStoreService  :class, IImageVerifyCodeStoreService
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

            services.AddScoped<IImageVerifyCodeStoreService, TImageVerifyCodeStoreService>();
            return services;
        }

        /// <summary>
        /// 添加验证码服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="enableAutoVerification">是否启用自动验证</param>
        /// <returns></returns>
        public static IServiceCollection AddImageVerifyCode(this IServiceCollection services, bool enableAutoVerification = true)
        {
            string storeMode = App.Configuration["ImageVerifyCodeSettings:StoreMode"]; ;
            if (storeMode.Equals("Cache"))
            { 
                services.AddImageVerifyCode<VerifyCodeCacheStoreService>();
            }else if (storeMode.Equals("DB"))
            {
                services.AddImageVerifyCode<ImageVerifyCodeDbStoreService>();
            }
            return services;
        }
    }
}
