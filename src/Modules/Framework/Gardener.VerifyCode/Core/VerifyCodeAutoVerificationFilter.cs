// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.FriendlyException;
using Gardener.Enums;
using Gardener.VerifyCode.Dtos;
using Gardener.VerifyCode.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.VerifyCode.Core
{
    /// <summary>
    /// 验证码自动验证过滤器
    /// </summary>
    public class VerifyCodeAutoVerificationFilter : IAsyncActionFilter
    {
        private readonly Func<VerifyCodeTypeEnum, IVerifyCode> verifyCodeServiceresolve;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resolve"></param>
        public VerifyCodeAutoVerificationFilter(Func<VerifyCodeTypeEnum, IVerifyCode> resolve)
        {
            this.verifyCodeServiceresolve = resolve;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            VerifyCodeAutoVerificationAttribute attribute = context.HttpContext.GetMetadata<VerifyCodeAutoVerificationAttribute>();
            if (attribute == null) { await next(); return; }

            IList<ParameterDescriptor> parameters = context.ActionDescriptor.Parameters;
            if (parameters == null || parameters.Count == 0)
            {
                await next(); return;
            }
            ParameterDescriptor parameter = parameters.FirstOrDefault(x => x.ParameterType.IsSubclassOf(typeof(VerifyCodeCheckInput)));
            if (parameter == null) { await next(); return; }
            var input = context.ActionArguments[parameter.Name] as VerifyCodeCheckInput;
            IVerifyCode _verifyCodeService = verifyCodeServiceresolve(input.VerifyCodeType);

            if (await _verifyCodeService.Verify(input.VerifyCodeKey, input.VerifyCode))
            {
                await next(); return;
            }
            else 
            {
                throw Oops.Bah(ExceptionCode.VERIFY_CODE_VERIFICATION_FAILED);
            }
        }
    }
}
