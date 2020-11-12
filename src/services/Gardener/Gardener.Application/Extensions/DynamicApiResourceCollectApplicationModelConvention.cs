// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DataEncryption;
using Fur.DependencyInjection;
using Gardener.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Linq;

namespace Gardener.Application.Extensions
{
    /// <summary>
    /// 收集pi资源
    /// </summary>
    [SkipScan]
    public class DynamicApiResourceCollectApplicationModelConvention : IApplicationModelConvention
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="application"></param>
        public void Apply(ApplicationModel application)
        {
            var controllers = application.Controllers;
            foreach (var controller in controllers)
            {
                // 判断是否处理 Mvc控制器
                if (!typeof(ControllerBase).IsAssignableFrom(controller.ControllerType))
                {
                    foreach (var action in controller.Actions)
                    {
                        Resource resource=  new Resource()
                        {
                            CreatedTime = DateTimeOffset.Now
                        };

                        var apiSecurityDefine = action.Attributes.FirstOrDefault(u => u is ApiSecurityDefineAttribute) as ApiSecurityDefineAttribute;
                        if (apiSecurityDefine != null && apiSecurityDefine.ResourceId != null)
                        {
                            resource.UniqueName= apiSecurityDefine.ResourceId;
                            resource.Name = apiSecurityDefine.ApiName;
                            resource.Remark = apiSecurityDefine.ApiName;
                        }
                        else
                        {
                            string method = action.DisplayName;
                            string parameters=string.Join('-', action.Parameters.Select(x => x.ParameterType.FullName + "_" + x.Name).ToArray());
                            if (parameters is not null and not "")
                            {
                                method += "-" + parameters;
                            }
                            string resouseId = MD5Encryption.Encrypt(method);
                            resource.UniqueName = resouseId;
                            resource.Name = action.DisplayName;
                            resource.Remark = method;
                        }
                        MyApplicationContext.AddApiResource(resource);
                    }
                }
            }

        }
    }
}
