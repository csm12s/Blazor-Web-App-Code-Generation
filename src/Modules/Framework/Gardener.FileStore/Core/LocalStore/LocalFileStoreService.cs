// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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
        /// <summary>
        /// 
        /// </summary>
        private readonly IServiceScopeFactory _serviceScopeFactory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="localFileStoreSettings"></param>
        /// <param name="serviceScopeFactory"></param>
        public LocalFileStoreService(IServiceScopeFactory serviceScopeFactory,LocalFileStoreSettings localFileStoreSettings)
        {
            _localFileStoreSettings = localFileStoreSettings;
            _serviceScopeFactory = serviceScopeFactory;
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
            using var scope = _serviceScopeFactory.CreateScope();
            IWebHostEnvironment _hostingEnvironment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            string baseDirectory = Path.Combine(_hostingEnvironment.WebRootPath, GetBaseDirectory());
            return baseDirectory;
        }
        /// <summary>
        /// 获取服务器的访问地址
        /// </summary>
        /// <returns></returns>
        public string GetBaseUrl()
        {
            if (!string.IsNullOrEmpty(_localFileStoreSettings.Domain))
            {
                return _localFileStoreSettings.Domain;
            }
            using var scope = _serviceScopeFactory.CreateScope();
            IHttpContextAccessor _httpContextAccessor= scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                throw new InvalidOperationException("HttpContext is null");
            }
            Uri url = new Uri(context.Request.GetRequestUrlAddress());
            return url.Scheme + "://" + url.Authority + "/" + GetBaseDirectory();
        }
        /// <summary>
        ///  保存文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="partialPath"></param>
        public async Task<string> Save(Stream file, string partialPath)
        {
            string filePath = Path.Combine(GetBaseDirectoryPath(), partialPath);
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                var directory = Path.GetDirectoryName(filePath);
                if (directory == null)
                {
                    throw new ArgumentException($"the {filePath} not find directory", "filePath");
                }
                Directory.CreateDirectory(directory);
            }
            using (FileStream filestream = File.Create(filePath))
            {
                await file.CopyToAsync(filestream);
            }
            return GetBaseUrl() +"/"+ partialPath;
        }

        /// <summary>
        ///  删除文件
        /// </summary>
        /// <param name="partialPath"></param>
        public void Delete(string partialPath)
        {
            string filePath = Path.Combine(GetBaseDirectoryPath(), partialPath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="partialPath"></param>
        /// <returns></returns>
        public Stream Get(string partialPath)
        {
            string filePath = Path.Combine(GetBaseDirectoryPath(), partialPath);
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
            throw new FileNotFoundException("文件未找到,path=" + partialPath);
        }
        /// <summary>
        /// 获取当前存储服务配置
        /// </summary>
        /// <returns></returns>
        public FileStoreSettingsBase GetFileStoreServiceSettings()
        {
            return _localFileStoreSettings;
        }
    }
}
