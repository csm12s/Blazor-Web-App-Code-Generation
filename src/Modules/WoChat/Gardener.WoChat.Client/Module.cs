// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Base.Model;
using Gardener.Client.Base.Services;
using Gardener.WoChat.Client.Components;

namespace Gardener.WoChat.Client
{
    /// <summary>
    /// 
    /// </summary>
    [SingletonService]
    internal class Module : IModule
    {
        public string Name => "WoChat";

        public IEnumerable<ModuleComponent> GetAutoRegisterComponents()
        {
            return [new ModuleComponent(typeof(WoChatBtn), "body::after")];
        }

        public Task Load()
        {
            return Task.CompletedTask;
        }
    }
}
