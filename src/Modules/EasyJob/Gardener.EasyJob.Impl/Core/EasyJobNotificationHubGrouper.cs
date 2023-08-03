// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Gardener.Authentication.Dtos;
using Gardener.Authentication.Enums;
using Gardener.NotificationSystem.Core;

namespace Gardener.EasyJob.Impl.Core
{
    /// <summary>
    /// 将需要监听用户进行分组-
    /// </summary>
    public class EasyJobNotificationHubGrouper : ISystemNotificationHubGrouper, IScoped
    {

        /// <summary>
        /// 将WoChat用户进行分组
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public Task<IEnumerable<string>> GetGroupName(Identity identity)
        {
            IEnumerable<string> groups = new string[0];
            //目前仅支持用户
            if (!IdentityType.User.Equals(identity.IdentityType))
            {
                return Task.FromResult(groups);
            }
            groups = new string[] { EasyJobConstant.EasyJobNotificationGroupName };
            return Task.FromResult(groups);
        }
    }
}
