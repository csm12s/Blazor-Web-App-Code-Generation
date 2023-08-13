// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;

namespace Gardener.LocalizationLocalizer
{
    /// <summary>
    /// 注入包装好的本地化内容处理器
    /// </summary>
    public static class LocalizationLocalizerExtensions
    {
        /// <summary>
        /// 注入包装好的本地化内容处理器
        /// </summary>
        /// <typeparam name="TDefaultResource">默认的共享资源</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <remarks>
        /// 代替系统的 <see cref="Microsoft.Extensions.Localization.IStringLocalizer"/>
        /// </remarks>
        public static IServiceCollection AddLocalizationLocalizer<TDefaultResource>(this IServiceCollection services)
        {
            //注入公共默认的
            services.TryAddTransient<ILocalizationLocalizer, LocalizationLocalizerImpl<TDefaultResource>>();
            //注入其它资源类的
            services.TryAddTransient(typeof(ILocalizationLocalizer<>), typeof(LocalizationLocalizerMultipleImpl<>));

            return services;
        }

        /// <summary>
        /// 设置静态本地化器
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static IServiceProvider InitLocalizationLocalizerUtil(this IServiceProvider serviceProvider)
        {

            Lo.Init(type =>
            {
                //IServiceScopeFactory scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
                //using var scope = scopeFactory.CreateScope();
                object localizer = serviceProvider.GetRequiredService(type);

                return (ILocalizationLocalizer)localizer;
            });
            return serviceProvider;
        }
    }
}
