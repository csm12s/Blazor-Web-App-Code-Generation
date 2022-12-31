// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Gardener.FileStore.Core.LocalStore
{
    /// <summary>
    /// 本地文件存储服务
    /// </summary>
    public class LocalFileStoreService : IFileStoreService
    {

        private LocalFileStoreSettings _localFileStoreSettings;
        private readonly IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 请求上下文访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="hostingEnvironment"></param>
        /// <param name="httpContextAccessor"></param>
        public LocalFileStoreService(IOptions<LocalFileStoreSettings> options, IWebHostEnvironment hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this._localFileStoreSettings = options.Value;
            this._hostingEnvironment = hostingEnvironment;
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 获取文件存储目录
        /// </summary>
        /// <returns></returns>
        private string GetBaseDirectory()
        {
            if (_localFileStoreSettings != null && !string.IsNullOrEmpty(_localFileStoreSettings.BaseDirectory))
            {
                return _localFileStoreSettings.BaseDirectory;
            }
            return "upload";
        }
        /// <summary>
        /// 获取文件存储目录地址
        /// </summary>
        /// <returns></returns>
        public string GetBaseDirectoryPath()
        {
            string baseDirectory = Path.Combine(_hostingEnvironment.WebRootPath, GetBaseDirectory());
            return baseDirectory;
        }
        /// <summary>
        /// 获取服务器的访问地址
        /// </summary>
        /// <returns></returns>
        public string GetBaseUrl() 
        {
            Uri url = new Uri(_httpContextAccessor.HttpContext.Request.GetRequestUrlAddress());
            return url.Scheme+"://"+url.Authority +"/"+ GetBaseDirectory()+"/";
        }
        /// <summary>
        ///  保存文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="path"></param>
        public async Task<string> Save(Stream file, string path)
        {
            string filePath = Path.Combine(GetBaseDirectoryPath(), path);
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            using (FileStream filestream = File.Create(filePath))
            {
                await file.CopyToAsync(filestream);
            }
            return GetBaseUrl() + path;
        }
        /// <summary>
        ///  删除文件
        /// </summary>
        /// <param name="path"></param>
        public void Delete(string path)
        {
            string filePath = Path.Combine(GetBaseDirectoryPath(), path);
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
            string filePath = Path.Combine(GetBaseDirectoryPath(), path);
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
            throw new FileNotFoundException("文件未找到,path=" + path);
        }
    }
}
