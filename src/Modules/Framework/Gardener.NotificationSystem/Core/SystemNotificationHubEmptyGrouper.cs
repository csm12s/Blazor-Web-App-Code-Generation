// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Dtos;

namespace Gardener.NotificationSystem.Core
{
    /// <summary>
    /// 空的分组器
    /// </summary>
    public class SystemNotificationHubEmptyGrouper : ISystemNotificationHubGrouper
    {
        /// <summary>
        /// 获取组名
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Task<IEnumerable<string>> GetGroupName(Identity identity)
        {
            return Task.FromResult<IEnumerable<string>>(new string[0]);
        }
    }
}
