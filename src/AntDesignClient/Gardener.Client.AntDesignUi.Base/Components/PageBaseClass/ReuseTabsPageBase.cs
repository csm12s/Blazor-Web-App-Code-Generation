// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.ProLayout;
using Gardener.Common;
using Microsoft.AspNetCore.Components;

namespace Gardener.Client.AntDesignUi.Base.Components
{
    /// <summary>
    /// tab基类 (可以被当作Modal打开)
    /// </summary>
    public abstract class ReuseTabsPageAndFormBase<Tkey, TOutput>
        : FeedbackComponent<Tkey, TOutput>, IReuseTabsPage
    {
        /// <summary>
        /// 获取页面title
        /// </summary>
        /// <returns></returns>
        public RenderFragment GetPageTitle()
        {
            return GetPageTitleValue().ToRenderFragment();
        }

        /// <summary>
        /// 获取页面title
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 根据页面路由path获取对应菜单名字作为title
        /// </remarks>
        public virtual string GetPageTitleValue()
        {
            string title = "";
            RouteAttribute routeAttribute = this.GetType().GetAttribute<RouteAttribute>(true);
            if (routeAttribute != null)
            {
                //根据路由去匹配菜单的名称
                MenuDataItem menu = ClientMenuCache.Get(routeAttribute.Template);
                title = menu?.Name;
            }
            return title;
        }

        /// <summary>
        /// 强制dom渲染
        /// </summary>
        /// <returns></returns>
        public async Task RefreshPageDom()
        {
            await InvokeAsync(StateHasChanged);
        }
    }

    /// <summary>
    /// tab基类
    /// </summary>
    public abstract class ReuseTabsPageBase : ComponentBase, IReuseTabsPage
    {
        /// <summary>
        /// 获取页面title
        /// </summary>
        /// <returns></returns>
        public RenderFragment GetPageTitle()
        {
            return GetPageTitleValue().ToRenderFragment();
        }

        /// <summary>
        /// 获取页面title
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 根据页面路由path获取对应菜单名字作为title
        /// </remarks>
        public virtual string GetPageTitleValue()
        {
            string title = "";
            RouteAttribute routeAttribute = this.GetType().GetAttribute<RouteAttribute>(true);
            if (routeAttribute != null)
            {
                //根据路由去匹配菜单的名称
                MenuDataItem menu = ClientMenuCache.Get(routeAttribute.Template);
                title = menu?.Name;
            }
            return title;
        }

        /// <summary>
        /// 强制dom渲染
        /// </summary>
        /// <returns></returns>
        public async Task RefreshPageDom()
        {
            await InvokeAsync(StateHasChanged);
        }
    }
}
