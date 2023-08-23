// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Gardener.Authentication.Dtos;
using Gardener.Authentication.Enums;
using Gardener.EasyJob.Impl.Domains;
using Gardener.NotificationSystem.Core;

namespace Gardener.EasyJob.Impl.Core
{
    /// <summary>
    /// 将需要监听用户进行分组
    /// </summary>
    public class EasyJobNotificationHubGrouper : ISystemNotificationHubGrouper, IScoped
    {
        private readonly IRepository<SysJobUserConfig> userConfigRepository;
        /// <summary>
        /// 将需要监听用户进行分组
        /// </summary>
        /// <param name="userConfigRepository"></param>
        public EasyJobNotificationHubGrouper(IRepository<SysJobUserConfig> userConfigRepository)
        {
            this.userConfigRepository = userConfigRepository;
        }

        /// <summary>
        /// 将WoChat用户进行分组
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetGroupName(Identity identity)
        {
            IEnumerable<string> groups = new string[0];
            //目前仅支持用户
            if (!IdentityType.User.Equals(identity.IdentityType))
            {
                return groups;
            }
            bool exits = await userConfigRepository.AnyAsync(x => x.IdentityId.Equals(identity.Id) && x.IdentityType.Equals(identity.IdentityType) && x.IsDeleted == false && x.IsLocked == false);
            if (exits)
            {
                groups = new string[] { EasyJobConstant.EasyJobNotificationGroupName };
            }
            return groups;
        }
    }
}
