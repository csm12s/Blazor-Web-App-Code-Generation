// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Microsoft.AspNetCore.Components;

namespace Gardener.Client.AntDesignUi.Base.Components
{
    /// <summary>
    /// 可以随机颜色
    /// </summary>
    [Obsolete("请用TagPro代替")]
    public partial class TagRandomColor : AntDomComponentBase
    {

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public object Text { get; set; } = null!;

        private string color = "";

        private string[] colors = { "magenta", "pink", "red", "volcano", "orange", "green", "cyan", "blue", "lime", "geekblue", "purple" };

        protected override void OnInitialized()
        {
            int code = Math.Abs((Text.ToString() ?? "").GetHashCode());
            int colorIndex = (code % 1000) % colors.Length;
            color = colors[colorIndex];
        }

    }
}
