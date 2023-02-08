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
        Task Debug(string msg, int? code = null, Exception ex = null);
        /// <summary>
        /// fatal
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        Task Fatal(string msg, int? code = null, Exception ex = null, bool sendNotify = true);
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        Task Error(string msg, int? code = null, Exception ex = null, bool sendNotify = true);
        /// <summary>
        /// Info
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        Task Info(string msg, int? code = null, Exception ex = null, bool sendNotify = false);
        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="code"></param>
        /// <param name="ex"></param>
        /// <param name="sendNotify"></param>
        Task Warn(string msg, int? code = null, Exception ex = null, bool sendNotify = true);
    }
}
