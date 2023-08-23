// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Furion.Localization;
using Gardener.Base;
using Gardener.Base.Entity;
using Gardener.Base.Resources;
using Gardener.EntityFramwork;
using Gardener.LocalizationLocalizer;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Resources;
using Gardener.SystemManager.Utils;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Gardener.SystemManager.Services
{
    /// <summary>
    /// 字典管理
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class CodeService : ServiceBase<Code, CodeDto, int>, ICodeService
    {
        private readonly ILocalizationLocalizer<CodeLocalResource> localizationLocalizer;
        /// <summary>
        /// 字典管理
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="localizationLocalizer"></param>
        public CodeService(IRepository<Code, MasterDbContextLocator> repository, ILocalizationLocalizer<CodeLocalResource> localizationLocalizer) : base(repository)
        {
            this.localizationLocalizer = localizationLocalizer;
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
            Base.PagedList<CodeDto> result= await queryable
                .Include(x => x.CodeType)
                .Select(x => x)
                .OrderConditions(request.OrderConditions.ToArray())
                .Select(x => x.Adapt<CodeDto>())
                .ToPageAsync(request.PageIndex, request.PageSize);

            if (result.Items.Any())
            {
                foreach (var item in result.Items)
                {
                    item.LocalCodeName = localizationLocalizer[item.CodeName];
                }
            }
            return result;
        }
    }
}
