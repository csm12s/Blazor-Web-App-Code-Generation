// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Core;
using Gardener.Authorization.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 安全服务
    /// </summary>
    public static class AuthorizationExtensions
    {
        /// <summary>
        /// 添加安全服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuthor<TIdentityPermissionService, TApiEndpointStoreService>(this IServiceCollection services) where TIdentityPermissionService:class, IIdentityPermissionService where TApiEndpointStoreService:class, IApiEndpointQueryService
        {

            services.TryAddSingleton<IAuthorizationPolicyProvider, AppAuthorizationPolicyProvider>();
            services.TryAddSingleton<IAuthorizationHandler, JwtHandler>();
            services.TryAddSingleton<IApiEndpointQueryService, TApiEndpointStoreService>();
            services.TryAddSingleton<IIdentityPermissionService, TIdentityPermissionService>();

            services.TryAddScoped<IIdentityService, IdentityService>();
            services.TryAddScoped<Gardener.Authorization.Core.IAuthorizationService, AuthorizationService>();
            services.Configure<MvcOptions>(options =>
            {
                // 添加策略需求
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new AppAuthorizeRequirement("api-auth"));
                //api授权
                options.Filters.Add(new AuthorizeFilter(policy.Build()));
            });

            return services;
        }
    }
}
