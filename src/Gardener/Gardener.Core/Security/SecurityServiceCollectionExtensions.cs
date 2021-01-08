// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.Authorization;
using Gardener.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 安全服务
    /// </summary>
    public static class SecurityServiceCollectionExtensions
    {
        /// <summary>
        /// 添加安全服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSecurity(this IServiceCollection services)
        {
            //权限管理
            services.AddScoped<IAuthorizationManager, AuthorizationManager>();
            //jwt处理器
            services.AddJwt<JwtHandler>(enableGlobalAuthorize: true);

            services.Configure<MvcOptions>(options =>
            {
                // 添加策略需求
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new AppAuthorizeRequirement("api-auth"));
                options.Filters.Add(new AuthorizeFilter(policy.Build()));
            });
            return services;
        }
    }
}
