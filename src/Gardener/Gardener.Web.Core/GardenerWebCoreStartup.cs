// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------
using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Gardener.Application.Extensions;

namespace Gardener.Web.Core
{
    [AppStartup(700)]
    public sealed class GardenerWebCoreStartup : AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //注册JWT授权
            services.AddJwt<JwtHandler>();
            //注册跨域
            services.AddCorsAccessor();
            //注册控制器和视图
            services.AddControllersWithViews()
                //注册Furion
                .AddInject()
                //注册规范返回格式
                .AddUnifyResult()
                //
                .AddDynamicApiStorage();
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
