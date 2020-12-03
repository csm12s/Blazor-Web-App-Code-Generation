// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Furion.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

namespace Gardener.Core
{
    /// <summary>
    /// 
    /// </summary>
    [SkipScan]
    public class AuthorizeControllerApplicationModelConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            var controllers = application.Controllers.Where(u => !typeof(Controller).IsAssignableFrom(u.ControllerType));
            foreach (var controller in controllers)
            {
                var actions = controller.Actions;
                foreach (var action in actions)
                {
                    // 添加规范化结果特性
                    action.Filters.Add(new ApiAuthorizeAttribute("api-auth"));
                }

            }
        }
    }
}
