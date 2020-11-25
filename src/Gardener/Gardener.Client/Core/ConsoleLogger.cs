// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Client.Services;
using System;
using System.Threading.Tasks;

namespace Gardener.Client
{
    public class ConsoleLogger : ILogger
    {

        private IApiErrorNotifier apiErrorNotifier;

        public ConsoleLogger(IApiErrorNotifier apiErrorNotifier)
        {
            this.apiErrorNotifier = apiErrorNotifier;
        }
        private string FormatMsg(string msg, int? code = null, Exception ex = null)
        {
            msg = "异常：" + msg;
            if (code != null)
            {
                msg += $"[{code}]";
            }
            return msg;
        }
        public async Task Debug(string msg, int? code = null, Exception ex = null)
        {
            msg = FormatMsg(msg, code, ex);
            Console.WriteLine(msg);
            Console.WriteLine(ex?.Message);
            Console.WriteLine(ex?.StackTrace);
        }

        public async Task Error(string msg, int? code = null, Exception ex = null)
        {
            msg = FormatMsg(msg, code, ex);
            Console.WriteLine(ex?.Message);
            Console.WriteLine(ex?.StackTrace);
            await apiErrorNotifier.Error(msg);
        }

        public async Task Fatal(string msg, int? code = null, Exception ex = null)
        {
            msg = FormatMsg(msg, code, ex);
            Console.WriteLine(ex?.Message);
            Console.WriteLine(ex?.StackTrace);
            await apiErrorNotifier.Error(msg);
        }

        public async Task Info(string msg, int? code = null, Exception ex = null)
        {
            msg = FormatMsg(msg, code, ex);
            Console.WriteLine(ex?.Message);
            Console.WriteLine(ex?.StackTrace);
            await apiErrorNotifier.Info(msg);
        }

        public async Task Warn(string msg, int? code = null, Exception ex = null)
        {
            msg = FormatMsg(msg, code, ex);
            Console.WriteLine(ex?.Message);
            Console.WriteLine(ex?.StackTrace);
            await apiErrorNotifier.Warn(msg);
        }
    }
}
