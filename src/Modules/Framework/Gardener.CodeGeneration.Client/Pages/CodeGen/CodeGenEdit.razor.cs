using Gardener.Client.Base;
using Gardener.CodeGeneration.Dtos;
using Gardener.CodeGeneration.Services;
using Gardener.Common;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.CodeGeneration.Client.Pages.CodeGen;

public partial class CodeGenEdit : EditOperationDialogBase<CodeGenDto, int>
{
    #region Init
    private List<SelectItem> _allTables = new List<SelectItem>();

    [Inject]
    IResourceService resourceService { get; set; }

    [Inject]
    ICodeGenService codeGenService { get; set; }

    private List<ResourceDto> _menuTree = new List<ResourceDto>();

    public string _menuParentId
    {
        get
        {
            return _editModel.MenuParentId?.ToString();
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                _editModel.MenuParentId = Guid.Parse(value);
            }
        }

    }

    [Inject]
    private ICodeGenService service { get; set; }

    protected override async Task OnInitializedAsync()
    {
        // table select
        var tableInfos = await service.GetTableListAsync();
        _allTables = tableInfos.ToSelectItems(it=>it.TableName, it=>it.TableName);
        
        // menus
        _menuTree = await resourceService.GetTree();

        // remove button
        var list = new List<ResourceDto>();
        _menuTree.TreeToList(list);
        list.RemoveAll(it => it.Type != Base.Enums.ResourceType.Root
            && it.Type != Base.Enums.ResourceType.Menu);
        _menuTree = TreeTools.ListToTree(list);

        await base.OnInitializedAsync();

    }
    #endregion

    protected async Task OnTableSelectChanged()
    {
        // Class name
        var nameList = _editModel.TableName
            .Replace("Sys_", "")
            .Split("_")
            .ToList();
        var newList = new List<string>();
        nameList.ForEach(it => newList.Add(it = it.ToLower().FirstToUpper()));
        _editModel.ClassName = string.Join("", newList);

        // Module
        _editModel.Module = _editModel.TableName
            .Split("_")
            .FirstOrDefault();
        // Menu
        _editModel.MenuName = _editModel.ClassName;
    }

    protected virtual async Task OnlySaveCodeGen()
    {
        _isLoading = true;
        //修改
        _editModel.UpdateCodeGenConfig = false;
        var result = await _service.Update(_editModel);
        if (result)
        {
            messageService.Success(localizer.Combination("编辑", "成功"));
            await base.FeedbackRef.CloseAsync(OperationDialogOutput<int>.Succeed(_editModel.Id));
        }
        else
        {
            messageService.Error(localizer.Combination("编辑", "失败"));
        }
        _isLoading = false;
    }
}
