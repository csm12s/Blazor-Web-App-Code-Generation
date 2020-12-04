// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
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
            double duration = 3;
            await msgSvr.Success(msg, duration);
        }
        public async Task Error(string msg, Exception ex = null)
        {
            double duration = 3;
            await msgSvr.Error(msg, duration);
        }
        public async Task Warn(string msg, Exception ex = null)
        {
            double duration = 3;
            await msgSvr.Warn(msg, duration);
        }
        public async Task Warning(string msg, Exception ex = null)
        {
            double duration = 3;
            await msgSvr.Warning(msg, duration);
        }
        public async Task Info(string msg, Exception ex = null)
        {
            double duration = 3;
            await msgSvr.Info(msg, duration);
        }
    }
}
