// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.ToolBox.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.ToolBox.Services
{
    /// <summary>
    /// Cron示例服务
    /// </summary>
    public interface ICronExampleService
    {
        /// <summary>
        /// 获取Cron示例列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CronExample>> GetCronExamples();
        /// <summary>
        /// 检验Cron
        /// </summary>
        /// <param name="checkInput"></param>
        /// <returns></returns>
        Task<CronCheckResult> Check(CronCheckInput checkInput);
    }
}
