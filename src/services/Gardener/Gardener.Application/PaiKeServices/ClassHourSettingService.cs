// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using Gardener.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Gardener.Application
{
    /// <summary>
    /// 课时设置服务
    /// </summary>
    [ApiDescriptionSettings("PaiKeServices")]
    public class ClassHourSettingService : ServiceBase<ClassHourSetting>
    {
        /// <summary>
        /// 课时设置服务
        /// </summary>
        /// <param name="repository"></param>
        public ClassHourSettingService(IRepository<ClassHourSetting> repository) : base(repository)
        {
        }
    }
}
