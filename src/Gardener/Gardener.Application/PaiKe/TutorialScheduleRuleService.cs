// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Core.Entites;
using Microsoft.AspNetCore.Mvc;

namespace Gardener.Application
{
    /// <summary>
    /// 上课安排规则服务
    /// </summary>
    [ApiDescriptionSettings("PaiKeServices")]
    public class TutorialScheduleRuleService : ServiceBase<TutorialScheduleRule>
    {
        /// <summary>
        /// 上课安排规则服务
        /// </summary>
        /// <param name="repository"></param>
        public TutorialScheduleRuleService(IRepository<TutorialScheduleRule> repository) : base(repository)
        {
        }
    }
}
