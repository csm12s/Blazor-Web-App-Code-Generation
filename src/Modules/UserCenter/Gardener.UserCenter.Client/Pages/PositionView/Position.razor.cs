// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base.Components;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.PositionView
{
    public partial class Position : TableBase<PositionDto, int>
    {
        [Inject]
        IPositionService positionService { get; set; }
       
        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="roleDto"></param>
        private async Task OnEditClick(int id)
        {
            var result = await drawerService.CreateDialogAsync<PositionEdit, int, bool>(id, true, title: "编辑", width: 500);

            if (result)
            {
                //刷新列表
                await ReLoadTable();
            }
        }
        /// <summary>
        /// 点击添加按钮
        /// </summary>
        private async Task OnAddClick()
        {
            var result = await drawerService.CreateDialogAsync<PositionEdit, int, bool>(0, true, title: "添加", width: 500);

            if (result)
            {
                //刷新列表
                pageRequest.PageIndex = 1;
                await ReLoadTable();
            }
        }
    }
}
