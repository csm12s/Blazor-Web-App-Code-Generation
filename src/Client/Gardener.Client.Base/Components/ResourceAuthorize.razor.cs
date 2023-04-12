// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Gardener.Client.Base.Components
{
    /// <summary>
    /// 仅加载资源时验证
    /// </summary>
    public partial class ResourceAuthorize
    {
        [Parameter]
        public RenderFragment? ChildContent
        {
            get;
            set;
        }
        /// <summary>
        /// 未通过验证时展示
        /// </summary>
        [Parameter]
        public RenderFragment? NotAuthorized
        {
            get;
            set;
        }
        /// <summary>
        /// 通过验证时展示
        /// </summary>
        [Parameter]
        public RenderFragment? Authorized
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
        /// <summary>
        /// 用户要拥有资源的，资源key
        /// 多个以逗号隔开 eg:key1,key2
        /// </summary>
        [Parameter]
        [Required]
        public string ResourceKey { get; set; } = null!;

        /// <summary>
        /// 并且关系
        /// 默认 true 是 and关系
        /// 想使用 or 置为 false
        /// </summary>
        [Parameter]
        public bool AndCondition { get; set; } = true;

        /// <summary>
        /// 身份状态管理
        /// </summary>
        [Inject]
        private IAuthenticationStateManager authenticationStateManager { get; set; } = null!;
        /// <summary>
        /// 0 ing
        /// -1 false
        /// 1 true
        /// </summary>
        private short state = 0;


        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrEmpty(ResourceKey)) 
            {
                string[] keys = ResourceKey.Split(',');
                foreach(string key in keys) 
                {
                    var isAuth = await authenticationStateManager.CheckCurrentUserHaveBtnResourceKey(key);
                    if (AndCondition)
                    {
                        if (!isAuth) 
                        {
                            state = -1;
                            break;
                        }
                    }
                    else
                    {
                        if (isAuth) 
                        {
                            state = 1;
                            break;
                        }
                    }
                }
                //正常运行结束
                if (state == 0)
                {
                    state = AndCondition ? (short)1 : (short)-1;
                }
            }
            
        }

    }
}
