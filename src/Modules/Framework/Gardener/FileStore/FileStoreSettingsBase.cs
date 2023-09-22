// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace Gardener.FileStore
{
    /// <summary>
    /// 基础配置
    /// </summary>
    public class FileStoreSettingsBase
    {
        /// <summary>
        /// 文件存储服务类型
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FileStoreServiceType FileStoreServiceType { get; set; } = FileStoreServiceType.Local;

    }
}
