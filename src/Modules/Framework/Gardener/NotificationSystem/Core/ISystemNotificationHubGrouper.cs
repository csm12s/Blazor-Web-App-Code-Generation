// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.NotificationSystem.Core
{
    /// <summary>
    /// 系统通知hub分组器
    /// </summary>
    public interface ISystemNotificationHubGrouper
    {
        /// <summary>
        /// 获取组名
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 如果返回空集合将不分组
        /// </remarks>
        Task<IEnumerable<string>> GetGroupName(Identity identity);
    }
}
