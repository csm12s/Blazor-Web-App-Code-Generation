// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    /// <summary>
    /// SignalR Client构建器
    /// </summary>
    public interface ISignalRClientBuilder
    {
        /// <summary>
        /// 获取一个新实例
        /// </summary>
        /// <returns></returns>
        ISignalRClientBuilder GetInstance();
        /// <summary>
        /// 设置URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        ISignalRClientBuilder SetUrl(string url);
        /// <summary>
        /// 设置客户端名称
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns></returns>
        ISignalRClientBuilder SetClientName(string clientName);
        /// <summary>
        /// 设置日志记录器
        /// </summary>
        /// <param name="clientLogger"></param>
        /// <returns></returns>
        ISignalRClientBuilder SetLogger(IClientLogger clientLogger);
        /// <summary>
        /// 设置token提供者
        /// </summary>
        /// <param name="accessTokenProvider"></param>
        /// <returns></returns>
        ISignalRClientBuilder SetAccessTokenProvider(Func<Task<string>> accessTokenProvider);
        /// <summary>
        /// 设置是否重连
        /// </summary>
        /// <param name="enableAutomaticReconnect"></param>
        /// <returns></returns>
        ISignalRClientBuilder SetEnableAutomaticReconnect(bool enableAutomaticReconnect = true);
        /// <summary>
        /// 构建client
        /// </summary>
        /// <returns></returns>
        ISignalRClient Build();
    }
}
