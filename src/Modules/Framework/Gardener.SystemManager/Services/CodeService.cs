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
        private readonly ILocalizationLocalizer localizationLocalizer1;
        private readonly ILocalizationLocalizer<SystemManagerResource> localizationLocalizer2;
        private readonly IStringLocalizerFactory stringLocalizerFactory;
        /// <summary>
        /// 字典管理
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="localizationLocalizer"></param>
        public CodeService(IRepository<Code, MasterDbContextLocator> repository, ILocalizationLocalizer<CodeLocalResource> localizationLocalizer, ILocalizationLocalizer localizationLocalizer1, ILocalizationLocalizer<SystemManagerResource> localizationLocalizer2, IStringLocalizerFactory stringLocalizerFactory) : base(repository)
        {
            this.localizationLocalizer = localizationLocalizer;
            this.localizationLocalizer1 = localizationLocalizer1;
            this.localizationLocalizer2 = localizationLocalizer2;
            this.stringLocalizerFactory = stringLocalizerFactory;
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

            var localizationSettings = App.GetOptions<LocalizationSettingsOptions>();
            IStringLocalizer s1= stringLocalizerFactory.Create(localizationSettings.LanguageFilePrefix, localizationSettings.AssemblyName);
            IStringLocalizer s2= stringLocalizerFactory.Create(typeof(SharedLocalResource));
            IStringLocalizer s3= stringLocalizerFactory.Create(typeof(SystemManagerResource));
            IStringLocalizer s4= stringLocalizerFactory.Create(typeof(CodeLocalResource));
            string f11 = s1["Yes"];
            string f22 = s2["Yes"];
            string f33 = s3["CodeName"];
            string f44 = s4["今日"];
            if (result.Items.Any())
            {
                foreach (var item in result.Items)
                {
                    string a= L.TextOf<CodeLocalResource>()["Yes"];
                    string f= L.Text["Yes"];
                    string b= L.TextOf<SystemManagerResource>()["Yes"];
                    string c= L.TextOf<SharedLocalResource>()["Yes"];
                    string d = Lo.GetValue<CodeLocalResource>("Yes");
                    string j = Lo.GetValue<SharedLocalResource>("Yes");
                    string k = Lo.GetValue<SystemManagerResource>("Yes");
                    string h = Lo.GetValue("Yes");
                    string e = localizationLocalizer["Yes"];
                    string g = localizationLocalizer1["Yes"];
                    string o= localizationLocalizer2["Yes"];
                    item.LocalCodeName = localizationLocalizer[item.CodeName];
                }
            }
            return result;
        }
    }
}
