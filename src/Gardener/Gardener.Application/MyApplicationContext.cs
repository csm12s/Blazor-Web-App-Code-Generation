// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Core;
using Gardener.Core.Entites;
using System.Collections.Generic;

namespace Gardener.Application
{
    /// <summary>
    /// 应用静态资源
    /// </summary>
    public static class MyApplicationContext
    {
        /// <summary>
        /// Api资源
        /// </summary>
        private static List<Resource> apiResources = new List<Resource>();
        /// <summary>
        /// 添加Api资源
        /// </summary>
        /// <param name="resource"></param>
        public static void AddApiResource(Resource resource)
        {
            apiResources.Add(resource);
        }
        /// <summary>
        /// 获取所有Api资源
        /// </summary>
        /// <returns></returns>
        public static List<Resource> GetApiResources()
        {

            return apiResources;
        }

    }
}
