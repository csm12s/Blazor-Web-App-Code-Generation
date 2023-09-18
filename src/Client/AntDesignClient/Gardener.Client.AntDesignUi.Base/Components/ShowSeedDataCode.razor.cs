// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using BlazorMonaco.Editor;

namespace Gardener.Client.AntDesignUi.Base.Components
{
    public partial class ShowSeedDataCode : FeedbackComponent<Task<string>, bool>
    {
        private StandaloneCodeEditor _editor = null!;
        private bool _loading = false;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private async Task EditorOnDidInit()
        {
            _loading = true;
            await _editor.SetValue(await this.Options);
            _loading = false;
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
    }
}
