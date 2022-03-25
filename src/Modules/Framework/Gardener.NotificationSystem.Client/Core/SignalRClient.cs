// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Microsoft.AspNetCore.SignalR.Client;

namespace Gardener.NotificationSystem.Client.Core
{
    /// <summary>
    /// client
    /// </summary>
    public class SignalRClient
    {
        private HubConnection? connection = null;
        private readonly string url;
        private readonly IClientLogger clientLogger;
        private readonly Action<HubConnection> connectedCallBack;
        private readonly Func<Task<string?>> accessTokenProvider;
        public SignalRClient(string url, IClientLogger clientLogger, Action<HubConnection> connectedCallBack, Func<Task<string>> accessTokenProvider)
        {
            this.url = url;
            this.clientLogger = clientLogger;
            this.connectedCallBack = connectedCallBack;
            this.accessTokenProvider = accessTokenProvider;
        }

        public Func<Exception?, Task>? Closed;
               
        public Func<Exception?, Task>? Reconnecting;
               
        public Func<string?, Task>? Reconnected;

        private async Task ConnectionClosed(Exception? arg)
        {
            clientLogger.Warn("SignalR连接中断", ex: arg, sendNotify: false);
            if (Closed != null)
            {
                await Closed.Invoke(arg);
            }
        }
        private async Task ConnectionReconnecting(Exception? arg)
        {
            clientLogger.Warn("SignalR连接重连中", ex: arg);
            if (Reconnecting != null)
            {
                await Reconnecting.Invoke(arg);
            }
        }
        private async Task ConnectionReconnected(string? arg)
        {
            clientLogger.Warn("SignalR连接重连完成" + arg == null ? "" : arg);
            if (Reconnected != null)
            {
                await Reconnected.Invoke(arg);
            }
        }
        
        /// <summary>
        /// 返回连接
        /// </summary>
        /// <returns></returns>
        public HubConnection? GetHubConnection()
        {
            return connection;
        }
        /// <summary>
        /// 创建连接
        /// </summary>
        public async Task Connection()
        {
            if (connection != null)
            {
                return;
            }
            try
            {
                Uri uri = new Uri(url);
                clientLogger.Info($"SignalR开始连接 url={uri.AbsoluteUri}");
                connection = new HubConnectionBuilder()
                .WithUrl(uri, options =>
                {
                    options.AccessTokenProvider = accessTokenProvider;
                    options.HttpMessageHandlerFactory = innerHandler =>
                        new IncludeRequestCredentialsMessageHandler(innerHandler);
                }).Build();
                if (connection != null)
                {
                    connection.Reconnected += ConnectionReconnected;
                    connection.Reconnecting += ConnectionReconnecting;
                    connection.Closed += ConnectionClosed;
                    connectedCallBack.Invoke(connection);
                    await connection.StartAsync();
                    clientLogger.Info("SignalR连接成功 id=" + connection.ConnectionId);
                }
            }
            catch (Exception ex)
            {
                clientLogger.Error("SignalR连接异常", ex: ex);
            }
           
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
    }
}
