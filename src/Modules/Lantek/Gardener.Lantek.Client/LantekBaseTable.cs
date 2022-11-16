

using AntDesign;
using Gardener.Client.Base;
using Gardener.Client.Base.Components;

namespace Gardener.Lantek.Client;

public abstract class LantekBaseTable<TDto, TOperationDialog>
    : BaseMainTable<TDto, int, TOperationDialog> 
    where TDto : LantekBaseDto, new() 
    where TOperationDialog : FeedbackComponent<OperationDialogInput<int>, OperationDialogOutput<int>>
{
   
    public readonly string _ControllerName;
    public string _Module = "Lantek";

    protected LantekBaseTable(string controllerName)
    {
        this._ControllerName = controllerName;
    }

    public string GetAuthKey(string key)
    {
        return _Module + "_" + _ControllerName + "_" + key;
    }

}

