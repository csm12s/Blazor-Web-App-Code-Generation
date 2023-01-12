// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Base.Constants;
using Gardener.Client.Base.Services;
using System.Globalization;
using System.Threading.Tasks;

namespace Gardener.Client.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientCultureService : IClientCultureService
    {
        private readonly IJsTool jsTool;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsTool"></param>
        public ClientCultureService(IJsTool jsTool)
        {
            this.jsTool = jsTool;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetSupportedCultures()
        {
            return ClientConstant.SupportedCultures;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public async Task<bool> SetCulture(string culture)
        {
            if (CultureInfo.CurrentCulture.Name != culture)
            {
                await jsTool.SessionStorage.SetAsync(ClientConstant.BlazorCultureKey, culture);
                await jsTool.Cookie.Set(".AspNetCore.Culture", $"c={culture}|uic={culture}",path:"/");
                return true;
            }
            return false;
        }
    }
}
