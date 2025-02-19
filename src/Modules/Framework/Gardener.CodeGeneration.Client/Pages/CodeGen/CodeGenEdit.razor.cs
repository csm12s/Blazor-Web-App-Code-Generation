﻿using Gardener.Base.Resources;
using Gardener.Client.Base;
using Gardener.CodeGeneration.Resources;
using Gardener.CodeGeneration.Dtos;
using Gardener.CodeGeneration.Services;
using Gardener.Common;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gardener.Client.AntDesignUi.Base.Components;
using AntDesign;

namespace Gardener.CodeGeneration.Client.Pages.CodeGen;

public partial class CodeGenEdit : EditOperationDialogBase<CodeGenDto, Guid, CodeGenLocalResource>
{
    #region Init
    private List<SelectItem> _allTables = new List<SelectItem>();

    [Inject]
    IResourceService resourceService { get; set; } = null!;

    [Inject]
    ICodeGenService codeGenService { get; set; } = null!;

    private List<ResourceDto> _menuTree = new List<ResourceDto>();

    public string _menuParentId
    {
        get
        {
            return _editModel.MenuParentId?.ToString() ?? string.Empty;
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
    private ICodeGenService service { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        // table select
        var tableInfos = await service.GetTableListAsync();
        _allTables = tableInfos.ToSelectItems
            (it=>it.TableName, it=>it.ClientSelectLabelText ?? string.Empty) ?? new List<SelectItem>();
        
        // menus
        _menuTree = await resourceService.GetTree();

        // remove button
        var list = new List<ResourceDto>();
        _menuTree.TreeToList(list);
        list.RemoveAll(it => it.Type != Base.Enums.ResourceType.Root
            && it.Type != Base.Enums.ResourceType.Menu);
        _menuTree = TreeHelper.ListToTree(list);

        await base.OnInitializedAsync();

    }
    #endregion

    protected Task OnTableSelectChanged()
    {
        // Class name
        _editModel.ClassName = _editModel.TableName
            .Replace("Sys_", "")
            .ToUpperCamel();

        // 这里处理非常规表名: AAA_BBB_CCC -> AaaBbbCcc
        //var nameList = _editModel.TableName
        //    .Replace("Sys_", "")
        //    .Split("_")
        //    .ToList();
        //var newList = new List<string>();
        //nameList.ForEach(it => newList.Add(it = it.ToLower().FirstToUpper()));
        //_editModel.ClassName = string.Join("", newList);

        // Module
        //_editModel.Module = _editModel.TableName
        //    .Split("_")
        //    .FirstOrDefault();
        // Menu
        //_editModel.MenuParentId
        return Task.CompletedTask;
    }

    protected virtual async Task OnlySaveCodeGen()
    {
        _dialogLoading.Start();
        //修改
        _editModel.UpdateCodeGenConfig = false;
        var result = await BaseService.Update(_editModel);
        if (result)
        {
            MessageService.Success(Localizer.Combination(nameof(SharedLocalResource.Edit), nameof(SharedLocalResource.Success)));
            await base.FeedbackRef.CloseAsync(OperationDialogOutput<Guid>.Succeed(_editModel.Id));
        }
        else
        {
            MessageService.Error(Localizer.Combination(nameof(SharedLocalResource.Edit), nameof(SharedLocalResource.Fail)));
        }
        _dialogLoading.Stop();
    }
}
