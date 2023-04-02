// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DataValidation;
using Furion.DependencyInjection;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using Gardener.Enums;
using Gardener.Common;
using Microsoft.Extensions.Logging;
using Furion.FriendlyException;

namespace Gardener.Admin
{
    /// <summary>
    /// RESTful 风格返回值
    /// </summary>
    [SuppressSniffer, UnifyModel(typeof(RESTfulResult<>))]
    public class MyRESTfulResultProvider : IUnifyResultProvider
    {

        /// <summary>
        /// 日志记录器
        /// </summary>
        private readonly ILogger<MyRESTfulResultProvider> _logger;
        /// <summary>
        /// RESTful 风格返回值
        /// </summary>
        /// <param name="logger"></param>
        public MyRESTfulResultProvider(ILogger<MyRESTfulResultProvider> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 异常返回值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public IActionResult OnException(ExceptionContext context, ExceptionMetadata metadata)
        {
            _logger.LogError(context.Exception, metadata.Errors?.ToString());
            return new JsonResult(RESTfulResult(metadata.StatusCode, errors: metadata.Errors, errorCode: metadata.ErrorCode));
        }

        /// <summary>
        /// 成功返回值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public IActionResult OnSucceeded(ActionExecutedContext context, object data)
        {
            return new JsonResult(RESTfulResult(StatusCodes.Status200OK, true, data));
        }

        /// <summary>
        /// 验证失败返回值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public IActionResult OnValidateFailed(ActionExecutingContext context, ValidationMetadata metadata)
        {
            return new JsonResult(RESTfulResult(StatusCodes.Status400BadRequest, errors: metadata.ValidationResult));
        }

        /// <summary>
        /// 特定状态码返回值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        /// <param name="unifyResultSettings"></param>
        /// <returns></returns>
        public async Task OnResponseStatusCodes(HttpContext context, int statusCode, UnifyResultSettingsOptions unifyResultSettings)
        {
            // 设置响应状态码
            UnifyContext.SetResponseStatusCodes(context, statusCode, unifyResultSettings);

            switch (statusCode)
            {
                // 处理 401 状态码
                case StatusCodes.Status401Unauthorized:
                    await context.Response.WriteAsJsonAsync(RESTfulResult(statusCode, errors: EnumHelper.GetEnumDescription(ExceptionCode.UNAUTHORIZED), errorCode: ExceptionCode.UNAUTHORIZED)
                        , App.GetOptions<JsonOptions>()?.JsonSerializerOptions);
                    break;
                // 处理 403 状态码
                case StatusCodes.Status403Forbidden:
                    await context.Response.WriteAsJsonAsync(RESTfulResult(statusCode, errors: EnumHelper.GetEnumDescription(ExceptionCode.FORBIDDEN), errorCode: ExceptionCode.FORBIDDEN)
                        , App.GetOptions<JsonOptions>()?.JsonSerializerOptions);
                    break;
                default: break;
            }
        }

        /// <summary>
        /// 返回 RESTful 风格结果集
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="succeeded"></param>
        /// <param name="data"></param>
        /// <param name="errors"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        static private object RESTfulResult(int statusCode, bool succeeded = default, object? data = default, object? errors = default, object? errorCode = default)
        {

            if (succeeded)
            {
                return new
                {
                    Data = data,
                    StatusCode = statusCode,
                    Succeeded = succeeded,
                    ErrorCode = errorCode?.ToString(),
                    Errors = errors,
                    Extras = UnifyContext.Take(),
                    Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()
                };
            }

            return new
            {
                StatusCode = statusCode,
                Succeeded = succeeded,
                ErrorCode = errorCode?.ToString(),
                Errors = errors,
                Extras = UnifyContext.Take(),
                Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()
            };
        }
    }
}
