// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Microsoft.AspNetCore.SignalR.Client;
using System.Reflection;

namespace Gardener.NotificationSystem.Client.Core
{
    /// <summary>
    /// signalr client
    /// </summary>
    public class SignalRClient
    {
        private readonly static string serviceName = "通知服务";
        private HubConnection? connection = null;
        private readonly string url;
        private readonly IClientLogger clientLogger;
        private readonly Func<Task<string?>> accessTokenProvider;

        private readonly List<MessageCallBackInfo> messageCallBacks=new List<MessageCallBackInfo>();

        /// <summary>
        /// signalr client
        /// </summary>
        /// <param name="url">连接地址</param>
        /// <param name="clientLogger">日志记录</param>
        /// <param name="accessTokenProvider">身份token提供方法</param>
        public SignalRClient(string url, IClientLogger clientLogger,  Func<Task<string?>> accessTokenProvider)
        {
            this.url = url;
            this.clientLogger = clientLogger;
            this.accessTokenProvider = accessTokenProvider;
        }

        public Func<Exception?, Task>? Closed;

        public Func<Exception?, Task>? Reconnecting;

        public Func<string?, Task>? Reconnected;

        /// <summary>
        /// 连接断开
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private async Task ConnectionClosed(Exception? arg)
        {
            clientLogger.Warn($"{serviceName}连接{(arg == null ? "关闭" : "中断")}", ex: arg, sendNotify: arg != null);
            if (Closed != null)
            {
                //回调
                await Closed.Invoke(arg);
            }
        }
        /// <summary>
        /// 连接重连中
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private async Task ConnectionReconnecting(Exception? arg)
        {
            clientLogger.Warn($"{serviceName}重连中", ex: arg,sendNotify:false);
            if (Reconnecting != null)
            {
                await Reconnecting.Invoke(arg);
            }
        }
        /// <summary>
        /// 连接重连完成
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private async Task ConnectionReconnected(string? arg)
        {
            clientLogger.Warn($"{serviceName}重连完成![{(arg == null ? "" : arg)}]");
            if (Reconnected != null)
            {
                await Reconnected.Invoke(arg);
            }
        }

        /// <summary>
        /// 创建连接
        /// </summary>
        public Task Connection()
        {
            if (connection != null)
            {
                return Task.CompletedTask;
            }
            try
            {
                Uri uri = new Uri(url);
                clientLogger.Info($"{serviceName}开始连接,url={uri.AbsoluteUri}");
                connection = new HubConnectionBuilder()
                //配置请求
                .WithUrl(uri, options =>
                {
                    options.AccessTokenProvider = accessTokenProvider;
                    options.HttpMessageHandlerFactory = innerHandler => new IncludeRequestCredentialsMessageHandler(innerHandler);
                })
                //配置重连
                .WithAutomaticReconnect().Build();
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
                clientLogger.Error($"{serviceName}连接异常", ex: ex);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public async Task Close()
        {
            if (connection != null)
            {
                await connection.StopAsync();
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
        public SignalRClient On(string methodName, Action resutHandler)
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
        public SignalRClient On(string methodName, Func<Task> resutHandler)
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
        public SignalRClient On<TResut>(string methodName, Action<TResut> resutHandler)
        {
            if (connection == null)
            {
                Action<object> callBack = data => {
                    resutHandler((TResut)data);
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
        public SignalRClient On<TResut>(string methodName, Func<TResut,Task> resutHandler)
        {
            if (connection == null)
            {
                Func<object,Task> callBack = data => {
                   return resutHandler((TResut)data);
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
        /// <typeparam name="TResut"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        public SignalRClient On<TResut1, TResut2>(string methodName, Action<TResut1, TResut2> resutHandler)
        {
            if (connection == null)
            {
                Action<object[]> callBack = datas => {
                    resutHandler((TResut1)datas[0], (TResut2)datas[1]);
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
        /// <typeparam name="TResut"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        public SignalRClient On<TResut1, TResut2>(string methodName, Func<TResut1, TResut2,Task> resutHandler)
        {
            if (connection == null)
            {
                Func<object[],Task> callBack = datas => {
                   return resutHandler((TResut1)datas[0], (TResut2)datas[1]);
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
        /// <typeparam name="TResut"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        public SignalRClient On<TResut1, TResut2, TResut3>(string methodName, Action<TResut1, TResut2, TResut3> resutHandler)
        {
            if (connection == null)
            {
                Action<object[]> callBack = datas => {
                    resutHandler((TResut1)datas[0], (TResut2)datas[1], (TResut3)datas[2]);
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
        /// <typeparam name="TResut"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        public SignalRClient On<TResut1, TResut2, TResut3>(string methodName, Func<TResut1, TResut2, TResut3,Task> resutHandler)
        {
            if (connection == null)
            {
                Func<object[],Task> callBack = datas => {
                   return resutHandler((TResut1)datas[0], (TResut2)datas[1], (TResut3)datas[2]);
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
        /// <typeparam name="TResut"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        public SignalRClient On<TResut1, TResut2, TResut3, TResut4>(string methodName, Action<TResut1, TResut2, TResut3, TResut4> resutHandler)
        {
            if (connection == null)
            {
                Action<object[]> callBack = datas => {
                    resutHandler((TResut1)datas[0], (TResut2)datas[1], (TResut3)datas[2], (TResut4)datas[3]);
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
        /// <typeparam name="TResut"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="resutHandler"></param>
        public SignalRClient On<TResut1, TResut2, TResut3, TResut4>(string methodName, Func<TResut1, TResut2, TResut3, TResut4,Task> resutHandler)
        {
            if (connection == null)
            {
                Func<object[],Task> callBack = datas => {
                    return resutHandler((TResut1)datas[0], (TResut2)datas[1], (TResut3)datas[2], (TResut4)datas[3]);
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
                CallBack = delegate (object?[] objs) {
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
                CallBack = delegate (object?[] objs) {
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
                CallBack = delegate (object?[] objs) {
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
            public MessageCallBackInfo(string methodName, Type argType, Func<object?,Task> callBack)
            { 
                this.MethodName = methodName;
                this.ArgumentTypes = new Type[] { argType };
                CallBack = delegate (object?[] objs) {
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
                CallBack = delegate (object?[] objs) {
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
            public MessageCallBackInfo(string methodName, Type[] argumentTypes, Func<object?[],Task> callBack)
            {
                MethodName = methodName;
                ArgumentTypes = argumentTypes;
                CallBack = delegate (object?[] objs) {
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
            public Func<object?[],Task> CallBack { get; set; }
        }
    }
}
