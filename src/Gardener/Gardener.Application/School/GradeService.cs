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
