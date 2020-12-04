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
