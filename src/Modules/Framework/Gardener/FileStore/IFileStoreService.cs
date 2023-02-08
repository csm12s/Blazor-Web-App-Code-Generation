// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.IO;
using System.Threading.Tasks;

namespace Gardener.FileStore
{
    /// <summary>
    /// 文件存储服务
    /// </summary>
    public interface IFileStoreService
    {
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="partialPath"></param>
        void Delete(string partialPath);
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="partialPath"></param>
        /// <returns></returns>
        Stream Get(string partialPath);
        /// <summary>
        /// 文件上传基础目录
        /// </summary>
        /// <returns></returns>
        string GetBaseDirectoryPath();
        /// <summary>
        /// 文件上传基础Url
        /// </summary>
        /// <returns></returns>
        string GetBaseUrl();

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="partialPath"></param>
        /// <returns>文件访问路径</returns>
        Task<string> Save(Stream file, string partialPath);

        /// <summary>
        /// Save file to server
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileSavePath"></param>
        /// <returns></returns>
        Task<bool> SaveToLocal(Stream stream, string fileSavePath);
    }
}
