// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace Gardener.Client.Core
{
    public interface IClientErrorNotifier
    {
        Task Error(string msg, Exception ex = null);
        Task Info(string msg, Exception ex = null);
        Task Success(string msg, Exception ex = null);
        Task Warn(string msg, Exception ex = null);
    }
}