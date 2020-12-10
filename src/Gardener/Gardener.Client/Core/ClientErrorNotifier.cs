// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Services;
using System;
using System.Threading.Tasks;

namespace Gardener.Client
{
    public class ClientErrorNotifier : IClientErrorNotifier
    {
        private MessageService msgSvr;
        private double duration = 3;
        public ClientErrorNotifier(MessageService msgSvr)
        {
            this.msgSvr = msgSvr;
        }
        public async Task Success(string msg, Exception ex = null)
        {
            await msgSvr.Success(msg, duration);
        }
        public async Task Error(string msg, Exception ex = null)
        {
            await msgSvr.Error(msg, duration);
        }
        public async Task Warn(string msg, Exception ex = null)
        {
            await msgSvr.Warn(msg, duration);
        }
        public async Task Warning(string msg, Exception ex = null)
        {
            await msgSvr.Warning(msg, duration);
        }
        public async Task Info(string msg, Exception ex = null)
        {
            await msgSvr.Info(msg, duration);
        }
    }
}
