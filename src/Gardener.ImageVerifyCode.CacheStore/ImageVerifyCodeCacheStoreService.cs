// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Cache;
using Gardener.ImageVerifyCode.Core;
using System;
using System.Threading.Tasks;

namespace Gardener.ImageVerifyCode.CacheStore
{
    /// <summary>
    /// 图片验证码数据库存储服务
    /// </summary>
    public class ImageVerifyCodeCacheStoreService : IImageVerifyCodeStoreService
    {
        private readonly ICache _cache;
        private readonly string keyPre = "ImageVerifyCode:";

        public ImageVerifyCodeCacheStoreService(ICache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="code"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        public async Task Add(string key, string code, TimeSpan expire)
        {
            await _cache.SetAsync(keyPre+key, code, expire);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> GetCode(string key)
        {
            return await _cache.GetStringAsync(keyPre+key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task Remove(string key)
        {
            await _cache.RemoveAsync(keyPre+key);
        }
    }
}
