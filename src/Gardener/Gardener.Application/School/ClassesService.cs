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
