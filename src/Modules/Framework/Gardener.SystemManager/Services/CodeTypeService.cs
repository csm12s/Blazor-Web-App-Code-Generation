// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.EntityFramwork;
using Gardener.SystemManager.Domains;
using Gardener.SystemManager.Dtos;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.SystemManager.Services
{
    /// <summary>
    /// 字典类型管理
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
    public class CodeTypeService : ServiceBase<CodeType, CodeTypeDto>, ICodeTypeService
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
        /// 根据多个字典类型获取所有字典的结果
        /// </summary>
        /// <param name="codeTypeIds"></param>
        /// <returns></returns>
        /// <remarks>
        /// 根据多个字典类型获取所有字典的结果
        /// </remarks>
        public async Task<Dictionary<int, List<CodeDto>>> GetCodeDic(params int[] codeTypeIds)
        {
            var list=await _codeRepository.AsQueryable(false).Where(x => x.IsLocked == false && x.IsDeleted == false && codeTypeIds.Contains(x.CodeTypeId)).ToListAsync();
            Dictionary<int, List<CodeDto>> result = new Dictionary<int, List<CodeDto>>();
            for (int i = 0; i < codeTypeIds.Length; i++)
            {
                result.Add(codeTypeIds[i], list.Where(x => x.CodeTypeId.Equals(codeTypeIds[i])).Select(x=>x.Adapt<CodeDto>()).ToList());
            }
            return result;
        }
        /// <summary>
        /// 根据字典类型获取字典列表
        /// </summary>
        /// <param name="codeTypeId"></param>
        /// <returns></returns>
        /// <remarks>
        /// 根据字典类型获取字典列表
        /// </remarks>
        public Task<List<CodeDto>> GetCodes([ApiSeat(ApiSeats.ActionStart)] int codeTypeId)
        {
            return _codeRepository.AsQueryable(false).Where(x => x.IsLocked == false && x.IsDeleted == false && x.CodeTypeId==codeTypeId).Select(x => x.Adapt<CodeDto>()).ToListAsync();
        }
    }
}
