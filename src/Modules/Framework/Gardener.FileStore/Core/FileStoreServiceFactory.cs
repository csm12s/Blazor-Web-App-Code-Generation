// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.ClayObject;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Gardener.Base.Resources;
using Gardener.Enums;
using Gardener.LocalizationLocalizer;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gardener.FileStore.Core
{
    /// <summary>
    /// 文件存储系统工厂
    /// </summary>
    public class FileStoreServiceFactory : IFileStoreServiceFactory
    {
        Dictionary<string, IFileStoreService> services = new Dictionary<string, IFileStoreService>();
        private readonly INamedServiceProvider<IFileStoreServiceProvider> _namedServiceProvider;
        private readonly IOptions<FileStoreSettings> _options;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="namedServiceProvider"></param>
        /// <param name="options"></param>
        public FileStoreServiceFactory(INamedServiceProvider<IFileStoreServiceProvider> namedServiceProvider, IOptions<FileStoreSettings> options)
        {
            _namedServiceProvider = namedServiceProvider;
            _options = options;
        }

        /// <summary>
        /// 获取存储服务
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public IFileStoreService GetFileStoreService(string serviceId)
        {
            if (services.ContainsKey(serviceId)) return services[serviceId];

            FileStoreSettings settings = _options.Value;
            var configDic = settings.Services.FirstOrDefault(x => x.ContainsKey(nameof(FileStoreSettingsBase.FileStoreServiceId)) && serviceId.Equals(x[nameof(FileStoreSettingsBase.FileStoreServiceId)]));
            if (configDic == null)
            {
                throw Oops.Bah(ExceptionCode.File_Store_Service_Config_Not_Find, serviceId);
            }
            object? fileStoreServiceType = null;
            if (!configDic.TryGetValue(nameof(FileStoreSettingsBase.FileStoreServiceType), out fileStoreServiceType))
            {
                throw Oops.Oh(
                    Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.RequiredValidationError)),
                    Lo.GetValue<SharedLocalResource>(nameof(SharedLocalResource.FileStoreServiceType))
                );
            }
            FileStoreServiceType type = FileStoreServiceType.Local;
            if (!Enum.TryParse<FileStoreServiceType>(fileStoreServiceType.ToString(), true, out type))
            {
                throw Oops.Bah(ExceptionCode.File_Store_Service_Type_Unsupported, fileStoreServiceType);
            }
            IFileStoreServiceProvider fileStoreServiceProvider = _namedServiceProvider.GetRequiredService(nameof(IFileStoreServiceProvider) + fileStoreServiceType.ToString());
            IFileStoreService service = fileStoreServiceProvider.GetFileStoreService(configDic);
            services.Add(serviceId, service);
            return service;
        }

        /// <summary>
        /// 获取默认服务
        /// </summary>
        /// <returns></returns>
        public IFileStoreService GetDefaultFileStoreService()
        {
            FileStoreSettings settings = _options.Value;
            if (string.IsNullOrEmpty(settings.DefaultFileStoreService))
            {
                throw Oops.Oh(
                    Lo.GetValue<ValidateErrorMessagesResource>(nameof(ValidateErrorMessagesResource.RequiredValidationError)),
                    Lo.GetValue<SharedLocalResource>(nameof(SharedLocalResource.DefaultFileStoreService))
                );
            }
            return GetFileStoreService(settings.DefaultFileStoreService);
        }
        /// <summary>
        /// 创建存储服务
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IFileStoreService CreateFileStoreService(FileStoreSettingsBase settings)
        {
            IFileStoreServiceProvider fileStoreServiceProvider = _namedServiceProvider.GetService(nameof(IFileStoreServiceProvider) + settings.FileStoreServiceType.ToString());
            IFileStoreService service = fileStoreServiceProvider.GetFileStoreService(settings);
            return service;
        }
    }
}
