// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Core.Entites;
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
