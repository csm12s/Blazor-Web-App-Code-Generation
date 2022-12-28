// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;

namespace Gardener.Client.Base
{
    /// <summary>
    /// 本地化器
    /// </summary>
    public interface IClientLocalizer
    {
        string this[string name]
        {
            get;
        }
        public string Combination(params string[] names);

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetValue(string name);

        /// <summary>
        /// 获取本地化器名称
        /// </summary>
        /// <returns></returns>
        public string GetLocalizerName();

    }
    /// <summary>
    /// 本地化器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IClientLocalizer<T> : IClientLocalizer
    {

    }
}
