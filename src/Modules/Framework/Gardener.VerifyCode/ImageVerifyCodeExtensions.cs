// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Gardener.ImageVerifyCode.Core;
using Gardener.VerifyCode.CacheStore;
using Gardener.VerifyCode.Core;
using Gardener.VerifyCode.DbStore;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 验证码
    /// </summary>
    public static class ImageVerifyCodeExtensions
    {
        /// <summary>
        /// 添加验证码服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="enableAutoVerification">是否启用自动验证</param>
        /// <returns></returns>
        public static IServiceCollection AddVerifyCode<TVerifyCodeStoreService>(this IServiceCollection services, bool enableAutoVerification = true) where TVerifyCodeStoreService : class, IVerifyCodeStoreService
        {
            if (enableAutoVerification)
            {
                services.Configure<MvcOptions>(options =>
                {
                    //自动验证
                    options.Filters.Add<VerifyCodeAutoVerificationFilter>();
                });
            }
            //图片验证码
            services.AddScoped<IImageVerifyCodeService, ImageVerifyCodeService>();
            //图片验证码配置
            services.AddConfigurableOptions<ImageVerifyCodeSettings>();
            //验证码存储实现
            services.AddScoped<IVerifyCodeStoreService, TVerifyCodeStoreService>();
            return services;
        }

        /// <summary>
        /// 添加验证码服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="enableAutoVerification">是否启用自动验证</param>
        /// <returns></returns>
        public static IServiceCollection AddVerifyCode(this IServiceCollection services, bool enableAutoVerification = true)
        {
            string storeMode = App.Configuration["VerifyCodeStoreSettings:StoreMode"]; ;
            if (storeMode.Equals("Cache"))
            { 
                services.AddVerifyCode<VerifyCodeCacheStoreService>();
            }else if (storeMode.Equals("DB"))
            {
                services.AddVerifyCode<VerifyCodeDbStoreService>();
            }
            return services;
        }
    }
}
