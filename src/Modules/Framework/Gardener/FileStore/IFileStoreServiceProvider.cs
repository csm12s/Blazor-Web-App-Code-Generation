// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Text.Json;

namespace Gardener.FileStore
{
    /// <summary>
    /// 文件存储服务提供者
    /// </summary>
    public interface IFileStoreServiceProvider
    {
        /// <summary>
        /// 获取文件存储服务
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        IFileStoreService GetFileStoreService(FileStoreSettingsBase settings);
        /// <summary>
        /// 获取文件存储服务
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        IFileStoreService GetFileStoreService(Dictionary<string, object> settings);
        /// <summary>
        /// 转换配置
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        FileStoreSettingsBase? ConvertSettings(Dictionary<string, object> settings);
    }
}
