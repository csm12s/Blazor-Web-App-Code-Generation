// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using Gardener.Client.Base;
using Gardener.LocalizationLocalizer;
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
        private readonly ILocalizationLocalizer localizer;
        /// <summary>
        /// 客户端异常记录器
        /// </summary>
        /// <param name="clientErrorNotifier"></param>
        /// <param name="logger"></param>
        /// <param name="localizer"></param>
        public ConsoleClientLogger(IClientNotifier clientErrorNotifier, ILogger<ConsoleClientLogger> logger, ILocalizationLocalizer localizer)
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
                        msg = $"{localizer[nameof(SharedLocalResource.Unauthorized)]}";
                        break;
                    case 403:
                        msg = $"{localizer[nameof(SharedLocalResource.Forbidden)]}";
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
            DebugAsync(msg, code, ex);
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
            ErrorAsync(msg, code, ex, sendNotify);
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
            FatalAsync(msg, code, ex, sendNotify);
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
            InfoAsync(msg, code, ex, sendNotify);
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
            WarnAsync(msg, code, ex, sendNotify);
        }
        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public Task DebugAsync(string msg, int? code = null, Exception? ex = null)
        {
            msg = FormatMsg($"{msg}", code);
            logger.LogDebug(ex, msg);
            return Task.CompletedTask;
        }
        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        /// <returns></returns>
        public Task FatalAsync(string msg, int? code = null, Exception? ex = null, bool sendNotify = true)
        {
            msg = FormatMsg($"{msg}", code);
            logger.LogCritical(ex, msg);
            if (sendNotify)
            {
                return clientErrorNotifier.ErrorAsync(msg, ex: ex);
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        /// <returns></returns>
        public Task ErrorAsync(string msg, int? code = null, Exception? ex = null, bool sendNotify = true)
        {
            msg = FormatMsg($"{msg}", code);
            logger.LogError(ex, msg);
            if (sendNotify)
            {
                return clientErrorNotifier.ErrorAsync(msg, ex: ex);
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// Info
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        /// <returns></returns>
        public Task InfoAsync(string msg, int? code = null, Exception? ex = null, bool sendNotify = false)
        {
            msg = FormatMsg($"{msg}", code);
            logger.LogInformation(ex, msg);
            if (sendNotify)
            {
                return clientErrorNotifier.InfoAsync(msg);
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        /// <returns></returns>
        public Task WarnAsync(string msg, int? code = null, Exception? ex = null, bool sendNotify = true)
        {
            msg = FormatMsg($"{msg}", code);
            logger.LogWarning(ex, msg);
            if (sendNotify)
            {
                return clientErrorNotifier.WarnAsync(msg, ex: ex);
            }
            return Task.CompletedTask;
        }
    }
}
