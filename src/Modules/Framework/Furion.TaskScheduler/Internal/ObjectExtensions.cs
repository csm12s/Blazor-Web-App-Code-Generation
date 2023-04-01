// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using System.Reflection;
using System.Runtime.CompilerServices;

namespace Furion.TaskScheduler
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// 判断方法是否是异步
        /// </summary>
        /// <param name="method">方法</param>
        /// <returns></returns>
        internal static bool IsAsync(this MethodInfo method)
        {
            return method.GetCustomAttribute<AsyncMethodBuilderAttribute>() != null
                || method.ReturnType.ToString().StartsWith(typeof(Task).FullName);
        }
    }
}
