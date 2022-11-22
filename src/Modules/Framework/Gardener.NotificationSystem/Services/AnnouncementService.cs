// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.EntityFramwork;
using Gardener.NotificationSystem.Domains;
using Gardener.NotificationSystem.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Gardener.NotificationSystem.Services
{
    /// <summary>
    /// 公告服务
    /// </summary>
    [ApiDescriptionSettings("NotificationSystem")]
    public class AnnouncementService : ServiceBase<Announcement, AnnouncementDto>, IAnnouncementService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public AnnouncementService(IRepository<Announcement> repository) : base(repository)
        {
        }
    }
}
