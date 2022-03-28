// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign.ProLayout;
using System;
using System.Collections.Generic;

namespace Gardener.Client.Base
{
    public static class ClientMenuCache
    {
       private static Dictionary<string, MenuDataItem> pathMap = new Dictionary<string, MenuDataItem>();

        public static void Add(MenuDataItem menu)
        {
            if (string.IsNullOrEmpty(menu.Path))
            {
                return;
            }
            UriBuilder uriBuilder = new UriBuilder($"http://www.gardener.com{menu.Path}");
            if (!pathMap.ContainsKey(uriBuilder.Path))
            {
                pathMap.Add(uriBuilder.Path, menu);
            }
        }
        public static MenuDataItem Get(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            if (pathMap.ContainsKey(path))
            {
                return pathMap[path];
            }
            return null;
        }
    }
}
