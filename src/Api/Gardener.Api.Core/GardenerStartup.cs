// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Gardener.Admin.JsonConverters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Gardener.Authorization.Core;
using Gardener.EntityFramwork.DbContexts;
using Gardener.Api.Core.Authorization.Subscribes;
using Gardener.UserCenter.Impl.Core.Subscribes;
using Gardener.VerifyCode.Core;

namespace Gardener.Admin
{
    /// <summary>
    /// 启动类
    /// </summary>
    [AppStartup(600)]
    public sealed class GardenerAdminStartup : AppStartup
    {
        private static readonly string migrationAssemblyName= "Gardener.Api.Core";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //业务库
            services.AddDatabaseAccessor(options =>
            {
                //注入数据库上下文
                options.AddDbPool<GardenerDbContext>(DbProvider.Sqlite);
                options.AddDbPool<GardenerAuditDbContext,GardenerAuditDbContextLocator>(DbProvider.Sqlite);
            }, migrationAssemblyName);
            //开启审计
            services.AddAudit();
            //开启身份认证
            services.AddAuthen();
            //开启授权
            services.AddAuthor<IdentityPermissionService,ApiEndpointQueryService>();
            //开启验证码
            services.AddVerifyCode(true);
            //开启本地文件存储
            services.AddFileLocalStore();
            //事件总线
            services.AddEventBusServices(builder =>
            {
                // 注册事件订阅者
                builder.AddSubscriber<FunctionChangeRefreshCacheSubscriber>();
                builder.AddSubscriber<FunctionDeleteClearRelationSubscriber>();
            });
            //注册跨域
            services.AddCorsAccessor();
            //远程请求
            services.AddRemoteRequest();
            //缓存服务
            services.AddCache();
            //注册swagger
            services.AddSpecificationDocuments(x => {
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

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCorsAccessor();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseInject();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                //endpoints.MapFallbackToFile("index.html");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
