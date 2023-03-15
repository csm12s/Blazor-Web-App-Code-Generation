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
        Task Error(string description, Exception? ex = null);
        Task Error(string msg,string description, Exception? ex = null);
        Task Info(string description);
        Task Info(string msg, string description);
        Task Success(string description);
        Task Success(string msg, string description);
        Task Warn(string description, Exception? ex = null);
        Task Warn(string msg, string description, Exception? ex = null);
    }
}