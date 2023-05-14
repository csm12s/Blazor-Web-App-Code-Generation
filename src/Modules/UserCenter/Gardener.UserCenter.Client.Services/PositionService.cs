// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Client.Base;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;

namespace Gardener.UserCenter.Client.Services
{
    [ScopedService]
    public class PositionService : ClientServiceBase<PositionDto, int>, IPositionService
    {
        public PositionService(IApiCaller apiCaller) : base(apiCaller, "position")
        {
        }
        
        public async Task<PagedList<PositionDto>> Search(string name, int pageIndex = 1, int pageSize = 10)
        {
            IDictionary<string, object?> pramas = new Dictionary<string, object?>()
            {
                {"name",name }
            };
            return await apiCaller.GetAsync<PagedList<PositionDto>>($"{controller}/search/{pageIndex}/{pageSize}", pramas);
        }
    }
}
