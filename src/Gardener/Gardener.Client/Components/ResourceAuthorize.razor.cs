// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
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
        /// <summary>
        /// 未通过验证时展示
        /// </summary>
        [Parameter]
        public RenderFragment<AuthenticationState> NotAuthorized
        {
            get;
            set;
        }
        /// <summary>
        /// 通过验证时展示
        /// </summary>
        [Parameter]
        public RenderFragment<AuthenticationState> Authorized
        {
            get;
            set;
        }
        /// <summary>
        /// 验证中展示
        /// </summary>
        [Parameter]
        public RenderFragment Authorizing
        {
            get;
            set;
        }
        private string Policy = AuthConstant.ClientUIResourcePolicy;
        /// <summary>
        /// 用户要拥有资源的，资源key
        /// </summary>
        [Parameter]
        public string ResourceKey { get; set; }
    }
}
