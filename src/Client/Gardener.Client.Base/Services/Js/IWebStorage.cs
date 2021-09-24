// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    public interface IWebStorage
    {
        Task<T> GetAsync<T>(string key);
        Task RemoveAsync(string key);
        Task SetAsync(string key, object value);
    }
}
