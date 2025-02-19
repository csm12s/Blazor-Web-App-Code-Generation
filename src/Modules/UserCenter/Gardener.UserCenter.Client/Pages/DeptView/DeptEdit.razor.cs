﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Client.Base;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Resources;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.DeptView
{
    public partial class DeptEdit : EditOperationDialogBase<DeptDto, int, UserCenterResource>
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
            await base.StartLoading();
            var task1 = deptService.GetTree();
            await base.OnInitializedAsync();
            deptDatas = await task1;
            //父级
            OperationDialogInput<int> editInput = this.Options;
            if (editInput.Type.Equals(OperationDialogInputType.Add))
            {
                _editModel.ParentId = editInput.Data == 0 ? null : editInput.Data;
            }
            await base.StopLoading(true);
        }

    }
}
