// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Aliyun.OSS.Common;
using Furion.ConfigurableOptions;

namespace Gardener.FileStore.ALiOSS
{
    public class ALiOSSFileStoreSettings : FileStoreSettingsBase, IConfigurableOptions
    {
        /// <summary>
        /// 请填写您的AccessKeyId。
        /// </summary>
        public string AccessKeyId { get; set; } = null!;
        /// <summary>
        /// 请填写您的AccessKeySecret。
        /// </summary>
        public string AccessKeySecret { get; set; } = null!;
        /// <summary>
        /// Endpoint
        /// </summary>
        public string Endpoint { get; set; } = null!;
        /// <summary>
        /// 域名
        /// </summary>
        public string Domain { get; set; } = null!;
        /// <summary>
        /// 从STS服务获取的安全令牌（SecurityToken）
        /// </summary>
        public string? SecurityToken { get; set; }
        /// <summary>
        /// BucketName
        /// </summary>
        public string BucketName { get; set; } = null!;
        /// <summary>
        /// 客户端配置
        /// </summary>
        public ClientConfiguration ClientConfiguration { get; set; }=new ClientConfiguration();
        ///// <summary>
        ///// 代理服务器，例如example.aliyundoc.com
        ///// </summary>
        //public string? ProxyHost { get; set; }
        ///// <summary>
        ///// 代理端口，例如3128 或 8080
        ///// </summary>
        //public int? ProxyPort { get; set; }
        ///// <summary>
        ///// 代码服务账号，可选参数
        ///// </summary>
        //public string? ProxyUserName { get; set; }
        ///// <summary>
        ///// 代码服务密码，可选参数
        ///// </summary>
        //public string? ProxyPassword { get; set; }
        ///// <summary>
        ///// 最大并发连接数。
        ///// </summary>
        //public int ConnectionLimit { get; set; } = 512;
        ///// <summary>
        ///// 请求失败后最大的重试次数
        ///// </summary>
        //public int MaxErrorRetry { get; set; } = 3;
        ///// <summary>
        ///// 设置连接超时时间，单位为毫秒,-1（不超时）
        ///// </summary>
        //public int ConnectionTimeout { get; set; } = -1;
        ///// <summary>
        ///// 上传或下载数据时是否开启MD5校验
        ///// </summary>
        ///// <remarks>
        ///// 上传或下载数据时是否开启MD5校验
        ///// true：开启MD5校验。
        ///// false：关闭MD5校验。
        ///// 重要
        ///// 使用MD5校验时会有一定的性能开销。
        ///// </remarks>
        //public bool EnalbeMD5Check { get; set; }
        ///// <summary>
        ///// Endpoint是否支持CNAME。CNAME用于将自定义域名绑定到存储空间
        ///// </summary>
        //public bool IsCname { get; set; }
        ///// <summary>
        ///// 进度条更新间隔，单位为字节
        ///// </summary>
        //public int ProgressUpdateInterval { get; set; } = 8096;

    }
}
