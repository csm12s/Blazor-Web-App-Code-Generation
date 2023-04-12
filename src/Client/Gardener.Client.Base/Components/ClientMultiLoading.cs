// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Collections.Generic;

namespace Gardener.Client.Base.Components
{
    /// <summary>
    /// 客户端加载中标记-多实例-应用于列表
    /// </summary>
    public class ClientMultiLoading
    {
        /// <summary>
        /// 默认是否是加载中
        /// </summary>
        private bool defaultIsLoading = false;

        /// <summary>
        /// 每个实例对应的加载中标记
        /// </summary>
        private Dictionary<object, bool> loadings = new Dictionary<object, bool>();

        /// <summary>
        /// 客户端加载中标记-多实例-应用于列表
        /// </summary>
        /// <param name="defaultIsLoading">默认是否是加载中</param>
        public ClientMultiLoading(bool defaultIsLoading)
        {
            this.defaultIsLoading = defaultIsLoading;
        }
        /// <summary>
        /// 获取加载中标记
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool this[object key] => GetValue(key);
        /// <summary>
        /// 获取加载中标记
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool GetValue(object key)
        {
            if (!loadings.ContainsKey(key))
            {
                loadings.Add(key, defaultIsLoading);
            }
            return loadings[key];
        }
        /// <summary>
        /// 开始loading
        /// </summary>
        /// <param name="key"></param>
        public void Start(object key)
        {
            if (loadings.ContainsKey(key) && !loadings[key])
            {
                loadings[key] = true;
            }
        }
        /// <summary>
        /// 停止loading
        /// </summary>
        /// <param name="key"></param>
        public void Stop(object key)
        {
            if (loadings.ContainsKey(key) && loadings[key])
            {
                loadings[key] = false;
            }
            
        }

    }
}
