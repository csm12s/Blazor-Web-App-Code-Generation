
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
        Task<List<CodeGenConfigDto>> GetCodeGenConfigsByCodeGenId(Guid codeGenId);
        Task<bool> SaveAll(List<CodeGenConfigDto> list);

        // NoAction:
        Task DeleteAndAddList(List<TableColumnInfo> tableColumnOuputs, CodeGenDto codeGen);
        Task DeleteByCodeGenId(Guid id);
    }
}
