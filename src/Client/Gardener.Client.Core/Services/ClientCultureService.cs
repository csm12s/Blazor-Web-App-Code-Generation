// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Base.Constants;
using Gardener.Client.Base.Services;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gardener.Client.Core.Services
{
    /// <summary>
    /// 客户端本地化服务
    /// </summary>
    public class ClientCultureService : IClientCultureService
    {
        private readonly IJsTool jsTool;
        /// <summary>
        /// 客户端本地化服务
        /// </summary>
        /// <param name="jsTool"></param>
        public ClientCultureService(IJsTool jsTool)
        {
            this.jsTool = jsTool;
        }
        /// <summary>
        /// 获取支持的本地语言
        /// </summary>
        /// <returns></returns>
        public string[] GetSupportedCultures()
        {
            return ClientConstant.SupportedCultures;
        }
        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public async Task<bool> SetCulture(string culture)
        {
            if (CultureInfo.CurrentCulture.Name != culture)
            {
                //存储到session，在刷新页面后生效
                var task1= jsTool.LocalStorage.SetAsync(ClientConstant.BlazorCultureKey, culture);
                //存储到cookie 调用接口时，api端生效
                var task2= jsTool.Cookie.Set(".AspNetCore.Culture", $"c={culture}|uic={culture}",path:"/");

                await task1;
                await task2;
                var cultureInfo = new CultureInfo(culture);
                CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
                CultureInfo.CurrentCulture = cultureInfo;
                CultureInfo.CurrentUICulture = cultureInfo;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="cultureStorageKey"></param>
        /// <param name="defaultCulture"></param>
        /// <returns></returns>
        public async Task Init(string cultureStorageKey, string defaultCulture)
        {
            var result = await jsTool.LocalStorage.GetAsync<string>(cultureStorageKey);
            await SetCulture(result ?? defaultCulture);
        }
    }
}
