﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.ProLayout;
using Gardener.Client.Base.Components;
using Gardener.Common;
using Microsoft.AspNetCore.Components;

namespace Gardener.Client.AntDesignUi.Base.Components
{
    /// <summary>
    /// tab基类 (可以被当作OperationDialog打开)
    /// </summary>
    /// <typeparam name="TSelfOperationDialogInput">自身作为OperationDialog接收的参数</typeparam>
    /// <typeparam name="TSelfOperationDialogOutput">自身作为OperationDialog返回的参数</typeparam>
    public abstract class ReuseTabsPageAndFormBase<TSelfOperationDialogInput, TSelfOperationDialogOutput>
        : FeedbackComponent<TSelfOperationDialogInput, TSelfOperationDialogOutput>, IReuseTabsPage
    {
        /// <summary>
        /// 弹框区域的加载中标识
        /// </summary>
        protected ClientLoading _dialogLoading = new ClientLoading();

        /// <summary>
        /// Page start loading
        /// </summary>
        /// <param name="forceRender">是否强制渲染</param>
        /// <returns></returns>
        protected Task StartLoading(bool forceRender = false)
        {
            var run = _dialogLoading.Start();
            if (run && forceRender)
            {
               return InvokeAsync(StateHasChanged);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Page stop loading
        /// </summary>
        /// <param name="forceRender">是否强制渲染</param>
        /// <returns></returns>
        protected Task StopLoading(bool forceRender = false)
        {
            var stop = _dialogLoading.Stop();
            if (stop && forceRender)
            {
               return InvokeAsync(StateHasChanged);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取页面title
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 只有当被当做页面打开时有效
        /// </remarks>
        public RenderFragment GetPageTitle()
        {
            return GetPageTitleValue().ToRenderFragment();
        }

        /// <summary>
        /// 获取页面title
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 根据页面路由path获取对应菜单名字作为title，只有当被当做页面打开时有效
        /// </remarks>
        public virtual string GetPageTitleValue()
        {
            string title = "";
            RouteAttribute? routeAttribute = this.GetType().GetAttribute<RouteAttribute>(true);
            if (routeAttribute != null)
            {
                //根据路由去匹配菜单的名称
                MenuDataItem? menu = ClientMenuCache.Get(routeAttribute.Template);
                title = menu?.Name ?? "";
            }
            return title;
        }

        /// <summary>
        /// 强制dom渲染
        /// </summary>
        /// <returns></returns>
        public Task RefreshPageDom()
        {
            return InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 关闭自己
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public Task CloseAsync(TSelfOperationDialogOutput? output=default)
        {
            return base.FeedbackRef.CloseAsync(output);
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
            RouteAttribute? routeAttribute = this.GetType().GetAttribute<RouteAttribute>(true);
            if (routeAttribute != null)
            {
                //根据路由去匹配菜单的名称
                MenuDataItem? menu = ClientMenuCache.Get(routeAttribute.Template);
                title = menu?.Name ?? "";
            }
            return title;
        }

        /// <summary>
        /// 强制dom渲染
        /// </summary>
        /// <returns></returns>
        public Task RefreshPageDom()
        {
            return InvokeAsync(StateHasChanged);
        }
    }
}
