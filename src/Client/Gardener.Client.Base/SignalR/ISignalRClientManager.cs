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
    /// signalR client 管理
    /// </summary>
    /// <remarks>
    /// 实现ISignalRClientProvider接口后，将被自动调用接管
    /// </remarks>
    public interface ISignalRClientManager
    {
        /// <summary>
        /// 连接某个客户端并启动
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns></returns>
        Task<ISignalRClient> ConnectionAndStart(string clientName);
        /// <summary>
        /// 连接并启动所有客户端
        /// </summary>
        /// <returns></returns>
        Task ConnectionAndStartAll();
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="handle">存在时处理</param>
        /// <returns></returns>
        ISignalRClientManager Exist(string clientName, Action<ISignalRClient> handle);
        /// <summary>
        /// 根据名称获取客户端
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns></returns>
        ISignalRClient Get(string clientName);
        /// <summary>
        /// 管理客户端
        /// </summary>
        /// <param name="client"></param>
        /// <exception cref="Exception"></exception>
        ISignalRClientManager Manage(ISignalRClient client);
        /// <summary>
        /// 判断是否不存在
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="handle">不存在时处理</param>
        /// <returns></returns>        
        ISignalRClientManager NoExist(string clientName, Action<ISignalRClientManager> handle);
        /// <summary>
        /// 启动指定客户端
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns></returns>        
        Task Start(string clientName);
        /// <summary>
        /// 启动所有客户端
        /// </summary>
        /// <returns></returns>
        Task StartAll();
        /// <summary>
        /// 停止指定客户端
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns></returns>
        Task Stop(string clientName);
        /// <summary>
        /// 停止所有客户端
        /// </summary>
        /// <returns></returns>
        Task StopAll();
    }
}