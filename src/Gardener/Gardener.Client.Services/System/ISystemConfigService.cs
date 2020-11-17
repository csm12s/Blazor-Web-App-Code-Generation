// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Client.Models;

namespace Gardener.Client.Services
{
    public interface ISystemConfigService
    {
        string GetCopyright();

        string GetSystemName();

        SystemConfig GetSystemConfig();
    }
}