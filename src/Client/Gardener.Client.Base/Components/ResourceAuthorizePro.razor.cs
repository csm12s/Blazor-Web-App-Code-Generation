﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Gardener.Client.Base.Components
{
    /// <summary>
    /// 页面有任何渲染变化，都会重新触发验证
    /// </summary>
    public partial class ResourceAuthorizePro
    {
        [Parameter]
        public RenderFragment<AuthenticationState>? ChildContent
        {
            get;
            set;
        }
        /// <summary>
        /// 未通过验证时展示
        /// </summary>
        [Parameter]
        public RenderFragment<AuthenticationState>? NotAuthorized
        {
            get;
            set;
        }
        /// <summary>
        /// 通过验证时展示
        /// </summary>
        [Parameter]
        public RenderFragment<AuthenticationState>? Authorized
        {
            get;
            set;
        }
        /// <summary>
        /// 验证中展示
        /// </summary>
        [Parameter]
        public RenderFragment? Authorizing
        {
            get;
            set;
        }
        private string Policy = AuthConstant.ClientUIResourcePolicy;
        /// <summary>
        /// 用户要拥有资源的，资源key
        /// 多个以逗号隔开 eg:key1,key2
        /// </summary>
        [Parameter]
        public string? ResourceKey { get; set; }
        /// <summary>
        /// 并且关系
        /// 默认 true 是 and关系
        /// 想使用 or 置为 false
        /// </summary>
        [Parameter]
        public bool AndCondition { get; set; } = true;
        /// <summary>
        /// 资源
        /// </summary>
        public ClientResource Resource
        {
            get
            {
                return new ClientResource()
                {
                    Keys = ResourceKey?.Split(",") ?? new string[0],
                    AndCondition = AndCondition
                };
            }
        }
    }
}
