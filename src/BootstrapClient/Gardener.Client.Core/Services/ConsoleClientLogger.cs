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
        public Task Debug(string msg, int? code = null, Exception ex = null)
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
            return Task.CompletedTask;
        }
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        public Task Error(string msg, int? code = null, Exception ex = null, bool sendNotify = true)
        {
            msg = FormatMsg($"{localizer[SharedLocalResource.Error]}:{msg}", code);
            logger.LogError(ex, msg);
            if (sendNotify)
            {
                return clientErrorNotifier.Error(msg);
            }
            return Task.CompletedTask;
        }
        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        public Task Fatal(string msg, int? code = null, Exception ex = null, bool sendNotify = true)
        {
            msg = FormatMsg($"{localizer[SharedLocalResource.FatalException]}:{msg}", code);
            logger.LogCritical(ex, msg);
            if (sendNotify)
            {
                return clientErrorNotifier.Error(msg);
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
        public Task Info(string msg, int? code = null, Exception ex = null, bool sendNotify = false)
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
                return clientErrorNotifier.Info(msg);
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
        public Task Warn(string msg, int? code = null, Exception ex = null, bool sendNotify = true)
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
                return clientErrorNotifier.Warn(msg);
            }
            return Task.CompletedTask;
        }
    }
}
