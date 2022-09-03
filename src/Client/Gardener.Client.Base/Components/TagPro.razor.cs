// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Attributes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;

namespace Gardener.Client.Base.Components
{
    /// <summary>
    /// tag 加强
    /// </summary>
    /// <remarks>
    /// 详情见： https://gitee.com/hgflydream/Gardener/issues/I5P6FA
    /// </remarks>
    public partial class TagPro : AntDomComponentBase
    {
        /// <summary>
        /// 随机用的预设颜色
        /// </summary>
        private PresetColor[] colors = Enum.GetValues<PresetColor>();

        /// <summary>
        /// Tag content
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// tag 显示的文本
        /// </summary>
        /// <remarks>
        /// 如果是枚举类型，可以自动获取枚举上的
        /// <see cref="System.ComponentModel.DescriptionAttribute"/>作为 文本
        /// 和
        /// <see cref="TagColorAttribute"/> 作为颜色
        /// 如果没有可用颜色，将自动随机从 <see cref="PresetColor"/> 中获取一个
        /// </remarks>
        [Parameter]
        public object Text { get; set; }

        /// <summary>
        /// 自定义颜色
        /// </summary>
        [Parameter]
        public string Color { get; set; }

        /// <summary>
        /// 禁用随机颜色
        /// </summary>
        [Parameter]
        public bool DisabledRandomColor { get; set; }=false;

        /// <summary>
        /// 预设颜色
        /// </summary>
        [Parameter]
        public PresetColor? PresetColor { get; set; }

        /// <summary>
        /// Whether the Tag can be closed
        /// </summary>
        [Parameter]
        public bool Closable { get; set; }

        /// <summary>
        /// Whether the Tag can be checked
        /// </summary>
        [Parameter]
        public bool Checkable { get; set; }

        /// <summary>
        /// Checked status of Tag
        /// </summary>
        [Parameter]
        public bool Checked { get; set; }

        /// <summary>
        /// Callback executed when Tag is checked/unchecked
        /// </summary>
        [Parameter]
        public EventCallback<bool> CheckedChanged { get; set; }


        /// <summary>
        /// Set the tag's icon 
        /// </summary>
        [Parameter]
        public string Icon { get; set; }

        /// <summary>
        /// Callback executed when tag is closed
        /// </summary>
        [Parameter]
        public EventCallback<MouseEventArgs> OnClose { get; set; }

        /// <summary>
        /// Triggered before true closing, can prevent the closing
        /// </summary>
        [Parameter]
        public EventCallback<CloseEventArgs<MouseEventArgs>> OnClosing { get; set; }

        /// <summary>
        /// Callback executed when tag is clicked (it is not called 
        /// when closing icon is clicked).
        /// </summary>
        [Parameter]
        public EventCallback OnClick { get; set; }

        /// <summary>
        /// Whether the Tag is closed or not
        /// </summary>
        [Parameter]
        public bool Visible { get; set; } = true;

        private string value = string.Empty;

        protected override void OnInitialized()
        {
            //如果color没有值，PresetColor 有值，使用PresetColor
            if (string.IsNullOrEmpty(this.Color) && this.PresetColor.HasValue)
            {
                this.Color = this.PresetColor.Value.ToString().ToLower();
            }
            //Text为null时什么也推断不出来
            if (Text == null)
            {
                base.OnInitialized();
                return;
            }
            //Text是枚举时，可以从枚举推断出来描述和颜色
            if (Text.GetType().IsEnum)
            {
                value = Common.EnumHelper.GetEnumDescription((Enum)Text);
                if (string.IsNullOrEmpty(this.Color))
                {
                    TagColorAttribute tagColorAttribute = Common.EnumHelper.GetEnumAttribute<TagColorAttribute>((Enum)Text);
                    if (tagColorAttribute != null)
                    {
                        if (!string.IsNullOrEmpty(tagColorAttribute.Color))
                        {
                            this.Color = tagColorAttribute.Color;
                        }
                        else if(tagColorAttribute.PresetColor.HasValue)
                        {
                            this.Color = tagColorAttribute.PresetColor.Value.ToString().ToLower();
                        }
                        
                    }
                }
            }
            
            if (string.IsNullOrEmpty(this.Color) && !DisabledRandomColor)
            {
                this.Color = GetRandomColor(Text.ToString());
            }
            if (string.IsNullOrEmpty(value))
            {
                value = Text.ToString();
            }
            //本地化
            value = LocalizerUtil.GetValue(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string GetRandomColor(string text)
        {

            int code = Math.Abs(text.GetHashCode());
            int colorIndex = (code % 1000) % colors.Length;
            return colors[colorIndex].ToString();
        }
    }

}
