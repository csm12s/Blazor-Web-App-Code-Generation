// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    public interface IClientNotifier
    {
        void Error(string description, Exception? ex = null);
        void Error(string msg,string description, Exception? ex = null);
        void Info(string description);
        void Info(string msg, string description);
        void Success(string description);
        void Success(string msg, string description);
        void Warn(string description, Exception? ex = null);
        void Warn(string msg, string description, Exception? ex = null);
        Task ErrorAsync(string description, Exception? ex = null);
        Task ErrorAsync(string msg,string description, Exception? ex = null);
        Task InfoAsync(string description);
        Task InfoAsync(string msg, string description);
        Task SuccessAsync(string description);
        Task SuccessAsync(string msg, string description);
        Task WarnAsync(string description, Exception? ex = null);
        Task WarnAsync(string msg, string description, Exception? ex = null);
    }
}