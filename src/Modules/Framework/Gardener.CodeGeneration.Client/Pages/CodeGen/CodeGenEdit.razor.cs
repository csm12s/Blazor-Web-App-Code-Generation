using Gardener.Base;
using Gardener.Client.Base;
using Gardener.CodeGeneration.Dtos;
using Gardener.CodeGeneration.Services;
using Gardener.Common;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Services;
using Humanizer.Localisation;
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
    private List<ResourceDto> _menus = new List<ResourceDto>();

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
        _menus = await resourceService.GetTree();

        await base.OnInitializedAsync();

    }
    #endregion

    protected async Task OnTableSelectChanged()
    {
        _editModel.ClassName = _editModel.TableName
            .Replace("Sys_", "")
            .Replace("_", "");
        _editModel.Module = _editModel.TableName
            .Split("_")
            .FirstOrDefault();
        _editModel.MenuName = _editModel.ClassName;
    }
}
