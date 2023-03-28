// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DynamicApiController;
using Gardener.Attributes;
using Gardener.VerifyCode.Core;
using Gardener.VerifyCode.Dtos;
using Gardener.VerifyCode.Enums;
using Gardener.VerifyCode.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gardener.ImageVerifyCode.Services
{
    /// <summary>
    /// 图片验证码服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class ImageVerifyCodeService : IImageVerifyCodeService, IDynamicApiController
    {
        private readonly IVerifyCode verifyCodeService;
        /// <summary>
        /// 验证码服务
        /// </summary>
        /// <param name="verifyCodeServiceProvider"></param>
        public ImageVerifyCodeService(Func<VerifyCodeTypeEnum, IVerifyCode> verifyCodeServiceProvider)
        {
            this.verifyCodeService = verifyCodeServiceProvider(VerifyCodeTypeEnum.Image);
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="input">类型</param>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<ImageVerifyCodeOutput> Create(ImageVerifyCodeInput input)
        {
            ImageVerifyCodeOutput imageVerifyCode =(ImageVerifyCodeOutput) await verifyCodeService.Create(input);
            return imageVerifyCode;
        }
        /// <summary>
        /// 移除验证码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<bool> Remove(string key)
        {
            return await verifyCodeService.Remove(key);
        }

        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<bool> Verify(ImageVerifyCodeCheckInput input)
        {
            return await verifyCodeService.Verify(input.VerifyCode, input.VerifyCodeKey);
        }
    }
}
