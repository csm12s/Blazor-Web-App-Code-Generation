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
            var list = await _codeRepository
                .AsQueryable(false)
                .Include(x => x.CodeType)
                .Where(x => x.CodeType.IsLocked == false && x.CodeType.IsDeleted == false && x.IsLocked == false && x.IsDeleted == false && codeTypeIds.Contains(x.CodeTypeId))
                .Select(x => x.Adapt<CodeDto>())
                .ToListAsync();
            Dictionary<int, IEnumerable<CodeDto>> result = new Dictionary<int, IEnumerable<CodeDto>>();
            for (int i = 0; i < codeTypeIds.Length; i++)
            {
                result.Add(codeTypeIds[i], list.Where(x => x.CodeTypeId.Equals(codeTypeIds[i])).Select(x => x.Adapt<CodeDto>()).ToList());
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
            List<CodeTypeDto> codeTypes = await _repository.AsQueryable(false).Where(x => x.IsLocked == false && x.IsDeleted == false && codeTypeValues.Contains(x.CodeTypeValue)).Select(x => x.Adapt<CodeTypeDto>()).ToListAsync();
            if (!codeTypes.Any())
            {
                return new Dictionary<string, IEnumerable<CodeDto>>(0);
            }
            var list = await _codeRepository.AsQueryable(false).Where(x => x.IsLocked == false && x.IsDeleted == false && codeTypes.Select(x => x.Id).Contains(x.CodeTypeId)).ToListAsync();
            Dictionary<string, IEnumerable<CodeDto>> result = new Dictionary<string, IEnumerable<CodeDto>>();
            foreach (CodeTypeDto codeType in codeTypes)
            {
                var codes = list.Where(x => x.CodeTypeId.Equals(codeType.Id)).Select(x =>
                  {
                      var code = x.Adapt<CodeDto>();
                      code.CodeType = codeType;
                      return code;
                  }).ToList();
                result.Add(codeType.CodeTypeValue, codes);
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
            await CodeUtil.InitAllCode(this);
            return true;
        }
    }
}
