// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Gardener.Client.Components
{
    public partial  class ResourceAuthorize
    {
        [Parameter]
        public RenderFragment<AuthenticationState> ChildContent
        {
            get;
            set;
        }
        [Parameter]
        public RenderFragment<AuthenticationState> NotAuthorized
        {
            get;
            set;
        }
        [Parameter]
        public RenderFragment<AuthenticationState> Authorized
        {
            get;
            set;
        }
        [Parameter]
        public RenderFragment Authorizing
        {
            get;
            set;
        }
        private string Policy = "permission";
        /// <summary>
        /// 资源key
        /// </summary>
        [Parameter]
        public string ResourceKey { get; set; }
    }
}
