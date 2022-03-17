// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.NotificationSystem.Dtos;
using Gardener.NotificationSystem.Impl.Domains;
using Gardener.NotificationSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gardener.NotificationSystem.Impl.Services
{
    /// <summary>
    /// 公告服务
    /// </summary>
    [ApiDescriptionSettings("NotificationSystem")]
    public class AnnouncementService : ServiceBase<Announcement, AnnouncementDto>, IAnnouncementService
    {
        public AnnouncementService(IRepository<Announcement> repository) : base(repository)
        {
        }
    }
}
