using Furion.DependencyInjection;
using Gardener.Core.Security.Authorization;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Gardener.Core.Security
{
    /// <summary>
    /// 授权策略执行程序
    /// </summary>
    [SkipScan]
    public abstract class AppAuthorizeHandler : AuthorizationHandler<AppAuthorizeRequirement>
    {
        ///// <summary>
        ///// 授权验证核心方法
        ///// </summary>
        ///// <param name="context"></param>
        ///// <returns></returns>
        //public Task HandleAsync(AuthorizationHandlerContext context)
        //{
        //    // 获取所有未成功验证的需求
        //    var pendingRequirements = context.PendingRequirements;

        //    DefaultHttpContext httpContext;

        //    // 获取 httpContext 对象
        //    if (context.Resource is AuthorizationFilterContext filterContext) httpContext = (DefaultHttpContext)filterContext.HttpContext;
        //    else if (context.Resource is DefaultHttpContext defaultHttpContext) httpContext = defaultHttpContext;
        //    else httpContext = null;

        //    // 调用子类管道
        //    var pipeline = Pipeline(context, httpContext);
        //    if (!pipeline) context.Fail();
        //    else
        //    {
        //        // 通过授权验证
        //        foreach (var requirement in pendingRequirements)
        //        {
        //            context.Succeed(requirement);
        //        }
        //    }

        //    return Task.CompletedTask;
        //}
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AppAuthorizeRequirement requirement)
        {
            if (Pipeline(context, requirement))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// 验证管道
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public virtual bool Pipeline(AuthorizationHandlerContext context, AppAuthorizeRequirement requirement)
        {
            return true;
        }
    }
}