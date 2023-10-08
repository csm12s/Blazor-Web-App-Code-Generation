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
    /// 系统作业任务服务
    /// </summary>
    public interface ISysJobDetailService : IServiceBase<SysJobDetailDto, int>
    {
        /// <summary>
        /// 获取触发器列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<SysJobTriggerDto>> GetTriggers(int id);

        /// <summary>
        /// 暂停作业
        /// </summary>
        /// <param name="id"></param>
        Task<bool> Pause(int id);

        /// <summary>
        /// 启动作业
        /// </summary>
        /// <param name="id"></param>
        Task<bool> Start(int id);

        /// <summary>
        /// 执行作业
        /// </summary>
        /// <param name="id"></param>
        Task<bool> Run(int id);

        /// <summary>
        /// 暂停所有作业
        /// </summary>
        /// <returns></returns>
        Task<bool> PauseAll();

        /// <summary>
        /// 启动所有作业
        /// </summary>
        /// <returns></returns>
        Task<bool> StartAll();

        /// <summary>
        /// 强制唤醒作业调度器
        /// </summary>
        Task<bool> CancelSleep();

        /// <summary>
        /// 强制触发所有作业持久化
        /// </summary>
        Task<bool> PersistAll();
    }
}
