using Gardener.Client.Base;
using Gardener.CodeGeneration.Dtos;
using Gardener.CodeGeneration.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Client.Services;

/// <summary>
/// 代码生成Config
/// </summary>
[ScopedService]
public class CodeGenConfigClientService: ClientServiceBase<CodeGenConfigDto>, 
    ICodeGenConfigService
{
    #region Init
    public CodeGenConfigClientService(IApiCaller apiCaller) 
        : base(apiCaller, "code-gen-config")
    {
    }

    #endregion

    public Task<List<CodeGenConfigDto>> GetCodeGenConfigsByCodeGenId(int codeGenId)
    {
        var url = $"{controller}/code-gen-configs-by-code-gen-id/{codeGenId}";
        return apiCaller.GetAsync<List<CodeGenConfigDto>>(url);
    }

    public Task<bool> SaveAll(List<CodeGenConfigDto> list)
    {
        var url = $"{controller}/save-all";
        return apiCaller.PostAsync<List<CodeGenConfigDto>, bool>(url, list);
    }


    #region NoAction
    // TODO: 建议是否可以考虑使用原来的Controller模式，即现在的ServiceBase改成Controller，
    // 增加新的BaseService用于业务逻辑，这样这里就不用有NoAction的方法了
    // 特别是对于业务复杂的场景
    public Task DeleteAndAddList(List<TableColumnInfo> tableColumnOuputs, CodeGenDto codeGen)
    {
        throw new System.NotImplementedException();
    }

    public Task DeleteByCodeGenId(int id)
    {
        throw new System.NotImplementedException();
    }



    #endregion
}
