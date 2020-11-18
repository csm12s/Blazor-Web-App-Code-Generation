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

        public async Task Debug(string msg, Exception ex = null)
        {
            Console.WriteLine(ex?.Message);
            Console.WriteLine(ex?.StackTrace);
            await apiErrorNotifier.Info(msg);
        }

        public async Task Error(string msg, Exception ex = null)
        {
            Console.WriteLine(ex?.Message);
            Console.WriteLine(ex?.StackTrace);
            await apiErrorNotifier.Error(msg);
        }

        public async Task Fatal(string msg, Exception ex = null)
        {
            Console.WriteLine(ex?.Message);
            Console.WriteLine(ex?.StackTrace);
            await apiErrorNotifier.Error(msg);
        }

        public async Task Info(string msg, Exception ex = null)
        {
            Console.WriteLine(ex?.Message);
            Console.WriteLine(ex?.StackTrace);
            await apiErrorNotifier.Info(msg);
        }

        public async Task Warn(string msg, Exception ex = null)
        {
            Console.WriteLine(ex?.Message);
            Console.WriteLine(ex?.StackTrace);
            await apiErrorNotifier.Warn(msg);
        }
    }
}
