// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Core.Entites;
using Microsoft.AspNetCore.Mvc;

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
