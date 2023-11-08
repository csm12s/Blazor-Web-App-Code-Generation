// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DatabaseAccessor.Extensions;
using Furion.DependencyInjection;
using Gardener.Base.Entity;
using Gardener.Base.Resources;
using Gardener.EntityFramwork;
using Gardener.LocalizationLocalizer;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Utils;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Gardener.SystemManager.Services
{
    /// <summary>
    /// 字典类型管理
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class CodeTypeService : ServiceBase<CodeType, CodeTypeDto>, ICodeTypeService, IScoped
    {
        private readonly IRepository<Code> _codeRepository;
        private readonly ILocalizationLocalizer<CodeLocalResource> localizationLocalizer;

        /// <summary>
        /// 字典类型管理
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="codeRepository"></param>
        /// <param name="localizationLocalizer"></param>
        public CodeTypeService(IRepository<CodeType, MasterDbContextLocator> repository, IRepository<Code> codeRepository, ILocalizationLocalizer<CodeLocalResource> localizationLocalizer) : base(repository)
        {
            _codeRepository = codeRepository;
            this.localizationLocalizer = localizationLocalizer;
        }
        /// <summary>
        /// 根据多个字典类型编号获取所有字典的结果
        /// </summary>
        /// <param name="codeTypeIds"></param>
        /// <returns></returns>
        /// <remarks>
        /// 根据多个字典类型编号获取所有字典的结果
        /// </remarks>
        public async Task<Dictionary<int, IEnumerable<CodeDto>>> GetCodeDic(params int[] codeTypeIds)
        {
            IQueryable<Code> query = _codeRepository
                .AsQueryable(false)
                .Include(x => x.CodeType)
                .Where(x => x.CodeType.IsLocked == false && x.CodeType.IsDeleted == false && x.IsLocked == false && x.IsDeleted == false);
            if (codeTypeIds.Length > 0)
            {
                query.Where(x => codeTypeIds.Contains(x.CodeTypeId));
            }
            var list = await query.Select(x => x.Adapt<CodeDto>())
             .ToListAsync();
            SetLocalCodeName(list);
            Dictionary<int, IEnumerable<CodeDto>> result = new Dictionary<int, IEnumerable<CodeDto>>();
            foreach (int codeTypeId in list.Select(x => x.CodeTypeId).Distinct())
            {
                result.Add(codeTypeId, list.Where(x => x.CodeTypeId.Equals(codeTypeId)));
            }
            return result;
        }
        /// <summary>
        /// 根据多个字典类型编号获取所有字典的结果
        /// </summary>
        /// <param name="codeTypeValues"></param>
        /// <returns></returns>
        /// <remarks>
        /// 根据多个字典类型编号获取所有字典的结果
        /// </remarks>
        public async Task<Dictionary<string, IEnumerable<CodeDto>>> GetCodeDicByValues(params string[] codeTypeValues)
        {
            IEnumerable<CodeDto> codes = new List<CodeDto>();

            if (codeTypeValues.Length > 0)
            {
                List<int> codeTypeIds = await _repository
                    .AsQueryable(false)
                    .Where(x => x.IsLocked == false && x.IsDeleted == false && codeTypeValues.Contains(x.CodeTypeValue))
                    .Select(x => x.Id).ToListAsync();
                if (!codeTypeIds.Any())
                {
                    return new Dictionary<string, IEnumerable<CodeDto>>(0);
                }
                IQueryable<Code> query = _codeRepository
                    .AsQueryable(false)
                    .Include(x => x.CodeType)
                    .Where(x => x.CodeType.IsLocked == false && x.CodeType.IsDeleted == false && x.IsLocked == false && x.IsDeleted == false);
                if (codeTypeIds.Count > 0)
                {
                    query.Where(x => codeTypeIds.Contains(x.CodeTypeId));
                }
                codes = await query.Select(x => x.Adapt<CodeDto>())
                 .ToListAsync();
            }
            else
            {
                codes = await _codeRepository
                       .AsQueryable(false)
                       .Include(x => x.CodeType)
                       .Where(x => x.CodeType.IsLocked == false && x.CodeType.IsDeleted == false && x.IsLocked == false && x.IsDeleted == false)
                       .Select(x => x.Adapt<CodeDto>()).ToListAsync();
            }
            SetLocalCodeName(codes);
            Dictionary<string, IEnumerable<CodeDto>> result = new Dictionary<string, IEnumerable<CodeDto>>();
            foreach (string codeTypeValue in codes.Select(x => x.CodeType.CodeTypeValue).Distinct())
            {
                var items = codes.Where(x => x.CodeType.CodeTypeValue.Equals(codeTypeValue)).ToList();
                result.Add(codeTypeValue, items);
            }
            return result;
        }
        /// <summary>
        /// 根据字典类型编号获取字典列表
        /// </summary>
        /// <param name="codeTypeId"></param>
        /// <returns></returns>
        /// <remarks>
        /// 根据字典类型编号获取字典列表
        /// </remarks>
        public async Task<IEnumerable<CodeDto>> GetCodes([ApiSeat(ApiSeats.ActionStart)] int codeTypeId)
        {
            IEnumerable<CodeDto> result = await _codeRepository
                .AsQueryable(false)
                .Include(x => x.CodeType)
                .Where(x => x.CodeType.IsLocked == false && x.CodeType.IsDeleted == false && x.IsLocked == false && x.IsDeleted == false && x.CodeTypeId == codeTypeId)
                .Select(x => x.Adapt<CodeDto>()).ToListAsync();
            SetLocalCodeName(result);
            return result;
        }
        /// <summary>
        /// 根据字典类型值获取字典列表
        /// </summary>
        /// <param name="codeTypeValue"></param>
        /// <returns></returns>
        /// <remarks>
        /// 根据字典类型值获取字典列表
        /// </remarks>
        public async Task<IEnumerable<CodeDto>> GetCodesByValue([ApiSeat(ApiSeats.ActionStart)] string codeTypeValue)
        {
            IEnumerable<CodeDto> result = await _codeRepository
                .AsQueryable(false)
                .Include(x => x.CodeType)
                .Where(x => x.CodeType.IsLocked == false && x.CodeType.IsDeleted == false && x.IsLocked == false && x.IsDeleted == false && x.CodeType.CodeTypeValue.Equals(codeTypeValue))
                .Select(x => x.Adapt<CodeDto>()).ToListAsync();
            SetLocalCodeName(result);
            return result;
        }
        /// <summary>
        /// 刷新字典工具缓存
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RefreshCodeUtilCache()
        {
            CodeUtil.InitAllCode(await GetCodeDicByValues());
            return true;
        }
        /// <summary>
        /// 设置本地化codeName
        /// </summary>
        /// <param name="codes"></param>
        private void SetLocalCodeName(IEnumerable<CodeDto> codes)
        {
            if (codes.Any())
            {
                foreach (var item in codes)
                {
                    item.LocalCodeName = localizationLocalizer[item.CodeName];
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <remarks>
        /// 根据主键删除一条数据
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<bool> Delete(int id)
        {
            var codes = await _codeRepository.Where(x => x.CodeTypeId.Equals(id)).ToListAsync();
            foreach (var code in codes)
            {
                await code.DeleteAsync();
            }
            return await base.Delete(id);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <remarks>
        /// 根据多个主键批量删除
        /// </remarks>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Summary = "批量删除", Description = "根据多个主键批量删除")]
        public override async Task<bool> Deletes([FromBody] int[] ids)
        {
            foreach(var id in ids)
            {
                await Delete(id);
            }
            return true;
        }
    }
}
