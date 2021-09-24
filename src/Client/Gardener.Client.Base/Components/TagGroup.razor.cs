// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gardener.Client.Base.Components
{
    public partial class TagGroup : AntInputComponentBase<List<string>>
    {

        

        private string _tagInputValue = string.Empty;
        private bool _tagInputVisible = false;

        protected override Task OnInitializedAsync()
        {
            this.UpdateClassMap();
            return base.OnInitializedAsync();
        }

        protected override void OnParametersSet()
        {
            this.UpdateClassMap();
            base.OnParametersSet();
        }

        private void UpdateClassMap()
        {
            string prefix = "ant-tag";
            this.ClassMapper.Clear().Add(prefix)
                .If($"{prefix}-has-color", () => false)
                ;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="tag"></param>
        private void OnCloseTagClick(string tag) 
        {
            this.CurrentValue.Remove(tag);
        }
        /// <summary>
        /// tag输入框失去焦点
        /// </summary>
        private async Task HandleTagInputValue()
        {
            if (string.IsNullOrEmpty(_tagInputValue)) return;

            if (!this.CurrentValue.Any(x => x.Equals(_tagInputValue)))
            {
                this.CurrentValue.Add(_tagInputValue);
            }
            this._tagInputValue = string.Empty;
            this._tagInputVisible = false;
            
        }
        /// <summary>
        /// 点击添加标签
        /// </summary>
        /// <returns></returns>
        private async Task OnTagAddClick()
        {
            _tagInputVisible = !_tagInputVisible;
        }
    }
}
