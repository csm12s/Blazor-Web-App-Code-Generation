// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.SystemManager.Client.Pages.FunctionView;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.SystemManager.Client.Pages.CodeView
{
    /// <summary>
    /// 字典类型列表
    /// </summary>
    public partial class CodeType : ListOperateTableBase<CodeTypeDto, int, CodeTypeEdit, SystemManagerResource>
    {
        /// <summary>
        /// 显示字典列表
        /// </summary>
        /// <param name="codeType"></param>
        private async Task OnClickShowCodesManager(CodeTypeDto codeType)
        {
            await OpenOperationDialogAsync<Code, OperationDialogInput<int?>, OperationDialogOutput> (codeType.CodeTypeName, OperationDialogInput<int?>.Other(codeType.Id), width: 1200, onClose: ot => {

                System.Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(ot));
                return Task.CompletedTask;
            });
        }

    }
}
