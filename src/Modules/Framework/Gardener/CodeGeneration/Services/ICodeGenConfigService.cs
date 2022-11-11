
using Gardener.Base;
using Gardener.CodeGeneration.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Services
{
    /// <summary>
    /// 代码生成服务, DB First
    /// </summary>
    public interface ICodeGenConfigService: IServiceBase<CodeGenConfigDto, int>
    {
        Task<List<CodeGenConfigDto>> GetCodeGenConfigsByCodeGenId(int codeGenId);
        Task<bool> SaveAll(List<CodeGenConfigDto> list);

        // NoAction:
        Task DeleteAndAddList(List<TableColumnInfo> tableColumnOuputs, CodeGenDto codeGen);
        Task DeleteByCodeGenId(int id);
    }
}
