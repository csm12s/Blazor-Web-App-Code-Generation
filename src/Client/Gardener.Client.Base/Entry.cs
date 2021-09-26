// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------


using System.Collections.Generic;
using System.Reflection;

namespace Gardener.Client.Base
{
    /// <summary>
    /// 入口
    /// </summary>
    public static class Entry
    {
        private static List<Assembly> assemblies = new List<Assembly>() { typeof(Entry).Assembly };

        public static Assembly[] GetAssemblies()
        {
            return assemblies.ToArray();
        }

        public static void Add(Assembly assembly)
        {
            assemblies.Add(assembly);
        }
        public static void Add(params Assembly [] assemblys)
        {
            assemblies.AddRange(assemblys);
        }
        public static void Add(List<Assembly> assemblys)
        {
            assemblies.AddRange(assemblys);
        }
    }
}
