// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Gardener.Client.Base.Services
{
    /// <summary>
    /// 客户端本地化服务
    /// </summary>
    public interface IClientCultureService
    {
        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        Task<bool> SetCulture(string culture);
        /// <summary>
        /// 获取支持的本地语言
        /// </summary>
        /// <returns></returns>
        string[] GetSupportedCultures();
    }
}
