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
    /// 定时任务-触发器服务
    /// </summary>
    public interface ISysJobTriggerService : IServiceBase<SysJobTriggerDto, int>
    {
        /// <summary>
        /// 暂停触发器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Pause(int id);

        /// <summary>
        /// 启动触发器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Start(int id);
    }
}
