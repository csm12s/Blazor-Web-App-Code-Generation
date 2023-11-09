// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using BlazorMonaco.Editor;

namespace Gardener.Client.AntDesignUi.Base.Components
{
    /// <summary>
    /// 配置
    /// </summary>
    public class ShowCodeOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public ShowCodeOptions() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public ShowCodeOptions(Task<string> code)
        { 
            this.Code = code;
        }
        /// <summary>
        /// 高度
        /// </summary>
        public string Height { get; set; } = "85Vh";
        /// <summary>
        /// 值
        /// </summary>
        public Task<string> Code { get; set; } = Task.FromResult(string.Empty);
        /// <summary>
        /// 语言样式
        /// </summary>
        public string Language { get; set; } = "csharp";
        /// <summary>
        /// 
        /// </summary>
        public bool AutomaticLayout { get; set; } = true;
        /// <summary>
        /// 
        /// </summary>
        public bool FormatOnPaste { get; set; } = true;
    }

    public partial class ShowCode : FeedbackComponent<ShowCodeOptions, bool>
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
            await _editor.SetValue(await this.Options.Code);
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
                AutomaticLayout = this.Options.AutomaticLayout,
                Language = this.Options.Language,
                FormatOnPaste= this.Options.FormatOnPaste
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _editor?.Dispose();
        }
    }
}
