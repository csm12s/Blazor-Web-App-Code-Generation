// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Base;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.SystemManager.Dtos;
using Gardener.SystemManager.Resources;

namespace Gardener.SystemManager.Client.Pages.CodeView
{
    /// <summary>
    /// 字典列表
    /// </summary>
    public partial class Code : ListOperateTableBase<CodeDto, int, CodeEdit, CodeEditParams, OperationDialogOutput, SystemManagerResource,OperationDialogInput<int?>,OperationDialogOutput>
    {
        private TableSize tableSize = TableSize.Default;
        protected override void SetTableSearchParameters(TableSearchSettings tableSearchSettings, List<Func<List<FilterGroup>?>> tableSearchFilterGroupProviders)
        {
            //传入编号，以小table展示
            if (this.Options?.Data != null)
            {
                tableSize = TableSize.Small;
                _pageSize = 10;
                tableSearchSettings.DefaultValue.Add(nameof(CodeDto.CodeTypeId), this.Options.Data);
            }
            base.SetTableSearchParameters(tableSearchSettings, tableSearchFilterGroupProviders);
        }
        
        /// <summary>
        /// 点击添加后，处理输入弹框的参数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override Task<bool> OnClickAddRunBefore(CodeEditParams input)
        {
            input.CodeTypeId = this.Options.Data;

            return base.OnClickAddRunBefore(input);
        }
    }
}
