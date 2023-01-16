using Gardener.Client.Base;
using Gardener.CodeGeneration.Dtos;
using Gardener.CodeGeneration.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Client.Services;

/// <summary>
/// 代码生成Config
/// </summary>
[ScopedService]
public class CodeGenConfigClientService: ClientServiceBase<CodeGenConfigDto, Guid>, 
    ICodeGenConfigService
{
    #region Init
    public CodeGenConfigClientService(IApiCaller apiCaller) 
        : base(apiCaller, "code-gen-config")
    {
    }

    #endregion

    public Task<List<CodeGenConfigDto>> GetCodeGenConfigsByCodeGenId(Guid codeGenId)
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
    public Task DeleteAndAddList(List<TableColumnInfo> tableColumnOuputs, CodeGenDto codeGen)
    {
        throw new System.NotImplementedException();
    }

    public Task DeleteByCodeGenId(Guid id)
    {
        throw new System.NotImplementedException();
    }



    #endregion
}
