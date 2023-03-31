// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using Gardener.Client.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gardener.Client.Core
{
    /// <summary>
    /// 客户端异常记录器
    /// </summary>
    [ScopedService]
    public class ConsoleClientLogger : IClientLogger
    {
        private readonly ILogger<ConsoleClientLogger> logger;
        private readonly IClientNotifier clientErrorNotifier;
        private readonly IClientLocalizer localizer;
        /// <summary>
        /// 客户端异常记录器
        /// </summary>
        /// <param name="clientErrorNotifier"></param>
        /// <param name="logger"></param>
        /// <param name="localizer"></param>
        public ConsoleClientLogger(IClientNotifier clientErrorNotifier, ILogger<ConsoleClientLogger> logger, IClientLocalizer localizer)
        {
            this.clientErrorNotifier = clientErrorNotifier;
            this.logger = logger;
            this.localizer = localizer;
        }
        /// <summary>
        /// 格式化消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private string FormatMsg(string msg, int? code = null)
        {
            if (code != null)
            {
                //特殊状态提示
                switch (code)
                {
                    case 401:
                        msg = $"{localizer[SharedLocalResource.Warn]}：{localizer[SharedLocalResource.Unauthorized]}";
                        break;
                    case 403:
                        msg = $"{localizer[SharedLocalResource.Warn]}：{localizer[SharedLocalResource.Forbidden]}";
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
        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        public void Debug(string msg, int? code = null, Exception? ex = null)
        {
            msg = FormatMsg($"{localizer[SharedLocalResource.Debug]}:{msg}", code);
            if (ex == null)
            {
                logger.LogDebug(msg);
            }
            else
            {
                logger.LogDebug(ex, msg);
            }
        }
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        public void Error(string msg, int? code = null, Exception? ex = null, bool sendNotify = true)
        {
            msg = FormatMsg($"{localizer[SharedLocalResource.Error]}:{msg}", code);
            logger.LogError(ex, msg);
            if (sendNotify)
            {
                clientErrorNotifier.Error(msg);
            }
        }
        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        public void Fatal(string msg, int? code = null, Exception? ex = null, bool sendNotify = true)
        {
            msg = FormatMsg($"{localizer[SharedLocalResource.FatalException]}:{msg}", code);
            logger.LogCritical(ex, msg);
            if (sendNotify)
            {
                clientErrorNotifier.Error(msg);
            }
        }
        /// <summary>
        /// Info
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        public void Info(string msg, int? code = null, Exception? ex = null, bool sendNotify = false)
        {
            msg = FormatMsg($"{localizer[SharedLocalResource.Info]}:{msg}", code);
            if (ex == null)
            {
                logger.LogInformation(msg);
            }
            else
            {
                logger.LogInformation(ex, msg);
            }
            if (sendNotify)
            {
                clientErrorNotifier.Info(msg);
            }
        }
        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        public void Warn(string msg, int? code = null, Exception? ex = null, bool sendNotify = true)
        {
            msg = FormatMsg($"{localizer[SharedLocalResource.Warn]}:{msg}", code);
            if (ex == null)
            {
                logger.LogWarning(msg);
            }
            else
            {
                logger.LogWarning(ex, msg);
            }
            if (sendNotify)
            {
                clientErrorNotifier.Warn(msg);
            }
        }

        public Task DebugAsync(string msg, int? code = null, Exception? ex = null)
        {
            throw new NotImplementedException();
        }

        public Task FatalAsync(string msg, int? code = null, Exception? ex = null, bool sendNotify = true)
        {
            throw new NotImplementedException();
        }

        public Task ErrorAsync(string msg, int? code = null, Exception? ex = null, bool sendNotify = true)
        {
            throw new NotImplementedException();
        }

        public Task InfoAsync(string msg, int? code = null, Exception? ex = null, bool sendNotify = false)
        {
            throw new NotImplementedException();
        }

        public Task WarnAsync(string msg, int? code = null, Exception? ex = null, bool sendNotify = true)
        {
            throw new NotImplementedException();
        }
    }
}
