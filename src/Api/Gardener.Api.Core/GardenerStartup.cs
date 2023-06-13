// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Gardener.Authorization.Core;
using Microsoft.AspNetCore.Http;
using Gardener.NotificationSystem;
using Gardener.Common.JsonConverters;
using System;
using Microsoft.AspNetCore.HttpOverrides;
using AspNetCoreRateLimit;
using Gardener.Sugar;
using Gardener.DistributedLock;

namespace Gardener.Admin
{
    /// <summary>
    /// 启动类
    /// </summary>
    [AppStartup(600)]
    public sealed class GardenerAdminStartup : AppStartup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //开启审计
            services.AddAudit();
            //开启身份认证
            services.AddAuthen();
            //开启授权
            services.AddAuthor<IdentityPermissionService, ApiEndpointQueryService>();
            //开启验证码
            services.AddVerifyCode(true);
            //开启本地文件存储
            services.AddFileLocalStore();
            //事件总线
            services.AddEventBusServices(builder =>
            {
                // 注册事件订阅者
            });
            //注册跨域
            services.AddCorsAccessor();
            //远程请求
            services.AddRemoteRequest();
            //缓存服务
            services.AddCache();
            //注册swagger
            services.AddSpecificationDocuments(x =>
            {
                x.EnableAnnotations();
            });
            //注册控制器和视图
            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetJsonConverter());
            })
            //配置多语言
            .AddAppLocalization()
            //注册动态api
            .AddDynamicApiControllers()
            //注册数据验证
            .AddDataValidation()
            //注册友好异常
            .AddFriendlyException()
            //注册规范返回格式
            .AddUnifyResult<MyRESTfulResultProvider>()
            ;
            //视图引擎
            services.AddViewEngine();
            //添加系统通知服务
            services.AddSystemNotify();
            //文件日志
            services.AddFileLogging(options => {
                options.FileNameRule = fileName =>
                {
                    return string.Format(fileName, DateTime.UtcNow);
                };

            });
            //默认读取 Logging:Monitor 下配置
            //services.AddMonitorLogging();

            // 配置Nginx转发获取客户端真实IP
            // 1：如果负载均衡不是在本机通过 Loopback 地址转发请求的，
            // 一定要加上options.KnownNetworks.Clear()和options.KnownProxies.Clear()
            // 2：如果设置环境变量 ASPNETCORE_FORWARDEDHEADERS_ENABLED 为 True，
            // 则不需要下面的配置代码
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.All;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            // 限流服务
            // 只提供最基础限流服务，更多配置请查看
            // https://github.com/stefanprodan/AspNetCoreRateLimit/wiki/IpRateLimitMiddleware#defining-rate-limit-rules
            services.AddInMemoryRateLimiting();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            //SqlSugar dbSettings.json:
            services.AddConfigurableOptions<ConnectionStringsOptions>();
            services.AddConfigurableOptions<DefaultDbSettingsOptions>();
            // SqlSugar
            services.SqlSugarScopeConfigure();
            //分部式锁
            services.AddDistributedLock();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpLogging();

            app.UseHttpsRedirection();
            //启用EnableBuffering 解决Request body获取不到
            app.Use(next => context =>
            {
                context.Request.EnableBuffering();
                return next(context);
            });

            // 配置多语言，必须在 路由注册之前
            app.UseAppLocalization();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCorsAccessor();

            // 限流组件(需注册在跨域之后)
            app.UseIpRateLimiting();
            app.UseClientRateLimiting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseInject();

            app.UseEndpoints(endpoints =>
            {
                // 注册集线器
                endpoints.MapHubs();
                //endpoints.MapRazorPages();
                //endpoints.MapFallbackToFile("index.html");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
