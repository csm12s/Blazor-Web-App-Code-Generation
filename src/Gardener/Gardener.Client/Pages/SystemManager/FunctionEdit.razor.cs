// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Gardener.Enums;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Gardener.Client.Pages.SystemManager
{
    public partial class FunctionEdit: DrawerTemplate<Guid, bool>
    {
        private bool _isLoading = false;
        private FunctionDto _editModel = new FunctionDto();
        [Required(ErrorMessage ="不能为空")]
        private string _currentEditModelHttpMethodType
        {
            get
            {
                return _editModel.Method.ToString();
            }
            set
            {
                _editModel.Method = (HttpMethodType)Enum.Parse(typeof(HttpMethodType), value);
            }
        }
        [Inject]
        MessageService messageService { get; set; }
        [Inject]
        IFunctionService functionService { get; set; }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _isLoading = true;

            Guid id = this.Options;

            if (!Guid.Empty.Equals(id))
            {
                //更新 回填数据
                var model = await functionService.Get(id);
                if (model != null)
                {
                    //赋值给编辑对象
                    model.Adapt(_editModel);
                }
                else
                {
                    messageService.Error("数据已不存在");
                }
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
            if (Guid.Empty.Equals(_editModel.Id))
            {
                //添加
                var result = await functionService.Insert(_editModel);

                if (result != null)
                {
                    messageService.Success("添加成功");
                    await base.CloseAsync(true);
                }
                else
                {
                    messageService.Error("添加失败");
                }
            }
            else
            {
                //修改
                var result = await functionService.Update(_editModel);
                if (result)
                {
                    messageService.Success("修改成功");
                    await base.CloseAsync(true);
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
            await base.CloseAsync(false);
        }
    }
}
