// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.DeptView
{
    public partial class DeptEdit : EditDrawerBase<DeptDto, int>
    {
        [Inject]
        IDeptService deptService { get; set; }

        //部门树
        List<DeptDto> deptDatas;
        /// <summary>
        /// 父级部门编号
        /// </summary>
        private string ParentDeptId
        {
            get { return _editModel.ParentId?.ToString(); }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _editModel.ParentId = int.Parse(value);
                }

            }
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            _isLoading = true;
            DrawerInput<int> editInput = this.Options;
            if (editInput.Type.Equals(DrawerInputType.Add))
            {
                _editModel.ParentId = editInput.Id;
            }
            //父级
            deptDatas = await deptService.GetTree();
            _isLoading = false;
        }

    }
}
