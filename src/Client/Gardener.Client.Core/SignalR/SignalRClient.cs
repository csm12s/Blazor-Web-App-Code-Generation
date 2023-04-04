// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------
#nullable enable

using Gardener.Client.Base;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Gardener.Client.Core
{
    /// <summary>
    /// signalr client
    /// </summary>
    public class SignalRClient : ISignalRClient
    {
        /// <summary>
        /// 客户端名称
        /// </summary>
        public string ClientName { get; }

        /// <summary>
        /// 是否自动重连
        /// </summary>
        private bool _automaticReconnect = true;
        /// <summary>
        /// 连接
        /// </summary>
        private HubConnection? connection = null;
        /// <summary>
        /// 连接地址
        /// </summary>
        private readonly string url;
        /// <summary>
        /// 日志记录
        /// </summary>
        private readonly IClientLogger clientLogger;
        /// <summary>
        /// 身份token提供者
        /// </summary>
        private readonly Func<Task<string?>> accessTokenProvider;
        /// <summary>
        /// 消息请求回调集合
        /// </summary>
        private readonly List<MessageCallBackInfo> messageCallBacks = new List<MessageCallBackInfo>();
        /// <summary>
        /// signalr client
        /// </summary>
        /// <param name="clientName">客户端唯一名称</param>
        /// <param name="url">连接地址</param>
        /// <param name="clientLogger">日志记录</param>
        /// <param name="accessTokenProvider">身份token提供方法</param>
        public SignalRClient(string clientName, string url, IClientLogger clientLogger, Func<Task<string?>> accessTokenProvider)
        {
            this.ClientName = clientName;
            this.url = url;
            this.clientLogger = clientLogger;
            this.accessTokenProvider = accessTokenProvider;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        public Func<Exception?, Task>? Closed { get; set; }
        /// <summary>
        /// 重连中
        /// </summary>
        public Func<Exception?, Task>? Reconnecting { get; set; }
        /// <summary>
        /// 重连后
        /// </summary>
        public Func<string?, Task>? Reconnected { get; set; }
        /// <summary>
        /// 连接断开
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task ConnectionClosed(Exception? arg)
        {
            clientLogger.Warn($"{ClientName}连接{(arg == null ? "关闭" : "中断")}", ex: arg, sendNotify: arg != null);
            if (Closed != null)
            {
                //回调
                return Closed.Invoke(arg);
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// 连接重连中
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task ConnectionReconnecting(Exception? arg)
        {
            clientLogger.Warn($"{ClientName}重连中", ex: arg);
            if (Reconnecting != null)
            {
                return Reconnecting.Invoke(arg);
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// 连接重连完成
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private Task ConnectionReconnected(string? arg)
        {
            clientLogger.Warn($"{ClientName}重连完成![{(arg == null ? "" : arg)}]");
            if (Reconnected != null)
            {
                return Reconnected.Invoke(arg);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 自动重连
        /// </summary>
        /// <param name="enable"></param>
        /// <returns></returns>
        public ISignalRClient AutomaticReconnect(bool enable = true)
        {
            this._automaticReconnect = enable;
            return this;
        }

        /// <summary>
        /// 创建连接并开启
        /// </summary>
        public Task Connection()
        {
            if (connection != null)
            {
                if (connection.State.Equals(HubConnectionState.Disconnected))
                {
                    return connection.StartAsync();
                }
                return Task.CompletedTask;
            }
            try
            {
                Uri uri = new Uri(url);
                clientLogger.Info($"{ClientName}开始连接,url={uri.AbsoluteUri}");

                IHubConnectionBuilder builder = new HubConnectionBuilder()
                            //配置请求
                            .WithUrl(uri, options =>
                            {
                                options.AccessTokenProvider = accessTokenProvider;
                                options.HttpMessageHandlerFactory = innerHandler => new IncludeRequestCredentialsMessageHandler(innerHandler);
                            });

                if (this._automaticReconnect)
                {
                    //配置重连
                    builder.WithAutomaticReconnect();
                }
                connection = builder.Build();
                if (connection != null)
                {
                    connection.Reconnected += ConnectionReconnected;
                    connection.Reconnecting += ConnectionReconnecting;
                    connection.Closed += ConnectionClosed;

                    //把暂存的订阅上
                    foreach (var item in messageCallBacks)
                    {
                        connection.On(item.MethodName, item.ArgumentTypes, item.CallBack);
                    }
                    return connection.StartAsync();
                }
            }
            catch (Exception ex)
            {
                clientLogger.Error($"{ClientName}连接异常", ex: ex);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 停止
        /// </summary>
        public async Task Stop()
        {

            if (connection != null && connection.State == HubConnectionState.Connected)
            {
                await connection.StopAsync();
            }
        }

        /// <summary>
        /// 开启
        /// </summary>
        public async Task Start()
        {
            if (connection != null && connection.State == HubConnectionState.Disconnected)
            {
                await connection.StartAsync();
            }
        }
        /// <summary>
        /// 释放
        /// </summary>
        public async Task Dispose()
        {
            if (connection != null)
            {
                await connection.DisposeAsync();
                connection = null;
            }
        }
        /// <summary>
        /// 发送消息到服务端
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Task SendAsync(string methodName, params object[] args)
        {
            if (connection == null)
            {
                throw new ArgumentNullException("connection is null");
            }
            var methods = typeof(HubConnectionExtensions).GetMethods();
            //反射调用
            MethodInfo methodInfo = (from u in typeof(HubConnectionExtensions).GetMethods()
                                     where u.Name == "SendAsync" && u.GetParameters().Length == args.Length + 3
                                     select u).First();
            List<object> arguments = new List<object>();
            arguments.Add(connection);
            arguments.Add(methodName);
            if (args.Length > 0)
            {
                arguments.AddRange(args);
            }
            arguments.Add(default(CancellationToken));
            object? task = methodInfo.Invoke(null, arguments.ToArray());
            if (task != null)
            {
                return (Task)task;
            }
            return Task.CompletedTask;
        }

        #region 暂时封装4个参数足够了吧，不行再加吧
        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        public ISignalRClient On(string methodName, Action resutHandler)
        {
            if (connection == null)
            {
                //暂存
                messageCallBacks.Add(new MessageCallBackInfo(methodName, resutHandler));
            }
            else
            {
                connection.On(methodName, resutHandler);
            }
            return this;
        }

        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        public ISignalRClient On(string methodName, Func<Task> resutHandler)
        {
            if (connection == null)
            {
                //暂存
                messageCallBacks.Add(new MessageCallBackInfo(methodName, resutHandler));
            }
            else
            {
                connection.On(methodName, resutHandler);
            }
            return this;
        }

        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        public ISignalRClient On<TResut>(string methodName, Action<TResut?> resutHandler)
        {
            if (connection == null)
            {
                Action<object?> callBack = data =>
                {
                    resutHandler(data == null ? default(TResut) : (TResut)data);
                };
                //暂存
                messageCallBacks.Add(new MessageCallBackInfo(methodName, typeof(TResut), callBack));
            }
            else
            {
                connection.On(methodName, resutHandler);
            }
            return this;
        }

        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        public ISignalRClient On<TResut>(string methodName, Func<TResut?, Task> resutHandler)
        {
            if (connection == null)
            {
                Func<object?, Task> callBack = data =>
                {
                    return resutHandler(data == null ? default(TResut) : (TResut)data);
                };
                //暂存
                messageCallBacks.Add(new MessageCallBackInfo(methodName, typeof(TResut), callBack));
            }
            else
            {
                connection.On<TResut>(methodName, resutHandler);
            }
            return this;
        }

        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut1"></typeparam>
        /// <typeparam name="TResut2"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        public ISignalRClient On<TResut1, TResut2>(string methodName, Action<TResut1?, TResut2?> resutHandler)
        {
            if (connection == null)
            {
                Action<object?[]> callBack = datas =>
                {
                    object? resut1 = datas[0];
                    object? resut2 = datas[1];

                    resutHandler(resut1 == null ? default : (TResut1)resut1, resut2 == null ? default : (TResut2)resut2);
                };
                //暂存
                messageCallBacks.Add(new MessageCallBackInfo(methodName, new Type[] { typeof(TResut1), typeof(TResut2) }, callBack));
            }
            else
            {
                connection.On(methodName, resutHandler);
            }
            return this;
        }

        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut1"></typeparam>
        /// <typeparam name="TResut2"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        public ISignalRClient On<TResut1, TResut2>(string methodName, Func<TResut1?, TResut2?, Task> resutHandler)
        {
            if (connection == null)
            {
                Func<object?[], Task> callBack = datas =>
                {
                    object? resut1 = datas[0];
                    object? resut2 = datas[1];
                    TResut1? t1 = resut1 == null ? default : (TResut1)resut1;
                    TResut2? t2 = resut2 == null ? default : (TResut2)resut2;
                    return resutHandler(t1, t2);
                };
                //暂存
                messageCallBacks.Add(new MessageCallBackInfo(methodName, new Type[] { typeof(TResut1), typeof(TResut2) }, callBack));
            }
            else
            {
                connection.On(methodName, resutHandler);
            }
            return this;
        }

        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut1"></typeparam>
        /// <typeparam name="TResut2"></typeparam>
        /// <typeparam name="TResut3"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        public ISignalRClient On<TResut1, TResut2, TResut3>(string methodName, Action<TResut1?, TResut2?, TResut3?> resutHandler)
        {
            if (connection == null)
            {
                Action<object?[]> callBack = datas =>
                {
                    object? resut1 = datas[0];
                    object? resut2 = datas[1];
                    object? resut3 = datas[2];
                    TResut1? t1 = resut1 == null ? default : (TResut1)resut1;
                    TResut2? t2 = resut2 == null ? default : (TResut2)resut2;
                    TResut3? t3 = resut3 == null ? default : (TResut3)resut3;
                    resutHandler(t1, t2, t3);
                };
                //暂存
                messageCallBacks.Add(new MessageCallBackInfo(methodName, new Type[] { typeof(TResut1), typeof(TResut2), typeof(TResut3) }, callBack));
            }
            else
            {
                connection.On(methodName, resutHandler);
            }
            return this;
        }

        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut1"></typeparam>
        /// <typeparam name="TResut2"></typeparam>
        /// <typeparam name="TResut3"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        public ISignalRClient On<TResut1, TResut2, TResut3>(string methodName, Func<TResut1?, TResut2?, TResut3?, Task> resutHandler)
        {
            if (connection == null)
            {
                Func<object?[], Task> callBack = datas =>
                {
                    object? resut1 = datas[0];
                    object? resut2 = datas[1];
                    object? resut3 = datas[2];
                    TResut1? t1 = resut1 == null ? default : (TResut1)resut1;
                    TResut2? t2 = resut2 == null ? default : (TResut2)resut2;
                    TResut3? t3 = resut3 == null ? default : (TResut3)resut3;
                    return resutHandler(t1, t2, t3);
                };
                //暂存
                messageCallBacks.Add(new MessageCallBackInfo(methodName, new Type[] { typeof(TResut1), typeof(TResut2), typeof(TResut3) }, callBack));
            }
            else
            {
                connection.On(methodName, resutHandler);
            }
            return this;
        }

        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut1"></typeparam>
        /// <typeparam name="TResut2"></typeparam>
        /// <typeparam name="TResut3"></typeparam>
        /// <typeparam name="TResut4"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        public ISignalRClient On<TResut1, TResut2, TResut3, TResut4>(string methodName, Action<TResut1?, TResut2?, TResut3?, TResut4?> resutHandler)
        {
            if (connection == null)
            {
                Action<object?[]> callBack = datas =>
                {
                    object? resut1 = datas[0];
                    object? resut2 = datas[1];
                    object? resut3 = datas[2];
                    object? resut4 = datas[3];
                    TResut1? t1 = resut1 == null ? default : (TResut1)resut1;
                    TResut2? t2 = resut2 == null ? default : (TResut2)resut2;
                    TResut3? t3 = resut3 == null ? default : (TResut3)resut3;
                    TResut4? t4 = resut4 == null ? default : (TResut4)resut4;
                    resutHandler(t1, t2, t3, t4);
                };
                //暂存
                messageCallBacks.Add(new MessageCallBackInfo(methodName, new Type[] { typeof(TResut1), typeof(TResut2), typeof(TResut3), typeof(TResut4) }, callBack));
            }
            else
            {
                connection.On(methodName, resutHandler);
            }
            return this;
        }


        /// <summary>
        /// 当收到消息时处理
        /// </summary>
        /// <typeparam name="TResut1"></typeparam>
        /// <typeparam name="TResut2"></typeparam>
        /// <typeparam name="TResut3"></typeparam>
        /// <typeparam name="TResut4"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        public ISignalRClient On<TResut1, TResut2, TResut3, TResut4>(string methodName, Func<TResut1?, TResut2?, TResut3?, TResut4?, Task> resutHandler)
        {
            if (connection == null)
            {
                Func<object?[], Task> callBack = datas =>
                {
                    object? resut1 = datas[0];
                    object? resut2 = datas[1];
                    object? resut3 = datas[2];
                    object? resut4 = datas[3];
                    TResut1? t1 = resut1 == null ? default : (TResut1)resut1;
                    TResut2? t2 = resut2 == null ? default : (TResut2)resut2;
                    TResut3? t3 = resut3 == null ? default : (TResut3)resut3;
                    TResut4? t4 = resut4 == null ? default : (TResut4)resut4;
                    return resutHandler(t1, t2, t3, t4);
                };
                //暂存
                messageCallBacks.Add(new MessageCallBackInfo(methodName, new Type[] { typeof(TResut1), typeof(TResut2), typeof(TResut3), typeof(TResut4) }, callBack));
            }
            else
            {
                connection.On(methodName, resutHandler);
            }
            return this;
        }

        #endregion

        /// <summary>
        /// 消息请求回调信息
        /// </summary>
        private class MessageCallBackInfo
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="methodName"></param>
            /// <param name="callBack"></param>
            public MessageCallBackInfo(string methodName, Action callBack)
            {
                MethodName = methodName;
                ArgumentTypes = Type.EmptyTypes;
                CallBack = delegate (object?[] objs)
                {
                    callBack();
                    return Task.CompletedTask;
                };
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="methodName"></param>
            /// <param name="callBack"></param>
            public MessageCallBackInfo(string methodName, Func<Task> callBack)
            {
                MethodName = methodName;
                ArgumentTypes = Type.EmptyTypes;
                CallBack = delegate (object?[] objs)
                {
                    return callBack();
                };
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="methodName"></param>
            /// <param name="argType"></param>
            /// <param name="callBack"></param>
            public MessageCallBackInfo(string methodName, Type argType, Action<object?> callBack)
            {
                this.MethodName = methodName;
                this.ArgumentTypes = new Type[] { argType };
                CallBack = delegate (object?[] objs)
                {
                    callBack(objs[0]);
                    return Task.CompletedTask;
                };
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="methodName"></param>
            /// <param name="argType"></param>
            /// <param name="callBack"></param>
            public MessageCallBackInfo(string methodName, Type argType, Func<object?, Task> callBack)
            {
                this.MethodName = methodName;
                this.ArgumentTypes = new Type[] { argType };
                CallBack = delegate (object?[] objs)
                {
                    return callBack(objs[0]);
                };
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="methodName"></param>
            /// <param name="argumentTypes"></param>
            /// <param name="callBack"></param>
            public MessageCallBackInfo(string methodName, Type[] argumentTypes, Action<object?[]> callBack)
            {
                MethodName = methodName;
                ArgumentTypes = argumentTypes;
                CallBack = delegate (object?[] objs)
                {
                    callBack(objs);
                    return Task.CompletedTask;
                }; ;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="methodName"></param>
            /// <param name="argumentTypes"></param>
            /// <param name="callBack"></param>
            public MessageCallBackInfo(string methodName, Type[] argumentTypes, Func<object?[], Task> callBack)
            {
                MethodName = methodName;
                ArgumentTypes = argumentTypes;
                CallBack = delegate (object?[] objs)
                {
                    return callBack(objs);
                }; ;
            }

            /// <summary>
            /// 方法
            /// </summary>
            public string MethodName { get; set; }
            /// <summary>
            /// 参数类型
            /// </summary>
            public Type[] ArgumentTypes { get; set; }
            /// <summary>
            /// 回调
            /// </summary>
            public Func<object?[], Task> CallBack { get; set; }
        }
    }
}
