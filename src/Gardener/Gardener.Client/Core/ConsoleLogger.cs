// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
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
            if (code != null)
            {
                //特殊状态提示
                switch (code)
                {
                    case 401 :
                        msg = "提示：登录验证失败";
                        break;
                    case 403:
                        msg = "提示：资源权限验证失败";
                        break;
                }

                msg += $"[{code}]";
            }
            return msg;
        }
        public async Task Debug(string msg, int? code = null, Exception ex = null)
        {
            msg = FormatMsg($"调试:{msg}", code, ex);
            Console.WriteLine(msg);
            Console.WriteLine(ex?.Message);
            Console.WriteLine(ex?.StackTrace);
        }

        public async Task Error(string msg, int? code = null, Exception ex = null)
        {
            msg = FormatMsg($"异常:{msg}", code, ex);
            Console.WriteLine(ex?.Message);
            Console.WriteLine(ex?.StackTrace);
            await apiErrorNotifier.Error(msg);
        }

        public async Task Fatal(string msg, int? code = null, Exception ex = null)
        {
            msg = FormatMsg($"致命异常:{msg}", code, ex);
            Console.WriteLine(ex?.Message);
            Console.WriteLine(ex?.StackTrace);
            await apiErrorNotifier.Error(msg);
        }

        public async Task Info(string msg, int? code = null, Exception ex = null)
        {
            msg = FormatMsg($"提示:{msg}", code, ex);
            Console.WriteLine(ex?.Message);
            Console.WriteLine(ex?.StackTrace);
            await apiErrorNotifier.Info(msg);
        }

        public async Task Warn(string msg, int? code = null, Exception ex = null)
        {
            msg = FormatMsg($"警告:{msg}", code, ex);
            Console.WriteLine(ex?.Message);
            Console.WriteLine(ex?.StackTrace);
            await apiErrorNotifier.Warn(msg);
        }
    }
}
