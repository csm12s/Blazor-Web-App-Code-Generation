// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Common;
using Gardener.Enums;
using Gardener.VerifyCode.Core;
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
    public class ImageVerifyCodeService : IImageVerifyCodeService
    {
        IVerifyCodeStoreService store;
        ImageVerifyCodeSettings settings;
        /// <summary>
        /// 图片验证码服务
        /// </summary>
        /// <param name="store"></param>
        /// <param name="options"></param>
        public ImageVerifyCodeService(IVerifyCodeStoreService store, IOptions<ImageVerifyCodeSettings> options)
        {
            this.store = store;
            this.settings = options.Value;
        }
        /// <summary>
        /// 创建校验码
        /// </summary>
        /// <param name="codeType">验证码类型</param>
        /// <returns></returns>
        public async Task<ImageVerifyCodeInfo> CreateFromType(CodeCharacterTypeEnum codeType= CodeCharacterTypeEnum.NumberAndCharacter)
        {
            return await Create(new ImageVerifyCodeCreateParam()
            {
                CharacterCount = settings.CodeCharacterCount,
                FontSize = settings.CodeFontSize,
                Type = codeType
            });
        }
        /// <summary>
        /// 创建校验码
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public async Task<ImageVerifyCodeInfo> Create(ImageVerifyCodeCreateParam param=null)
        {
            
            ImageVerifyCodeCreateParam tParam = param ?? new ImageVerifyCodeCreateParam()
            {
                CharacterCount = settings.CodeCharacterCount,
                FontSize = settings.CodeFontSize,
                Type = settings.CodeType
            };

            ImageVerifyCodeInfo result = InnerCreate(tParam);
            await this.store.Add(VerifyCodeTypeEnum.Image,result.Key, result.Code, settings.CodeExpire);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private ImageVerifyCodeInfo InnerCreate(ImageVerifyCodeCreateParam param)
        {
            if (param == null) return null;

            ImageVerifyCodeInfo result = new ImageVerifyCodeInfo();
            result.Key = Guid.NewGuid().ToString("N");

            switch (param.Type)
            {
                case CodeCharacterTypeEnum.Character:
                    result.Code = RandomCodeCreator.CreatRandomChar(param.CharacterCount);
                    break;
                case CodeCharacterTypeEnum.Number:
                    result.Code = RandomCodeCreator.CreatRandomNum(param.CharacterCount);
                    break;
                case CodeCharacterTypeEnum.NumberAndCharacter:
                    result.Code = RandomCodeCreator.CreatRandomNumAndChar(param.CharacterCount);
                    break;
            }
            result.Image = RandomCodeImageCreator.Create(result.Code, param.FontSize);

            return result;
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
