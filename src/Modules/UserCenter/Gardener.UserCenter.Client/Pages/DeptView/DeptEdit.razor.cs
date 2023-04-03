// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.DeptView
{
    public partial class DeptEdit : EditOperationDialogBase<DeptDto, int>
    {
        [Inject]
        IDeptService deptService { get; set; } = null!;

        //部门树
        List<DeptDto> deptDatas = new List<DeptDto>();
        /// <summary>
        /// 父级部门编号
        /// </summary>
        private string ParentDeptId
        {
            get { return _editModel.ParentId?.ToString() ?? string.Empty; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _editModel.ParentId = int.Parse(value);
                }
                else
                {
                    _editModel.ParentId = null;
                }

            }
        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            base.StartLoading();
            await base.LoadEditModelData();
            //父级
            deptDatas = await deptService.GetTree();
            OperationDialogInput<int> editInput = this.Options;
            if (editInput.Type.Equals(DrawerInputType.Add))
            {
                _editModel.ParentId = editInput.Id==0?null: editInput.Id;
            }
            base.StopLoading();
        }

    }
}
