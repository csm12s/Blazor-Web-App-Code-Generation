// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using BlazorMonaco.Editor;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.EasyJob.Dtos;
using Gardener.EasyJob.Resources;
using Microsoft.AspNetCore.Components.Forms;

namespace Gardener.EasyJob.Client.Pages.JobView
{
    /// <summary>
    /// 定时任务详情页
    /// </summary>
    public partial class JobDetailEdit : EditOperationDialogBase<SysJobDetailDto, int, EasyJobLocalResource>
    {
        private string tabActiveKey = "baseInfo";

        private StandaloneCodeEditor _editor = null!;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private async Task EditorOnDidInit()
        {
            await _editor.SetValue(_editModel.ScriptCode);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="editor"></param>
        /// <returns></returns>
        private StandaloneEditorConstructionOptions EditorConstructionOptions(Editor editor)
        {
            return new StandaloneEditorConstructionOptions
            {
                AutomaticLayout = true,
                Language = "csharp"
            };
        }

        protected override async Task OnFormFinish(EditContext editContext)
        {
            _editModel.ScriptCode = await _editor.GetValue();
            await base.OnFormFinish(editContext);
        }
    }
}
