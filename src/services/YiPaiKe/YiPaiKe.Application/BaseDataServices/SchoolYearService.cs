﻿// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using YiPaiKe.Core.Entities;

namespace YiPaiKe.Application
{
    /// <summary>
    /// 学年服务
    /// </summary>
    [ApiDescriptionSettings("BaseDataServices")]
    public class SchoolYearService : ServiceBase<SchoolYear>
    {
        /// <summary>
        /// 学年服务
        /// </summary>
        /// <param name="repository"></param>
        public SchoolYearService(IRepository<SchoolYear> repository) : base(repository)
        {
        }
    }
}
