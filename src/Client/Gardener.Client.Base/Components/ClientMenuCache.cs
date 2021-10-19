// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign.ProLayout;
using System.Collections.Generic;

namespace Gardener.Client.Base
{
    public static class ClientMenuCache
    {
       private static Dictionary<string, MenuDataItem> pathMap = new Dictionary<string, MenuDataItem>();

        public static void Add(MenuDataItem menu)
        {
            if (!pathMap.ContainsKey(menu.Path))
            {
                pathMap.Add(menu.Path, menu);
            }
        }
        public static MenuDataItem Get(string path)
        {
            if (pathMap.ContainsKey(path))
            {
                return pathMap[path];
            }
            return null;
        }
    }
}
