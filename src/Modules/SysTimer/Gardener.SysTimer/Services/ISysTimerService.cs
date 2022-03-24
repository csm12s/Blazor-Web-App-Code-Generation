// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.SysTimer.Dtos;

namespace Gardener.SysTimer.Services
{
    public interface ISysTimerService:Base.IServiceBase<SysTimerDto, int>
    {
        void AddTimerJob(SysTimerDto input);

        void Start(SysTimerDto input);

        void Stop(StopJobInput input);

        void StartTimerJob();
    }
}
