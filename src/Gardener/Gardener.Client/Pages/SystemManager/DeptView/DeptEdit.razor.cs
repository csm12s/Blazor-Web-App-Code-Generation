// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Pages.SystemManager.DeptView
{
    public partial class DeptEdit:FeedbackComponent<int, bool>
    {
        [Inject]
        MessageService messageService { get; set; }
        [Inject]
        IDeptService deptService { get; set; }

        private bool _isLoading = false;

        private DeptDto _editModel=null;

        /// <summary>
        /// 父级选择数据
        /// </summary>
        private List<CascaderNode> _resourceCascaderNodes;
        /// <summary>
        /// 选择器绑定值
        /// </summary>
        private string _resourceCascaderValue = String.Empty;
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;

            int id = this.Options;

            if (id > 0)
            {
                //更新 回填数据
                var model = await deptService.Get(id);
                if (model != null)
                {
                    //赋值给编辑对象
                    _editModel = model;
                }
                else
                {
                    messageService.Error("数据已不存在");
                }
            }
            else 
            {
                _editModel = new DeptDto();
            }
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
            //开始请求
            if (_editModel.Id<=0)
            {
                //添加
                var result = await deptService.Insert(_editModel);

                if (result != null)
                {
                    messageService.Success("添加成功");
                    await (base.FeedbackRef as DrawerRef<bool>)!.CloseAsync(true);
                }
                else
                {
                    messageService.Error("添加失败");
                }
            }
            else
            {
                //修改
                var result = await deptService.Update(_editModel);
                if (result)
                {
                    messageService.Success("修改成功");
                    await (base.FeedbackRef as DrawerRef<bool>)!.CloseAsync(true);
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
            await (base.FeedbackRef as DrawerRef<bool>)!.CloseAsync(false);
        }
    }
}
