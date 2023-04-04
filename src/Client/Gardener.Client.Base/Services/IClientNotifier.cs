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
    /// Client Notifier
    /// </summary>
    public interface IClientNotifier
    {
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="description"></param>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        void Error(string description, string? title=null, Exception? ex = null);
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="description"></param>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        Task ErrorAsync(string description, string? title=null, Exception? ex = null);
        /// <summary>
        /// Info
        /// </summary>
        /// <param name="description"></param>
        /// <param name="title"></param>
        void Info(string description, string? title=null);
        /// <summary>
        /// Info
        /// </summary>
        /// <param name="description"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        Task InfoAsync(string description, string? title=null);
        /// <summary>
        /// Success
        /// </summary>
        /// <param name="description"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        void Success(string description, string? title=null);
        /// <summary>
        /// Success
        /// </summary>
        /// <param name="description"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        Task SuccessAsync(string description, string? title=null);
        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="description"></param>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        void Warn(string description, string? title=null, Exception? ex = null);
        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="description"></param>
        /// <param name="title"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        Task WarnAsync(string description, string? title=null, Exception? ex = null);
    }
}