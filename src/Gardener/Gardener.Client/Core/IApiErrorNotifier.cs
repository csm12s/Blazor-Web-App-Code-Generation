// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace Gardener.Client
{
    public interface IApiErrorNotifier
    {
        Task Error(string msg, Exception ex = null);
        Task Info(string msg, Exception ex = null);
        Task Success(string msg, Exception ex = null);
        Task Warn(string msg, Exception ex = null);
        Task Warning(string msg, Exception ex = null);
    }
}