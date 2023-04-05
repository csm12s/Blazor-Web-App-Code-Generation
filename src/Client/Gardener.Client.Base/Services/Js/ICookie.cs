// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    /// <summary>
    /// cookie 操作
    /// </summary>
    public interface ICookie
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public Task<Dictionary<string,string>> Get();
        /// <summary>
        /// 获取指定键的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public Task<string> Get(string key,string? domain=null);
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expires"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public Task Set(string key, string value,int? expires=null,string? path=null);
        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="path"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        public Task Remove(string key, string? path=null,string? domain=null);
    }
}
