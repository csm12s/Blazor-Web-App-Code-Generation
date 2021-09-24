// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.EntityFramwork.Dto;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Services
{
    [ScopedService]
    public class PositionService : ApplicationServiceBase<PositionDto, int>, IPositionService
    {
        private readonly static string controller = "position";

        private IApiCaller apiCaller;
        public PositionService(IApiCaller apiCaller) : base(apiCaller, controller)
        {
            this.apiCaller = apiCaller;
        }
        
        public async Task<PagedList<PositionDto>> Search(string name, int pageIndex = 1, int pageSize = 10)
        {
            IDictionary<string, object> pramas = new Dictionary<string, object>()
            {
                {"name",name }
            };
            return await apiCaller.GetAsync<PagedList<PositionDto>>($"{controller}/search/{pageIndex}/{pageSize}", pramas);
        }
    }
}
