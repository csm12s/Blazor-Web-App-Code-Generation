// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using Gardener.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Gardener.Application
{
    /// <summary>
    /// 课程服务
    /// </summary>
    [ApiDescriptionSettings("BaseDataServices")]
    public class CurriculumService : ServiceBase<Curriculum>
    {
        /// <summary>
        /// 课程服务
        /// </summary>
        /// <param name="repository"></param>
        public CurriculumService(IRepository<Curriculum> repository) : base(repository)
        {
        }
    }
}
