// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
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
    /// 字典类型管理
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class CodeTypeService : ServiceBase<CodeType, CodeTypeDto>, ICodeTypeService, IScoped
    {
        private readonly IRepository<Code> _codeRepository;
        /// <summary>
        /// 字典类型管理
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="codeRepository"></param>
        public CodeTypeService(IRepository<CodeType, MasterDbContextLocator> repository, IRepository<Code> codeRepository) : base(repository)
        {
            _codeRepository = codeRepository;
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
            return await _codeRepository
                .AsQueryable(false)
                .Include(x => x.CodeType)
                .Where(x => x.CodeType.IsLocked == false && x.CodeType.IsDeleted == false && x.IsLocked == false && x.IsDeleted == false && x.CodeTypeId == codeTypeId)
                .Select(x => x.Adapt<CodeDto>()).ToListAsync();
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
            return await _codeRepository
                .AsQueryable(false)
                .Include(x => x.CodeType)
                .Where(x => x.CodeType.IsLocked == false && x.CodeType.IsDeleted == false && x.IsLocked == false && x.IsDeleted == false && x.CodeType.CodeTypeValue.Equals(codeTypeValue))
                .Select(x => x.Adapt<CodeDto>()).ToListAsync();
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
    }
}
