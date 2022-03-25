// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    public interface IClientLogger
    {
        Task Debug(string msg, int? code = null, Exception ex = null);
        Task Fatal(string msg, int? code = null, Exception ex = null, bool sendNotify = true);
        Task Error(string msg, int? code = null, Exception ex = null, bool sendNotify = true);
        Task Info(string msg, int? code = null, Exception ex = null, bool sendNotify = false);
        Task Warn(string msg, int? code = null, Exception ex = null, bool sendNotify = true);
    }
}
