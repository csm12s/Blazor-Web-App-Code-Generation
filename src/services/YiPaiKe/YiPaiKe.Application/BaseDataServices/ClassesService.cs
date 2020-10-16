// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using YiPaiKe.Core.Entities;

namespace YiPaiKe.Application
{
    /// <summary>
    /// 班级服务
    /// </summary>
    [ApiDescriptionSettings("BaseDataServices")]
    public class ClassesService : ServiceBase<Classes>
    {
        /// <summary>
        /// 班级服务
        /// </summary>
        /// <param name="repository"></param>
        public ClassesService(IRepository<Classes> repository) : base(repository)
        {
        }
    }
}
