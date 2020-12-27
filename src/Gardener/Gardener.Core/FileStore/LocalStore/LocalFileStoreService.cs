// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Gardener.Core.FileStore
{
    /// <summary>
    /// 本地文件存储服务
    /// </summary>
    public class LocalFileStoreService : IFileStoreService, ISingleton
    {

        private LocalFileStoreSettings _localFileStoreSettings;
        private readonly IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public LocalFileStoreService(IOptions<LocalFileStoreSettings> options, IWebHostEnvironment hostingEnvironment)
        {
            _localFileStoreSettings = options.Value;
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 获取文件存储目录
        /// </summary>
        /// <returns></returns>
        private string GetBaseDirectory()
        {
            if (_localFileStoreSettings != null && !string.IsNullOrEmpty(_localFileStoreSettings.BaseDirectory)) return _localFileStoreSettings.BaseDirectory;
            string baseDirectory =Path.Combine(_hostingEnvironment.WebRootPath, "upload");
            return baseDirectory;
        }

        /// <summary>
        ///  保存文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="path"></param>
        public async Task<string> Save(Stream file, string path)
        {
            string filePath = Path.Combine(GetBaseDirectory(), path);
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            using (FileStream filestream = File.Create(filePath))
            {
                await file.CopyToAsync(filestream);
            }

            return _localFileStoreSettings.BaseUrl + path;
        }
        /// <summary>
        ///  删除文件
        /// </summary>
        /// <param name="path"></param>
        public void Delete(string path)
        {
            string filePath = Path.Combine(GetBaseDirectory(), path);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Stream Get(string path)
        {
            string filePath = Path.Combine(GetBaseDirectory(), path);
            if (File.Exists(filePath))
            {
                // 打开文件 
                FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                // 读取文件的 byte[] 
                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);
                fileStream.Close();
                // 把 byte[] 转换成 Stream 
                Stream stream = new MemoryStream(bytes);
                return stream;
            }
            throw new FileNotFoundException("文件为找到,path=" + path);
        }
    }
}
