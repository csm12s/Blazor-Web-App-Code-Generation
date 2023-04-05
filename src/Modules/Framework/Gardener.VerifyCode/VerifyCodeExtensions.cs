// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Gardener.ImageVerifyCode.Core;
using Gardener.ImageVerifyCode.Services;
using Gardener.VerifyCode.CacheStore;
using Gardener.VerifyCode.Core;
using Gardener.VerifyCode.Core.Settings;
using Gardener.VerifyCode.DbStore;
using Gardener.VerifyCode.Enums;
using Gardener.VerifyCode.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 验证码
    /// </summary>
    public static class VerifyCodeExtensions
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
            services.AddScoped<ImageVerifyCode>();
            services.AddScoped<IImageVerifyCodeService, ImageVerifyCodeService>();
            //图片验证码配置
            services.AddConfigurableOptions<ImageVerifyCodeOptions>();
             //邮件验证码
            services.AddScoped<EmailVerifyCode>();
            services.AddScoped<IEmailVerifyCodeService, EmailVerifyCodeService>();
            //邮件验证码配置
            services.AddConfigurableOptions<EmailVerifyCodeOptions>();
            //验证码存储实现
            services.AddScoped<IVerifyCodeStoreService, TVerifyCodeStoreService>();
            //验证码服务提供器
            services.AddScoped(serviceProvider => {
                Func<VerifyCodeTypeEnum, IVerifyCode> accesor = key =>
                {
                    if (VerifyCodeTypeEnum.Image.Equals(key))
                        return serviceProvider.GetRequiredService<ImageVerifyCode>();
                    else if (VerifyCodeTypeEnum.Email.Equals(key))
                        return serviceProvider.GetRequiredService<EmailVerifyCode>();
                    else
                        throw new ArgumentException($"不支持的验证码类型: {key}");
                };
                return accesor;

            });
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
            string? storeMode = App.Configuration["VerifyCodeStoreSetting"];
            storeMode = storeMode ?? "Cache";
            if ("Cache".Equals(storeMode))
            { 
                services.AddVerifyCode<VerifyCodeCacheStoreService>();
            }else if ("DB".Equals(storeMode))
            {
                services.AddVerifyCode<VerifyCodeDbStoreService>();
            }
            return services;
        }
    }
}
