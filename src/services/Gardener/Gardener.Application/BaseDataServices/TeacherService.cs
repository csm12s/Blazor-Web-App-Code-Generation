// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using Gardener.Core.Entities;

namespace Gardener.Application
{
    /// <summary>
    /// 老师服务
    /// </summary>
    [ApiDescriptionSettings("BaseDataServices")]
    public class TeacherService : ServiceBase<Teacher>
    {
        /// <summary>
        /// 老师服务
        /// </summary>
        /// <param name="repository"></param>
        public TeacherService(IRepository<Teacher> repository) : base(repository)
        {
        }
    }
}
