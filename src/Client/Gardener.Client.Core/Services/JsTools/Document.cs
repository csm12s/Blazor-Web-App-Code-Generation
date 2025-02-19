﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Gardener.Client.Core.Services.JsTools
{
    /// <summary>
    /// Document
    /// </summary>
    [ScopedService]
    public class Document : JsModuleBase, IDocument
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="js"></param>
        public Document(IJSRuntime js) : base(js, "./_content/Gardener.Client.Core/js-tool-document.js")
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task SetTitle(string title)
        {
            var module = await GetModule();
            await module.InvokeVoidAsync("setDocumentTitle", title);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task DownloadFile(string url)
        {
            var module = await GetModule();
            await module.InvokeVoidAsync("downloadFile", url);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="boxId"></param>
        /// <returns></returns>
        public async Task ScrollBarToBottom(string boxId)
        {
            var module = await GetModule();
            await module.InvokeVoidAsync("scrollBarToBottom", boxId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="boxId"></param>
        /// <returns></returns>
        public async Task ScrollBarToTop(string boxId)
        {
            var module = await GetModule();
            await module.InvokeVoidAsync("scrollBarToTop", boxId);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="boxId"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public async Task ScrollBar(string boxId, int height)
        {
            var module = await GetModule();
            await module.InvokeVoidAsync("scrollBar", boxId, height);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task copyTextToClipboard(string text)
        {
            var module = await GetModule();
            await module.InvokeVoidAsync("copyTextToClipboard", text);
        }
    }
}
