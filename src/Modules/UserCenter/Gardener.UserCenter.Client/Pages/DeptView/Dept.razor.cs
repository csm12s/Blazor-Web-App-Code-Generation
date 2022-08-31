// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

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
    public partial class Dept : TreeTableBase<DeptDto, int, DeptEdit>
    {

        protected override OperationDialogSettings GetOperationDialogSettings()
        {
            OperationDialogSettings dialogSettings = base.GetOperationDialogSettings();
            dialogSettings.Width = 1000;
            return dialogSettings;
        }

        [Inject]
        public IDeptService deptService { get; set; }

        protected override ICollection<DeptDto> GetChildren(DeptDto dto)
        {
            return dto.Children;
        }


        protected override int GetParentKey(DeptDto dto)
        {
            return dto.ParentId.HasValue ? dto.ParentId.Value : 0;
        }

        protected override async Task<List<DeptDto>> GetTree()
        {
            return await deptService.GetTree(true);
        }

        protected override void SetChildren(DeptDto dto, ICollection<DeptDto> children)
        {
            dto.Children = children;
        }


        protected override ICollection<DeptDto> SortChildren(ICollection<DeptDto> children)
        {
            return children.OrderBy(x=>x.Order).ToList();
        }
    }
}
