using Gardener.Client.Base;
using Gardener.CodeGeneration.Dtos;
using Gardener.CodeGeneration.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Client.Services;

/// <summary>
/// 代码生成
/// </summary>
[ScopedService]
public class CodeGenClientService: ClientServiceBase<CodeGenDto, Guid>, 
    ICodeGenService
{
    #region Init
    public CodeGenClientService(IApiCaller apiCaller) 
        : base(apiCaller, "code-gen")
    {
    }
    #endregion
    public async Task<bool> GenerateCode(Guid[] codeGenIds)
    {
        var url = $"{controller}/generate-code";
        return await apiCaller.PostAsync<Guid[], bool>(url, codeGenIds);
    }

    public Task<bool> GenerateLocale(Guid codeGenId)
    {
        var url = $"{controller}/generate-locale/{codeGenId}";
        return apiCaller.PostAsync<Guid, bool>(url, codeGenId);
    }

    public async Task<bool> GenerateMenu(Guid codeGenId)
    {
        var url = $"{controller}/generate-menu/{codeGenId}";
        return await apiCaller.PostAsync<Guid, bool>(url, codeGenId);
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
