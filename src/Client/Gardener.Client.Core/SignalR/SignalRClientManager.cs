// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Core
{
    /// <summary>
    /// signalR client 管理
    /// </summary>
    public class SignalRClientManager : ISignalRClientManager
    {

        private readonly IClientLogger clientLogger;
        public SignalRClientManager(IClientLogger clientLogger,
            IServiceProvider serviceProvider)
        {
            this.clientLogger = clientLogger;
            this.ScanClients(serviceProvider);
        }
        /// <summary>
        /// 初始化来自于client提供者的client
        /// </summary>
        /// <param name="serviceProvider"></param>
        private void ScanClients(IServiceProvider serviceProvider)
        {
            serviceProvider.GetServices<ISignalRClientProvider>().ToList().ForEach(x =>
            {
                Manage(x.GetSignalRClient());
            });
        }


        /// <summary>
        /// 客户端集合
        /// </summary>
        private List<ISignalRClient> Clients { get; } = new List<ISignalRClient>();

        /// <summary>
        /// 管理客户端
        /// </summary>
        /// <param name="client"></param>
        /// <exception cref="Exception"></exception>
        public ISignalRClientManager Manage(ISignalRClient client)
        {
            //判断是否已存在
            if (Clients.Any(x => x.ClientName == client.ClientName))
            {
                throw new Exception($"客户端名称{client.ClientName}已存在");
            }
            Clients.Add(client);
            clientLogger.Info($"SignalRClientManager 接管 {client.ClientName}");
            return this;
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="handle">存在时处理</param>
        /// <returns></returns>
        public ISignalRClientManager Exist(string clientName, Action<ISignalRClient> handle)
        {
            if (Clients.Any(x => x.ClientName.Equals(clientName)))
            {
                var client = Get(clientName);
                handle(client);
            }
            return this;
        }

        /// <summary>
        /// 判断是否不存在
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="handle">不存在时处理</param>
        /// <returns></returns>
        public ISignalRClientManager NoExist(string clientName, Action<ISignalRClientManager> handle)
        {
            if (!Clients.Any(x => x.ClientName.Equals(clientName)))
            {
                handle(this);
            }
            return this;
        }

        /// <summary>
        /// 根据名称获取客户端
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public ISignalRClient Get(string clientName)
        {
            ISignalRClient? client = Clients.FirstOrDefault(x => x.ClientName.Equals(clientName));
            if (client != null)
            {
                return client;
            }
            throw new Exception($"客户端名称{clientName}不存在");
        }

        /// <summary>
        /// 连接某个客户端并启动
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public async Task<ISignalRClient> ConnectionAndStart(string clientName)
        {
            ISignalRClient client = Get(clientName);
            await client.Connection();
            return client;
        }

        /// <summary>
        /// 连接并启动所有客户端
        /// </summary>
        /// <returns></returns>
        public Task ConnectionAndStartAll()
        {
            return Clients.ForEachAsync(async x => await x.Connection());
        }

        /// <summary>
        /// 停止指定客户端
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public Task Stop(string clientName)
        {
            ISignalRClient client = Get(clientName);
            return client.Stop();
        }

        /// <summary>
        /// 停止所有客户端
        /// </summary>
        /// <returns></returns>
        public Task StopAll()
        {
           return Clients.ForEachAsync(async x => await x.Stop());
        }

        /// <summary>
        /// 启动指定客户端
        /// </summary>
        /// <param name="clientName"></param>
        /// <returns></returns>
        public Task Start(string clientName)
        {
            ISignalRClient client = Get(clientName);
            return client.Start();
        }

        /// <summary>
        /// 启动所有客户端
        /// </summary>
        /// <returns></returns>
        public Task StartAll()
        {
           return Clients.ForEachAsync(async x => await x.Start());
        }
    }
}
