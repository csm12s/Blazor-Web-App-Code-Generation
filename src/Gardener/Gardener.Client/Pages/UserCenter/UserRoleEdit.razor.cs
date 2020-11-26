// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Application.Dtos;
using System.Threading.Tasks;

namespace Gardener.Client.Pages.UserCenter
{
    public partial class UserRoleEdit : DrawerTemplate<int, int>
    {
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }
    }
}
