// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.NotificationSystem.Dtos;
using Gardener.Base;

namespace Gardener.NotificationSystem.Services
{
    /// <summary>
    /// 公告服务
    /// </summary>
    public interface IAnnouncementService : IServiceBase<AnnouncementDto, int>
    {
    }
}
