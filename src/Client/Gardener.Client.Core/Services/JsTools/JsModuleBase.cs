// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Gardener.Client.Core.Services.JsTools
{
    /// <summary>
    /// js模块基类
    /// </summary>
    public abstract class JsModuleBase
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = null!;
        private IJSObjectReference? jSObjectReference;
        public readonly IJSRuntime js;

        /// <summary>
        /// js模块基类
        /// </summary>
        /// <param name="js"></param>
        /// <param name="moduleJsPath">js模块路径 (eg:./_content/Gardener.Client.Core/js-tool.js)</param>
        public JsModuleBase(IJSRuntime js, string moduleJsPath)
        {
            this.js = js;
            moduleTask = new(() => js.InvokeAsync<IJSObjectReference>("import", $"{moduleJsPath}?_={DateTime.Now.ToString("yyyyMMddHHmmss")}").AsTask());
        }

        /// <summary>
        /// js模块基类
        /// </summary>
        /// <param name="js"></param>
        /// <param name="identifier"></param>
        /// <param name="args"></param>
        public JsModuleBase(IJSRuntime js, string identifier, params object?[]? args)
        {
            this.js = js;
            moduleTask = new(() => js.InvokeAsync<IJSObjectReference>(identifier, args).AsTask());
        }

        /// <summary>
        /// 获取模块
        /// </summary>
        /// <returns></returns>
        public async Task<IJSObjectReference> GetModule()
        {
            if (jSObjectReference == null)
            {
                jSObjectReference = await moduleTask.Value;
            }
            return jSObjectReference;
        }

    }
}
