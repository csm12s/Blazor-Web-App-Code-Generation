using Gardener.Client.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Gardener.Client.BootstrapUi.Base
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class App
    {
        [Inject]
        [NotNull]
        private ClientModuleContext moduleContext { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Inject]
        [NotNull]
        private IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && OperatingSystem.IsBrowser())
            {
                await JSRuntime.InvokeVoidAsync("$.loading");
            }
        }
    }
}