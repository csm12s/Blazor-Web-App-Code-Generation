// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using AntDesign.Pro.Layout;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Gardener.Client.Services;
using Gardener.Client.Core;
using Gardener.Clientt.Core;
using Gardener.Application.Interfaces;

namespace Gardener.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            #region api settings
            builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));
            #endregion

            #region httpclient
            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetSection("ApiSettings:BaseAddres")?.Value) });
            #endregion

            #region log
            builder.Logging.AddConfiguration(
                builder.Configuration.GetSection("Logging")
            );
            #endregion

            #region ant design
            builder.Services.AddAntDesign();
            builder.Services.Configure<ProSettings>(builder.Configuration.GetSection("ProSettings"));
            #endregion

            #region 认证、授权
            builder.Services.AddScoped<IAuthenticationStateManager, AuthenticationStateManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            builder.Services.AddAuthorizationCore(option =>
            {
                option.AddPolicy(AuthConstant.DefaultAuthenticatedPolicy, a => a.RequireAuthenticatedUser());
                option.AddPolicy(AuthConstant.ClientUIResourcePolicy, a => a.Requirements.Add(new ClientUIAuthorizationRequirement()));
                option.DefaultPolicy = option.GetPolicy(AuthConstant.DefaultAuthenticatedPolicy);
            });
            builder.Services.AddScoped<IAuthorizationHandler, ClientUIResourceAuthorizationHandler>();
            builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));
            #endregion

            #region 本地化
            builder.Services.AddLocalization(option =>
            {
                option.ResourcesPath = "Resources";
            });
            #endregion

            #region services
            //builder.Services.AddScoped<IClientLogger, ConsoleClientLogger>();
            //builder.Services.AddScoped<JsTool>();
            //builder.Services.AddScoped<HttpClientManager>();
            //builder.Services.AddScoped<IApiCaller, ApiCaller>();
            //builder.Services.AddScoped<IClientErrorNotifier, ClientErrorNotifier>();
            //builder.Services.AddScoped<ISystemConfigService, SystemConfigService>();
            //builder.Services.AddScoped<IAuthorizeService, AuthorizeService>();
            //builder.Services.AddScoped<IRoleService, RoleService>();
            //builder.Services.AddScoped<IUserService, UserService>();
            //builder.Services.AddScoped<IResourceService, ResourceService>();
            //builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            //builder.Services.AddScoped<IAuditOperationService, AuditOperationService>();
            //builder.Services.AddScoped<IAuditEntityService, AuditEntityService>();
            //builder.Services.AddScoped<IFunctionService, FunctionService>();
            //builder.Services.AddScoped<ISwaggerService, SwaggerService>();
            //builder.Services.AddScoped<IResourceFunctionService, ResourceFunctionService>();
            //builder.Services.AddScoped<IDeptService, DeptService>();

            builder.Services.AddServicesWithAttributeOfType<ScopedServiceAttribute>(
                typeof(Dragon).GetTypeInfo().Assembly,
                typeof(ScopedServiceAttribute).GetTypeInfo().Assembly);
            #endregion

            builder.Services.AddTypeAdapterConfigs();

            var host = await builder
                 .Build().UseCulture();
            await host.RunAsync();
        }
    }
}
