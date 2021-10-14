// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DynamicApiController;
using Gardener.Attributes;
using Gardener.Enums;
using Gardener.ImageVerifyCode.Core;
using Gardener.ImageVerifyCode.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gardener.ImageVerifyCode.Services
{
    /// <summary>
    /// 验证码服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class VerifyCodeService : IVerifyCodeService, IDynamicApiController
    {
        private readonly IImageVerifyCodeService imageVerifyCodeService;
        /// <summary>
        /// 验证码服务
        /// </summary>
        /// <param name="imageVerifyCodeService"></param>
        public VerifyCodeService(IImageVerifyCodeService imageVerifyCodeService)
        {
            this.imageVerifyCodeService = imageVerifyCodeService;
        }
        /// <summary>
        /// 获取图片验证码
        /// </summary>
        /// <param name="codeType">类型</param>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<ImageVerifyCodeDto> GetImageVerifyCode(CodeCharacterTypeEnum codeType)
        {
            ImageVerifyCodeInfo imageVerifyCode= await imageVerifyCodeService.CreateFromType(codeType);
            if (imageVerifyCode != null)
            {
                return new ImageVerifyCodeDto { Key=imageVerifyCode.Key,Base64Image = Convert.ToBase64String(imageVerifyCode.Image) };
            }
            return null;
        }
        /// <summary>
        /// 移除图片验证码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<bool> RemoveImageVerifyCode(string key)
        {
           return await imageVerifyCodeService.Remove(key);
        }

        /// <summary>
        /// 验证图片验证码
        /// </summary>
        /// <param name="verifyCodeInput"></param>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<bool> VerifyImageVerifyCode(VerifyCodeInput verifyCodeInput)
        {
            return await imageVerifyCodeService.Verify(verifyCodeInput.VerifyCode, verifyCodeInput.VerifyCodeKey);
        }
    }
}
