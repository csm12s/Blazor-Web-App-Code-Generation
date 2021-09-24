// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Client.Base;
using Gardener.Client.Base.Components;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Client.Pages.DeptView
{
    public partial class Dept : TreeTableBase<DeptDto, int, DeptEdit, EditInput<int?>, EditOutput<int>>
    {
        ITable _table;

        [Inject]
        public IDeptService deptService { get; set; }

        public override EditInput<int?> GetAddOption(DeptDto dto)
        {
            return EditInput<int?>.IsAdd(dto.Id);
        }

        public override EditInput<int?> GetAddOption()
        {
            return EditInput<int?>.IsAdd();
        }

        public override ICollection<DeptDto> GetChildren(DeptDto dto)
        {
            return dto.Children;
        }

        public override EditDrawerSettings GetDrawerSettings()
        {
            return new EditDrawerSettings {Width=800 };
        }

        public override EditInput<int?> GetEditOption(DeptDto dto)
        {
            return EditInput<int?>.IsEdit(dto.Id);
        }

        public override int GetKey(DeptDto dto)
        {
            return dto.Id;
        }

        public override int GetParentKey(DeptDto dto)
        {
            return dto.ParentId.HasValue ? dto.ParentId.Value : 0;
        }

        public override async Task<List<DeptDto>> GetTree()
        {
            return await deptService.GetTree();
        }

        public override void SetChildren(DeptDto dto, ICollection<DeptDto> children)
        {
            dto.Children = children;
        }

        public override void SetIsLocked(DeptDto dto, bool isLocked)
        {
            dto.IsLocked = isLocked;
        }

        public override ICollection<DeptDto> SortChildren(ICollection<DeptDto> children)
        {
            return children.OrderBy(x=>x.Order).ToList();
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        private async Task OnDownloadClick()
        {
            //var result = await drawerService.CreateDialogAsync<RoleResourceDownload, string, bool>(
            //          string.Empty,
            //           true,
            //           title: "种子数据",
            //           width: 1300,
            //           placement: "right");
        }
    }
}
