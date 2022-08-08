﻿// -----------------------------------------------------------------------------
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
using Furion.Logging;

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
                x.SwaggerGenConfigure = config => config.EnableAnnotations();
            });
            //注册控制器和视图
            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetJsonConverter());
            })
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
            services.AddFileLogging();
            //默认读取 Logging:Monitor 下配置
            services.AddMonitorLogging();
            //全局启用 LoggingMonitor
            services.AddMvcFilter<LoggingMonitorAttribute>();

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
            }); ;

            app.UseStaticFiles();


            app.UseRouting();

            app.UseCorsAccessor();

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
