// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Common
{
    public static class LinqExtension
    {
        /// <summary>
        /// 使用异步遍历处理数据
        /// </summary>
        /// <typeparam name="T">需要遍历的基类</typeparam>
        /// <param name="list">集合</param>
        /// <param name="func">Lambda表达式</param>
        /// <returns> </returns>
        public static Task ForEachAsync<T>(this IEnumerable<T> list, Func<T, Task> func)
        {
            List<Task> tasks = new List<Task>();
            foreach (T value in list)
            {
                tasks.Add(func(value));
            }

            return Task.WhenAll(tasks);
        }
    }
}
