// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public interface IClientLogger
    {
        Task Debug(string msg, int? code = null, Exception ex = null);
        Task Fatal(string msg, int? code = null, Exception ex = null);
        Task Error(string msg, int? code = null, Exception ex = null);
        Task Info(string msg, int? code = null, Exception ex = null);
        Task Warn(string msg, int? code = null, Exception ex = null);
    }
}
