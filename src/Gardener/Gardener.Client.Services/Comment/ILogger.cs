// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public interface ILogger
    {
        Task Debug(string msg, Exception ex = null);
        Task Fatal(string msg, Exception ex = null);
        Task Error(string msg, Exception ex = null);
        Task Info(string msg, Exception ex = null);
        Task Warn(string msg, Exception ex = null);
    }
}
