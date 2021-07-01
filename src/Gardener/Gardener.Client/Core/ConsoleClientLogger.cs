// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gardener.Client
{
    public class ConsoleClientLogger : IClientLogger
    {
        private readonly ILogger<ConsoleClientLogger> logger;
        private readonly IClientErrorNotifier clientErrorNotifier;

        public ConsoleClientLogger(IClientErrorNotifier clientErrorNotifier, ILogger<ConsoleClientLogger> logger)
        {
            this.clientErrorNotifier = clientErrorNotifier;
            this.logger = logger;
        }
        private string FormatMsg(string msg, int? code = null)
        {
            if (code != null)
            {
                //特殊状态提示
                switch (code)
                {
                    case 401:
                        msg = "提示：登录验证失败";
                        break;
                    case 403:
                        msg = "提示：资源权限验证失败";
                        break;
                }

                msg += $"[{code}]";
            }
            if (!string.IsNullOrEmpty(msg))
            {
                msg = Regex.Unescape(msg);
            }
            return msg;
        }
        public async Task Debug(string msg, int? code = null, Exception ex = null)
        {
            msg = FormatMsg($"调试:{msg}", code);
            if (ex == null)
            {
                logger.LogDebug(msg);
            }
            else
            {
                logger.LogDebug(ex, msg);
            }
        }
        public async Task Error(string msg, int? code = null, Exception ex = null)
        {
            msg = FormatMsg($"异常:{msg}", code);
            logger.LogError(ex, msg);
            await clientErrorNotifier.Error(msg);
        }

        public async Task Fatal(string msg, int? code = null, Exception ex = null)
        {
            msg = FormatMsg($"致命异常:{msg}", code);
            logger.LogCritical(ex, msg);
            await clientErrorNotifier.Error(msg);
        }

        public async Task Info(string msg, int? code = null, Exception ex = null)
        {
            msg = FormatMsg($"提示:{msg}", code);
            if (ex == null)
            {
                logger.LogInformation(msg);
            }
            else 
            {
                logger.LogInformation(ex, msg);
            }
            await clientErrorNotifier.Info(msg);
        }

        public async Task Warn(string msg, int? code = null, Exception ex = null)
        {
            msg = FormatMsg($"警告:{msg}", code);
            if (ex == null)
            {
                logger.LogWarning(msg);
            }
            else
            {
                logger.LogWarning(ex, msg);
            }
            await clientErrorNotifier.Warn(msg);
        }
    }
}
