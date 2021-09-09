// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Audit;
using Gardener.Audit.DbContextLocator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 审计
    /// </summary>
    public static class AuditServiceCollectionExtensions
    {
        /// <summary>
        /// 添加审计服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAudit(this IServiceCollection services)
        {
            return services;
        }

        /// <summary>
        /// 添加审计服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAudit<TDbContextLocator>(this IServiceCollection services) where TDbContextLocator : class, IDbContextLocator
        {
            services.Configure<MvcOptions>(options =>
            {
                //审计过滤器
                options.Filters.Add<AuditActionFilter>();
            });
            //数据管理
            services.AddScoped<IAuditDataManager, AuditDataManager<TDbContextLocator>>();
            return services;
        }
    }
}
