// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Furion.FriendlyException;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Gardener.FileStore.Core.LocalStore
{
    /// <summary>
    /// 本地存储服务提供器
    /// </summary>
    [Injection(Named = nameof(IFileStoreServiceProvider) + nameof(FileStoreServiceType.Local))]
    public class LocalFileStoreServiceProvider : IFileStoreServiceProvider, ISingleton
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceScopeFactory"></param>
        public LocalFileStoreServiceProvider(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public FileStoreSettingsBase? ConvertSettings(Dictionary<string, object> settings)
        {
            var json = JsonSerializer.Serialize(settings);
            return JsonSerializer.Deserialize<LocalFileStoreSettings>(json);
        }
        /// <summary>
        /// 获取存储服务
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IFileStoreService GetFileStoreService(FileStoreSettingsBase settings)
        {
            return new LocalFileStoreService(_serviceScopeFactory, (LocalFileStoreSettings)settings);
        }
        /// <summary>
        /// 获取存储服务
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public IFileStoreService GetFileStoreService(Dictionary<string, object> settings)
        {
            FileStoreSettingsBase? config = ConvertSettings(settings);
            if (config == null)
            {
                throw Oops.Bah(nameof(LocalFileStoreService) + " ConvertSettings result null,settings:" + JsonSerializer.Serialize(settings));
            }
            return GetFileStoreService(config);
        }
    }
}
