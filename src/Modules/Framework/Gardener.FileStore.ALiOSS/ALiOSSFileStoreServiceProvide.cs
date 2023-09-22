// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Furion.FriendlyException;
using Gardener.Enums;
using System.Text.Json;

namespace Gardener.FileStore.ALiOSS
{
    /// <summary>
    /// 
    /// </summary>
    [Injection(Named = nameof(IFileStoreServiceProvider) + nameof(FileStoreServiceType.ALiOSS))]
    public class ALiOSSFileStoreServiceProvide : IFileStoreServiceProvider, ISingleton
    {
        public FileStoreSettingsBase? ConvertSettings(Dictionary<string, object> settings)
        {
            var json = JsonSerializer.Serialize(settings);
            return JsonSerializer.Deserialize<ALiOSSFileStoreSettings>(json);
        }

        public IFileStoreService GetFileStoreService(FileStoreSettingsBase settings)
        {
            return new ALiOSSFileStoreService((ALiOSSFileStoreSettings)settings);
        }

        public IFileStoreService GetFileStoreService(Dictionary<string, object> settings)
        {
            FileStoreSettingsBase? config = ConvertSettings(settings);
            if(config == null)
            {
                throw Oops.Bah(nameof(ALiOSSFileStoreServiceProvide)+ " ConvertSettings result null,settings:" + JsonSerializer.Serialize(settings));
            }
            return GetFileStoreService(config);
        }
    }
}
