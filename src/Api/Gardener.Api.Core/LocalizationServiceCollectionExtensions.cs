// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DependencyInjection;
using Furion.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using System;
using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Gardener.Api.Core
{
    internal static class PenetratesPro
    {
        /// <summary>
        /// 设置请求多语言对象
        /// </summary>
        /// <param name="requestLocalization"></param>
        /// <param name="localizationSettings"></param>
        internal static void SetRequestLocalization(RequestLocalizationOptions requestLocalization, LocalizationSettingsOptions localizationSettings)
        {
            // 如果设置了默认语言，则取默认语言，否则取第一个
            requestLocalization.SetDefaultCulture(
                        string.IsNullOrWhiteSpace(localizationSettings.DefaultCulture)
                            ? (localizationSettings.SupportedCultures != null && localizationSettings.SupportedCultures.Length > 0 ? localizationSettings.SupportedCultures[0] : localizationSettings.DefaultCulture)
                            : localizationSettings.DefaultCulture)
                   .AddSupportedCultures(localizationSettings.SupportedCultures)
                   .AddSupportedUICultures(localizationSettings.SupportedCultures);

            // 自动根据客户端浏览器的语言实现多语言机制
            requestLocalization.ApplyCurrentCultureToResponseHeaders = true;

            // 修复 DateTime 问题 https://gitee.com/dotnetchina/Furion/issues/I6RUOU
            if (!string.IsNullOrWhiteSpace(localizationSettings.DateTimeFormatCulture))
            {
                var standardCulture = new CultureInfo(localizationSettings.DateTimeFormatCulture);

                // 修复默认区域语言
                FixedCultureDateTimeFormat(requestLocalization.DefaultRequestCulture, standardCulture);

                // 修复所有支持的区域语言
                foreach (var culture in requestLocalization.SupportedCultures)
                {
                    FixedCultureDateTimeFormat(culture, standardCulture);
                }

                // 修复线程区域语言
                foreach (var culture in requestLocalization.SupportedUICultures)
                {
                    FixedCultureDateTimeFormat(culture, standardCulture);
                }
            }
        }

        /// <summary>
        /// 修复多语言引起的 DateTime.Now 问题
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="targetCulture"></param>
        internal static void FixedCultureDateTimeFormat(CultureInfo culture, CultureInfo targetCulture)
        {
            culture.DateTimeFormat = targetCulture.DateTimeFormat;
        }

        /// <summary>
        /// 修复多语言引起的 DateTime.Now 问题
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="targetCulture"></param>
        internal static void FixedCultureDateTimeFormat(RequestCulture culture, CultureInfo targetCulture)
        {
            culture.Culture.DateTimeFormat = targetCulture.DateTimeFormat;
            culture.UICulture.DateTimeFormat = targetCulture.DateTimeFormat;
        }

        /// <summary>
        /// 修复多语言引起的 DateTime.Now 问题
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="targetCulture"></param>
        internal static void FixedCultureDateTimeFormat(CultureInfo culture, string targetCulture)
        {
            if (!string.IsNullOrWhiteSpace(targetCulture))
            {
                FixedCultureDateTimeFormat(culture, new CultureInfo(targetCulture));
            }
        }

        /// <summary>
        /// 修复多语言引起的 DateTime.Now 问题
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="targetCulture"></param>
        internal static void FixedCultureDateTimeFormat(RequestCulture culture, string targetCulture)
        {
            if (!string.IsNullOrWhiteSpace(targetCulture))
            {
                FixedCultureDateTimeFormat(culture, new CultureInfo(targetCulture));
            }
        }
    }
    /// <summary>
    /// 多语言服务拓展类
    /// </summary>
    [SuppressSniffer]
    public static class LocalizationServiceCollectionExtensions
    {
        /// <summary>
        /// 配置多语言服务
        /// </summary>
        /// <param name="mvcBuilder"></param>
        /// <param name="customizeConfigure">如果传入该参数，则使用自定义多语言机制</param>
        /// <returns></returns>
        public static IMvcBuilder AddAppLocalizationPro(this IMvcBuilder mvcBuilder, Action<LocalizationSettingsOptions> customizeConfigure = default)
        {
            // 添加多语言配置选项
            mvcBuilder.Services.AddAppLocalizationPro(customizeConfigure);

            // 获取多语言配置选项
            var localizationSettings = App.GetConfig<LocalizationSettingsOptions>("LocalizationSettings", true);

            // 配置视图多语言和验证多语言
            mvcBuilder.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                     .AddDataAnnotationsLocalization(options =>
                     {
                         options.DataAnnotationLocalizerProvider = (type, factory) =>
                             factory.Create(localizationSettings.LanguageFilePrefix, localizationSettings.AssemblyName);
                     });

            return mvcBuilder;
        }

        /// <summary>
        /// 配置多语言服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="customizeConfigure">如果传入该参数，则使用自定义多语言机制</param>
        /// <returns></returns>
        public static IServiceCollection AddAppLocalizationPro(this IServiceCollection services, Action<LocalizationSettingsOptions> customizeConfigure = default)
        {
            // 添加多语言配置选项
            services.AddConfigurableOptions<LocalizationSettingsOptions>();

            // 获取多语言配置选项
            var localizationSettings = App.GetConfig<LocalizationSettingsOptions>("LocalizationSettings", true);
            services.TryAddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactoryPro>();
            // 注册默认多语言服务
            if (customizeConfigure == null)
            {
                services.AddLocalization(options =>
                {
                    //if (!string.IsNullOrWhiteSpace(localizationSettings.ResourcesPath))
                    //    options.ResourcesPath = localizationSettings.ResourcesPath;
                });
            }
            // 使用自定义
            else customizeConfigure.Invoke(localizationSettings);

            // 注册请求多语言配置选项
            services.Configure<RequestLocalizationOptions>(options =>
            {
                PenetratesPro.SetRequestLocalization(options, localizationSettings);
            });

            // 处理多语言在 Razor 视图中文乱码问题
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            return services;
        }
    }
}