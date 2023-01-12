// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Gardener.Client.Base.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IClientCultureService
    {
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        Task<bool> SetCulture(string culture);
        /// <summary>
        /// 获取支持的列表
        /// </summary>
        /// <returns></returns>
        string[] GetSupportedCultures();
    }
}
