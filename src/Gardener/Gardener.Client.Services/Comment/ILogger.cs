// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public interface ILogger
    {
        Task Debug(string msg, int? code = null, Exception ex = null);
        Task Fatal(string msg, int? code = null, Exception ex = null);
        Task Error(string msg, int? code = null, Exception ex = null);
        Task Info(string msg, int? code = null, Exception ex = null);
        Task Warn(string msg, int? code = null, Exception ex = null);
    }
}
