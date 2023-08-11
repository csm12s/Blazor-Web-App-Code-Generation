// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Impl.Domains;
using Gardener.EasyJob.Services;
using Gardener.EntityFramwork;
using Microsoft.AspNetCore.Mvc;

namespace Gardener.EasyJob.Impl.Services
{
    /// <summary>
    /// 定时任务-执行日志
    /// </summary>
    [ApiDescriptionSettings("EasyJobServices")]
    public class SysJobLogService : ServiceBase<SysJobLog, SysJobLogDto, long>, ISysJobLogService
    {
        /// <summary>
        /// 定时任务-执行日志
        /// </summary>
        /// <param name="repository"></param>
        public SysJobLogService(IRepository<SysJobLog, MasterDbContextLocator> repository) : base(repository)
        {
        }
    }
}
