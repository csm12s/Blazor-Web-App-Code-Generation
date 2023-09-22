// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.FileStore
{
    /// <summary>
    /// 文件存储系统工厂
    /// </summary>
    public interface IFileStoreServiceFactory
    {
        /// <summary>
        /// 获取默认服务
        /// </summary>
        /// <returns></returns>
        IFileStoreService GetDefaultFileStoreService();
        /// <summary>
        /// 获取存储服务
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        IFileStoreService GetFileStoreService(string serviceId);
        /// <summary>
        /// 创建存储服务
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        IFileStoreService CreateFileStoreService(FileStoreSettingsBase settings);
    }
}