// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    public interface IJsTool
    {
        IDocument Document { get; init; }
        IWebStorage LocalStorage { get; init; }
        IWebStorage SessionStorage { get; init; }
        ICookie Cookie { get; init; }

        Task<IJSObjectReference> GetJsToolModule();

    }
}