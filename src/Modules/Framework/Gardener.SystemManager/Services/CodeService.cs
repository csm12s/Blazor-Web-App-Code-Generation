// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base;
using Gardener.Base.Entity;
using Gardener.EntityFramwork;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Utils;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gardener.SystemManager.Services
{
    /// <summary>
    /// 字典管理
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class CodeService : ServiceBase<Code, CodeDto, int>, ICodeService
    {
        /// <summary>
        /// 字典管理
        /// </summary>
        /// <param name="repository"></param>
        public CodeService(IRepository<Code, MasterDbContextLocator> repository) : base(repository)
        {
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <remarks>
        /// 搜索数据
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<Base.PagedList<CodeDto>> Search(PageRequest request)
        {
            IQueryable<Code> queryable = base.GetSearchQueryable(request);
            var codes=CodeUtil.GetCodesFromCache("mood");
            return await queryable
                .Include(x => x.CodeType)
                .Select(x => x)
                .OrderConditions(request.OrderConditions.ToArray())
                .Select(x => x.Adapt<CodeDto>())
                .ToPageAsync(request.PageIndex, request.PageSize);
        }
    }
}
