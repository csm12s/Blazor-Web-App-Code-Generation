// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign.ProLayout;
using Gardener.Base.Resources;
using Gardener.Client.Base;
using Gardener.Client.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System;
using Microsoft.Extensions.Configuration;
using Gardener.Client.AntDesignUi.Base.Extensions;
using System.Threading.Tasks;
using Gardener.Client.AntDesignUi.Base.Constants;

namespace Gardener.Client.AntDesignUi.Entry
{
    public static class ServiceCombine
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void Inject(this IServiceCollection services, string? hostBaseAddress = null)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

            #region module
            services.LoadModulesAndScanServices();
            #endregion

            services.AddApiSetting(hostBaseAddress);

            #region httpclient
            services.AddScoped(sp =>
            {
                IOptions<ApiSettings> settings = sp.GetRequiredService<IOptions<ApiSettings>>();
                return new HttpClient { BaseAddress = new Uri(settings.Value.BaseAddres) };
            });
            #endregion

            #region ant design
            services.AddAntDesign();
            services.Configure<ProSettings>(configuration.GetSection("ProSettings"));
            #endregion

            #region 认证、授权
            services.AddScoped<IAuthenticationStateManager, AuthenticationStateManager>();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            services.AddAuthorizationCore(option =>
            {
                option.AddPolicy(AuthConstant.DefaultAuthenticatedPolicy, a => a.RequireAuthenticatedUser());
                option.AddPolicy(AuthConstant.ClientUIResourcePolicy, a => a.Requirements.Add(new ClientUIAuthorizationRequirement()));
                var defaultPolicy = option.GetPolicy(AuthConstant.DefaultAuthenticatedPolicy);
                if (defaultPolicy != null)
                {
                    option.DefaultPolicy = defaultPolicy;
                }
            });
            services.AddScoped<IAuthorizationHandler, ClientUIResourceAuthorizationHandler>();
            services.Configure<AuthSettings>(configuration.GetSection("AuthSettings"));
            #endregion

            

            #region 本地化
            services.AddAppLocalization<SharedLocalResource>();
            #endregion


            #region  Mapster 配置
            services.AddTypeAdapterConfigs();
            #endregion

            #region  SignalR
            services.AddSignalRClientManager();
            #endregion

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static Task UseInject(this IServiceProvider serviceProvider)
        {
            return serviceProvider.UseAppLocalization(ClientConstant.BlazorCultureKey, ClientConstant.DefaultCulture);
        }
    }
}