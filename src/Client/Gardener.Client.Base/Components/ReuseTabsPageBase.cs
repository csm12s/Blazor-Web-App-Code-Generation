// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.ProLayout;
using Gardener.Common;
using Microsoft.AspNetCore.Components;

namespace Gardener.Client.Base
{
    public abstract class ReuseTabsPageBase : ComponentBase, IReuseTabsPage
    {
        public RenderFragment GetPageTitle()
        {
            string title = "";

            RouteAttribute routeAttribute = this.GetType().GetAttribute<RouteAttribute>(true);
            if (routeAttribute != null)
            {
                //根据路由去匹配菜单的名称
                MenuDataItem  menu= ClientMenuCache.Get(routeAttribute.Template);
                title = menu?.Name;
            }

            return title.ToRenderFragment();
        }
    }
}
