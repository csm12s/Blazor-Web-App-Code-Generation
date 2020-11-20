// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Core.Entites;
using Microsoft.AspNetCore.Mvc;

namespace Gardener.Application
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
