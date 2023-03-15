
using Gardener.Base;
using Gardener.CodeGeneration.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Services
{
    /// <summary>
    /// 代码生成服务, DB First
    /// </summary>
    public interface ICodeGenConfigService: IServiceBase<CodeGenConfigDto, Guid>
    {
        /// <summary>
        /// GetCodeGenConfigsByCodeGenId
        /// </summary>
        /// <param name="codeGenId"></param>
        /// <returns></returns>
        Task<List<CodeGenConfigDto>> GetCodeGenConfigsByCodeGenId(Guid codeGenId);
        /// <summary>
        /// SaveAll
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<bool> SaveAll(List<CodeGenConfigDto> list);

        /// <summary>
        /// NoAction
        /// </summary>
        /// <param name="tableColumnOuputs"></param>
        /// <param name="codeGen"></param>
        /// <returns></returns>
        Task DeleteAndAddList(List<TableColumnInfo> tableColumnOuputs, CodeGenDto codeGen);
        /// <summary>
        /// DeleteByCodeGenId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteByCodeGenId(Guid id);
    }
}
