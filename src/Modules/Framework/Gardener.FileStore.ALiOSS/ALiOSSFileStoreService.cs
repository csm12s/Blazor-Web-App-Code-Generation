using Aliyun.OSS;

namespace Gardener.FileStore.ALiOSS
{
    /// <summary>
    /// 阿里 oss文件存储
    /// </summary>
    public class ALiOSSFileStoreService : IFileStoreService
    {

        private readonly ALiOSSFileStoreSettings _storeSettings;
        private readonly OssClient _ossClient;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="storeSettings"></param>
        public ALiOSSFileStoreService(ALiOSSFileStoreSettings storeSettings)
        {
            _storeSettings = storeSettings;

            if (string.IsNullOrEmpty(storeSettings.SecurityToken))
            {
                _ossClient = new OssClient(storeSettings.Endpoint, storeSettings.AccessKeyId, storeSettings.AccessKeySecret, storeSettings.ClientConfiguration);
            }
            else 
            {
                _ossClient = new OssClient(storeSettings.Endpoint, storeSettings.AccessKeyId, storeSettings.AccessKeySecret, storeSettings.SecurityToken,storeSettings.ClientConfiguration);
            }
        }

        public void Delete(string partialPath)
        {
            throw new NotImplementedException();
        }

        public Stream Get(string partialPath)
        {
            throw new NotImplementedException();
        }

        public Task<string> Save(Stream file, string partialPath)
        {
            PutObjectResult result= _ossClient.PutObject(_storeSettings.BucketName, partialPath, file);
            var url = _storeSettings.Domain +"/"+ partialPath;
            return Task.FromResult(url);
        }
    }
}
