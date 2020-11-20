// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using AntDesign;
using System;
using System.Threading.Tasks;

namespace Gardener.Client
{
    public class ApiErrorNotifier : IApiErrorNotifier
    {
        private MessageService msgSvr;

        public ApiErrorNotifier(MessageService msgSvr)
        {
            this.msgSvr = msgSvr;
        }
        public async Task Success(string msg, Exception ex = null)
        {
            double duration = 1;
            await msgSvr.Success(msg, duration);
        }
        public async Task Error(string msg, Exception ex = null)
        {
            double duration = 1;
            await msgSvr.Error(msg, duration);
        }
        public async Task Warn(string msg, Exception ex = null)
        {
            double duration = 1;
            await msgSvr.Warn(msg, duration);
        }
        public async Task Warning(string msg, Exception ex = null)
        {
            double duration = 1;
            await msgSvr.Warning(msg, duration);
        }
        public async Task Info(string msg, Exception ex = null)
        {
            double duration = 1;
            await msgSvr.Info(msg, duration);
        }
    }
}
