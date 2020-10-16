﻿// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using YiPaiKe.Core.Entities;

namespace YiPaiKe.Application
{
    /// <summary>
    /// 老师服务
    /// </summary>
    [ApiDescriptionSettings("BaseDataServices")]
    public class CurriculumService : ServiceBase<Curriculum>
    {
        /// <summary>
        /// 老师服务
        /// </summary>
        /// <param name="repository"></param>
        public CurriculumService(IRepository<Curriculum> repository) : base(repository)
        {
        }
    }
}
