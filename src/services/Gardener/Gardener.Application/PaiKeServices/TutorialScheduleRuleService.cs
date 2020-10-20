// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using Gardener.Core.Entities;

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
