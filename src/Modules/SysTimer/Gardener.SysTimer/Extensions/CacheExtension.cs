// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Cache;
using System;

namespace Gardener.SysTimer
{
    public static class CacheExtension
    {
        public static bool Exists<T>(this ICache cache, string key)
        {
            if (cache == null)
                throw new ArgumentNullException();

            var data =  cache.Get<T>(key);

            return data == null?false:true;
        }
    }
}
