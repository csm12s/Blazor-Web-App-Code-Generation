// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Gardener.Client.Core
{
    /// <summary>
    /// 模块扩展
    /// </summary>
    /// <remarks>
    /// 解决Client无法扫描到所有引用包的问题
    /// </remarks>
    public static class ModuleExtension
    {
        /// <summary>
        /// client 所有模块Assembly
        /// </summary>
        private static ClientModuleContext? moduleContext;

        /// <summary>
        /// 获取 client 所有模块Assembly
        /// </summary>
        /// <returns></returns>
        public static ClientModuleContext? GetModuleContext()
        {
            return moduleContext;
        }
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="builder"></param>
        public static void AddModuleLoader(this WebAssemblyHostBuilder builder)
        {
            IEnumerable<IConfigurationSection> sections = builder.Configuration.GetSection("ModuleSettings:Dlls").GetChildren();
            List<string> dlls = new List<string> {
            "Gardener.Client.Base.dll",
            "Gardener.Client.Core.dll"
            };
            foreach (IConfigurationSection configuration in sections)
            {
                if (string.IsNullOrEmpty(configuration.Value)) 
                {
                    continue;
                }
                dlls.Add(configuration.Value);
            }
            dlls = dlls.Distinct().ToList();
            List<Assembly> assemblies = new List<Assembly>();
            foreach (string dll in dlls)
            {
                assemblies.Add(Assembly.LoadFrom(dll));
                System.Console.WriteLine("加载DLL：" + dll);
            }
            moduleContext = new ClientModuleContext
            {
                ModeuleDlls = dlls,
                ModeuleAssemblies = assemblies.ToArray()
            };
            builder.Services.AddScoped(typeof(ClientModuleContext), p => moduleContext);
        }
    }
}
