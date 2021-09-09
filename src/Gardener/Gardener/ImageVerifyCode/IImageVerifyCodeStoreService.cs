// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace Gardener.ImageVerifyCode
{
    /// <summary>
    /// 图片验证码存储
    /// </summary>
    public interface IImageVerifyCodeStoreService
    {
        /// <summary>
        /// 保存校验码
        /// </summary>
        /// <param name="key"></param>
        /// <param name="code"></param>
        /// <param name="expire"></param>
        Task Add(string key, string code, TimeSpan expire);

        /// <summary>
        /// 获取校验码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetCode(string key);

        /// <summary>
        /// 移除校验码
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task Remove(string key);
    }
}
