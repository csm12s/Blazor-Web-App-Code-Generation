// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;

namespace Gardener.Core.FileStore
{
    /// <summary>
    /// 
    /// </summary>
    public class LocalFileStoreSettings : IConfigurableOptions
    {
        /// <summary>
        /// 存储的基础目录
        /// 为空时，默认是wwwroot/upload 路径
        /// </summary>
        public string BaseDirectory { get; set; }
        /// <summary>
        /// 文件访问路径
        /// </summary>
        public string BaseUrl { get; set; }
    }
}
