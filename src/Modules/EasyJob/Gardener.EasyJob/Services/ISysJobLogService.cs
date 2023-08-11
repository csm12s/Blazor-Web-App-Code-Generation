// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.EasyJob.Dtos;

namespace Gardener.EasyJob.Services
{
    /// <summary>
    /// 定时任务日志服务
    /// </summary>
    public interface ISysJobLogService : IServiceBase<SysJobLogDto, long>
    {
    }
}
