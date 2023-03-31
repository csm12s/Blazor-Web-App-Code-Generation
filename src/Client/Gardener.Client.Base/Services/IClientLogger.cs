// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    /// <summary>
    /// 客户端日志记录
    /// </summary>
    public interface IClientLogger
    {
        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        void Debug(string msg, int? code = null, Exception? ex = null);
        /// <summary>
        /// fatal
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        void Fatal(string msg, int? code = null, Exception? ex = null, bool sendNotify = true);
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        void Error(string msg, int? code = null, Exception? ex = null, bool sendNotify = true);
        /// <summary>
        /// Info
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        void Info(string msg, int? code = null, Exception? ex = null, bool sendNotify = false);
        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        void Warn(string msg, int? code = null, Exception? ex = null, bool sendNotify = true);
        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        Task DebugAsync(string msg, int? code = null, Exception? ex = null);
        /// <summary>
        /// fatal
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        Task FatalAsync(string msg, int? code = null, Exception? ex = null, bool sendNotify = true);
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        Task ErrorAsync(string msg, int? code = null, Exception? ex = null, bool sendNotify = true);
        /// <summary>
        /// Info
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        Task InfoAsync(string msg, int? code = null, Exception? ex = null, bool sendNotify = false);
        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        Task WarnAsync(string msg, int? code = null, Exception? ex = null, bool sendNotify = true);
    }
}
