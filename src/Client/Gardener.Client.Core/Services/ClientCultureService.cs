﻿// -----------------------------------------------------------------------------
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
                await jsTool.SessionStorage.SetAsync(ClientConstant.BlazorCultureKey, culture);
                //存储到cookie 调用接口时，api端生效
                await jsTool.Cookie.Set(".AspNetCore.Culture", $"c={culture}|uic={culture}",path:"/");
                return true;
            }
            return false;
        }
    }
}
