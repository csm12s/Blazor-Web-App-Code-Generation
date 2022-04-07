// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Client.Base
{
    /// <summary>
    /// SignalRClient 提供者
    /// </summary>
    public interface ISignalRClientProvider
    {
        /// <summary>
        /// 创建client
        /// </summary>
        /// <returns></returns>
        ISignalRClient GetSignalRClient();
    }
}
