// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.EasyJob.Dtos;

namespace Gardener.EasyJob.Services
{
    /// <summary>
    /// 定时任务用户配置服务
    /// </summary>
    public interface IEasyJobUserConfigService
    {
        /// <summary>
        /// 获取我的的配置
        /// </summary>
        /// <returns></returns>
        Task<EasyJobUserConfigDto?> GetMyConfig();

        /// <summary>
        /// 保存我的配置
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        Task<EasyJobUserConfigDto?> SaveMyConfig(EasyJobUserConfigDto config);
    }
}
