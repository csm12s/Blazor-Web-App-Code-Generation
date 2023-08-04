// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using Gardener.Authentication.Core;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Impl.Core;
using Gardener.EasyJob.Impl.Domains;
using Gardener.EasyJob.Services;
using Gardener.NotificationSystem.Core;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Gardener.EasyJob.Impl.Services
{
    /// <summary>
    /// 定时任务-用户配置服务
    /// </summary>
    [ApiDescriptionSettings("EasyJobServices")]
    public class EasyJobUserConfigService : IEasyJobUserConfigService, IDynamicApiController
    {
        private readonly IIdentityService identityService;
        private readonly IRepository<EasyJobUserConfig> userConfigRepository;
        private readonly ISystemNotificationService systemNotificationService;
        /// <summary>
        /// 定时任务-用户配置服务
        /// </summary>
        /// <param name="identityService"></param>
        /// <param name="userConfigRepository"></param>
        /// <param name="systemNotificationService"></param>
        public EasyJobUserConfigService(IIdentityService identityService, IRepository<EasyJobUserConfig> userConfigRepository, ISystemNotificationService systemNotificationService)
        {
            this.identityService = identityService;
            this.userConfigRepository = userConfigRepository;
            this.systemNotificationService = systemNotificationService;
        }

        /// <summary>
        /// 获取我的的配置
        /// </summary>
        /// <returns></returns>
        public async Task<EasyJobUserConfigDto?> GetMyConfig()
        {
            var identity = identityService.GetIdentity();
            if (identity == null)
            {
                return null;
            }
            EasyJobUserConfig? userConfig = await userConfigRepository.Where(x => x.IdentityId.Equals(identity.Id) && x.IdentityType.Equals(identity.IdentityType) && x.IsDeleted == false && x.IsLocked == false).FirstOrDefaultAsync();
            if (userConfig == null)
            {
                userConfig = new EasyJobUserConfig();
                userConfig.IdentityType = identity.IdentityType;
                userConfig.IdentityId = identity.Id;
                userConfig.EnableRealTimeMonitor = false;
            }
            return userConfig.Adapt<EasyJobUserConfigDto>();
        }

        /// <summary>
        /// 保存我的配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public async Task<EasyJobUserConfigDto?> SaveMyConfig(EasyJobUserConfigDto config)
        {
            var identity = identityService.GetIdentity();
            if (identity == null)
            {
                return null;
            }
            EasyJobUserConfigDto? result;
            EasyJobUserConfig? userConfig = await userConfigRepository.Where(x => x.IdentityId.Equals(identity.Id) && x.IdentityType.Equals(identity.IdentityType) && x.IsDeleted == false && x.IsLocked == false).FirstOrDefaultAsync();
            if (userConfig != null)
            {
                userConfig.EnableRealTimeMonitor = config.EnableRealTimeMonitor;
                EntityEntry<EasyJobUserConfig> entity = await userConfigRepository.UpdateAsync(userConfig);
                result = entity.Entity.Adapt<EasyJobUserConfigDto>();
            }
            else
            {
                userConfig = new EasyJobUserConfig();
                config.Adapt(userConfig);
                EntityEntry<EasyJobUserConfig> entity = await userConfigRepository.InsertAsync(userConfig);
                result = entity.Entity.Adapt<EasyJobUserConfigDto>();
            }
            //控制通知系统
            if (config.EnableRealTimeMonitor)
            {
                await systemNotificationService.UserGroupAdd<EasyJobNotificationHubGrouper>(identity);
            }
            else 
            {
                await systemNotificationService.UserGroupRemove<EasyJobNotificationHubGrouper>(identity);
            }
            return result;
        }
    }
}
