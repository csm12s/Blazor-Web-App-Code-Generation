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
    /// signalr client
    /// </summary>
    public interface ISignalRClient
    {
        /// <summary>
        /// 客户端名称
        /// </summary>
        string ClientName { get; }
        /// <summary>
        /// 关闭
        /// </summary>
        Func<Exception?, Task>? Closed { get; set; }
        /// <summary>
        /// 重连中
        /// </summary>
        Func<Exception?, Task>? Reconnecting { get; set; }
        /// <summary>
        /// 重连后
        /// </summary>
        Func<string?, Task>? Reconnected { get; set; }
        /// <summary>
        /// 是否自动重连
        /// </summary>
        ISignalRClient AutomaticReconnect(bool enable = true);
        /// <summary>
        /// 创建连接并开启
        /// </summary>
        Task Connection();
        /// <summary>
        /// 释放
        /// </summary>
        Task Dispose();
        /// <summary>
        /// 开启
        /// </summary>
        Task Start();
        /// <summary>
        /// 停止
        /// </summary>
        Task Stop();
        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        ISignalRClient On(string methodName, Action resutHandler);
        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        ISignalRClient On(string methodName, Func<Task> resutHandler);
        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        ISignalRClient On<TResut>(string methodName, Action<TResut> resutHandler);
        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        ISignalRClient On<TResut>(string methodName, Func<TResut, Task> resutHandler);
        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut1"></typeparam>
        /// <typeparam name="TResut2"></typeparam>
        /// <typeparam name="TResut3"></typeparam>
        /// <typeparam name="TResut4"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        ISignalRClient On<TResut1, TResut2, TResut3, TResut4>(string methodName, Action<TResut1, TResut2, TResut3, TResut4> resutHandler);
        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut1"></typeparam>
        /// <typeparam name="TResut2"></typeparam>
        /// <typeparam name="TResut3"></typeparam>
        /// <typeparam name="TResut4"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        ISignalRClient On<TResut1, TResut2, TResut3, TResut4>(string methodName, Func<TResut1, TResut2, TResut3, TResut4, Task> resutHandler);
        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut1"></typeparam>
        /// <typeparam name="TResut2"></typeparam>
        /// <typeparam name="TResut3"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        ISignalRClient On<TResut1, TResut2, TResut3>(string methodName, Action<TResut1, TResut2, TResut3> resutHandler);
        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut1"></typeparam>
        /// <typeparam name="TResut2"></typeparam>
        /// <typeparam name="TResut3"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        ISignalRClient On<TResut1, TResut2, TResut3>(string methodName, Func<TResut1, TResut2, TResut3, Task> resutHandler);
        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut1"></typeparam>
        /// <typeparam name="TResut2"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        ISignalRClient On<TResut1, TResut2>(string methodName, Action<TResut1, TResut2> resutHandler);
        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut1"></typeparam>
        /// <typeparam name="TResut2"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        ISignalRClient On<TResut1, TResut2>(string methodName, Func<TResut1, TResut2, Task> resutHandler);
        /// <summary>
        /// 发送消息到服务端
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        Task SendAsync(string methodName, params object[] args);
        
    }
}