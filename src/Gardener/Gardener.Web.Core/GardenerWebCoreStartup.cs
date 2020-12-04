// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------
using Furion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gardener.Web.Core
{
    [AppStartup(700)]
    public sealed class GardenerWebCoreStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //注册App授权
            services.AddJwt<JwtHandler>(enableGlobalAuthorize:true);
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new ApiAuthorizeAttribute("api-auth"));
            });
            //注册跨域
            services.AddCorsAccessor();
            //注册控制器和视图
            services.AddControllersWithViews().AddJsonOptions(options =>
            {

                options.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
                options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetJsonConverter());
            })
            //注册Furion
            .AddInject()
            //注册规范返回格式
            .AddUnifyResult();
            //
            //.AddGlobalApiAuthorizeAttribute();
        }

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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
