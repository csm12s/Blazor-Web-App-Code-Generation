using Gardener.Client.Base;
using Gardener.CodeGeneration.Dtos;
using Gardener.CodeGeneration.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Client.Services;

/// <summary>
/// 代码生成
/// </summary>
[ScopedService]
public class CodeGenClientService: ClientServiceBase<CodeGenDto>, 
    ICodeGenService
{
    #region Init
    public CodeGenClientService(IApiCaller apiCaller) 
        : base(apiCaller, "code-gen")
    {
    }
    #endregion
    public async Task<bool> GenerateCode(int[] codeGenIds)
    {
        var url = $"{controller}/generate-code";
        return await apiCaller.PostAsync<int[], bool>(url, codeGenIds);
    }

    public async Task<bool> GenerateMenu(int codeGenId)
    {
        var url = $"{controller}/generate-menu/{codeGenId}";
        return await apiCaller.PostAsync<int, bool>(url, codeGenId);
    }

    public async Task<List<TableOutput>> GetTableListAsync()//string dbContextLocatorName = ""
    {
        var url = $"{controller}/table-list";
        return await apiCaller.GetAsync<List<TableOutput>>(url);
    }

    public async Task<bool> OpenCodeGenFolder()
    {
        var url = $"{controller}/open-code-gen-folder";
        return await apiCaller.PostWithoutBodyAsync<bool>(url);
    }
}
