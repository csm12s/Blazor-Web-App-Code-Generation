// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using YiPaiKe.Core.Entities;

namespace YiPaiKe.Application
{
    /// <summary>
    /// 年级服务
    /// </summary>
    [ApiDescriptionSettings("BaseDataServices")]
    public class GradeService : ServiceBase<Grade>
    {
        /// <summary>
        /// 年级服务
        /// </summary>
        /// <param name="repository"></param>
        public GradeService(IRepository<Grade> repository) : base(repository)
        {
        }
    }
}
