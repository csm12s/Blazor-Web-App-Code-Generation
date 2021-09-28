// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.DeptView
{
    public partial class DeptEdit:FeedbackComponent<EditInput<int?>, EditOutput<int>>
    {
        [Inject]
        MessageService messageService { get; set; }
        [Inject]
        IDeptService deptService { get; set; }

        private bool _isLoading = false;

        private DeptDto _editModel=null;

        //部门树
        List<DeptDto> deptDatas;
        private string deptId = null;
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;

            EditInput<int?> editInput = this.Options;

            if (editInput.Type.Equals(EditInputType.Edit))
            {
                //更新 回填数据
                var model = await deptService.Get(editInput.Id ?? 0);
                if (model != null)
                {
                    //赋值给编辑对象
                    _editModel = model;
                   
                }
                else
                {
                    messageService.Error("数据已不存在");
                    await base.FeedbackRef.CloseAsync(EditOutput<int>.Fail());
                }
            }
            else if (editInput.Type.Equals(EditInputType.Add))
            {
                _editModel = new DeptDto();
                _editModel.ParentId = editInput.Id;
            }
            deptId = _editModel.ParentId.ToString();
            //父级
            deptDatas = await deptService.GetTree();
            _isLoading = false;
            await base.OnInitializedAsync();
        }

        /// <summary>
        /// 表单完成时
        /// </summary>
        /// <param name="editContext"></param>
        /// <returns></returns>
        private async Task OnFormFinish(EditContext editContext)
        {
            _isLoading = true;
            _editModel.ParentId = int.Parse(deptId);
            //开始请求
            if (this.Options.Type.Equals(EditInputType.Add))
            {
                //添加
                var result = await deptService.Insert(_editModel);

                if (result != null)
                {
                    messageService.Success("添加成功");
                    await base.FeedbackRef.CloseAsync(EditOutput<int>.Succeed(result.Id));
                }
                else
                {
                    messageService.Error("添加失败");
                }
            }
            else if (this.Options.Type.Equals(EditInputType.Edit))
            {
                //修改
                var result = await deptService.Update(_editModel);
                if (result)
                {
                    messageService.Success("修改成功");
                    await base.FeedbackRef.CloseAsync(EditOutput<int>.Succeed(_editModel.Id));
                }
                else
                {
                    messageService.Error("修改失败");
                }
            }
            _isLoading = false;
        }
        /// <summary>
        /// 取消
        /// </summary>
        private async Task OnFormCancel()
        {
            await base.FeedbackRef.CloseAsync(EditOutput<int>.Cancel());
        }

    }
}
