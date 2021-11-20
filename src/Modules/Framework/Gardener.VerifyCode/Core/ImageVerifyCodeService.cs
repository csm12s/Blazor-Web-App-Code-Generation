// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Common;
using Gardener.VerifyCode.Core;
using Gardener.VerifyCode.Core.Settings;
using Gardener.VerifyCode.Dtos;
using Gardener.VerifyCode.Enums;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Gardener.ImageVerifyCode.Core
{
    /// <summary>
    /// 图片验证码服务
    /// </summary>
    public class ImageVerifyCodeService : IVerifyCodeService
    {
        IVerifyCodeStoreService store;
        ImageVerifyCodeOptions settings;
        /// <summary>
        /// 图片验证码服务
        /// </summary>
        /// <param name="store"></param>
        /// <param name="options"></param>
        public ImageVerifyCodeService(IVerifyCodeStoreService store, IOptions<ImageVerifyCodeOptions> options)
        {
            this.store = store;
            this.settings = options.Value;
        }
        /// <summary>
        /// 创建校验码
        /// </summary>
        /// <param name="input">参数</param>
        /// <returns></returns>
        public async Task<VerifyCodeOutput> Create(VerifyCodeInput input)
        {
            ImageVerifyCodeInput imageInput = input as ImageVerifyCodeInput;
            
            var (code,image) = InnerCreate(imageInput);
            ImageVerifyCodeOutput verifyCodeInfo = new ImageVerifyCodeOutput() {
                Key = Guid.NewGuid().ToString(),
                Base64Image = Convert.ToBase64String(image)
            };
            await this.store.Add(VerifyCodeTypeEnum.Image, verifyCodeInfo.Key, code, settings.CodeExpire);
            return verifyCodeInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private (string,byte[]) InnerCreate(ImageVerifyCodeInput param)
        {
            param.CreateCodeParam = param.CreateCodeParam ?? new CharacterCodeCreateParam();
            if (!param.CreateCodeParam.CharacterCount.HasValue)
            {
                param.CreateCodeParam.CharacterCount = settings.CodeCharacterCount;
            }
            if (!param.CreateCodeParam.Type.HasValue)
            {
                param.CreateCodeParam.Type = settings.CodeType;
            }
            if (!param.FontSize.HasValue)
            {
                param.FontSize = settings.CodeFontSize;
            }

            string code = null;
            byte[] image = null;
            if (param == null) return (code,image);
            CharacterCodeCreateParam CharacterVerifyCodeParam = param.CreateCodeParam;
            code= RandomCodeCreator.Create(CharacterVerifyCodeParam.Type.Value, CharacterVerifyCodeParam.CharacterCount.Value);
            image = RandomCodeImageCreator.Create(code, param.FontSize.Value);
            return (code, image);
        }

        /// <summary>
        /// 校验校验码
        /// </summary>
        /// <param name="key"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<bool> Verify(string key, string code)
        {
            //code为空，直接返回错误
            if (string.IsNullOrEmpty(code)) return false;
            string dbCode = await this.store.GetCode(VerifyCodeTypeEnum.Image, key);
            bool success = string.Compare(dbCode, code, settings.IgnoreCase) == 0;
            if (success && settings.UseOnce)
            {
                await this.store.Remove(VerifyCodeTypeEnum.Image, key);
            }

            return success;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> Remove(string key)
        {
            await this.store.Remove(VerifyCodeTypeEnum.Image, key);

            return true;
        }
    }
}
