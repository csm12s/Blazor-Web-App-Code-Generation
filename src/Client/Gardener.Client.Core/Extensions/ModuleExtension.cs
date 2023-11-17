// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Gardener.Client.Base.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

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
        private static ClientModuleManager? clientModuleManager;

        /// <summary>
        /// 获取 client 所有模块Assembly
        /// </summary>
        /// <returns></returns>
        public static ClientModuleManager? GetModuleContext()
        {
            return clientModuleManager;
        }
        /// <summary>
        /// 加载各个dll，并扫描需要注册的服务
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterModulesAndScanServices(this IServiceCollection services)
        {
            //加载各个dll
            services.RegisterModules();
            //扫描需要注册的服务
            services.AddServicesWithAttributeOfType(AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary>
        /// 加载各个dll
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterModules(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            IEnumerable<IConfigurationSection> sections = configuration.GetSection("ModuleSettings:Dlls").GetChildren();
            List<string> dlls = new();
            foreach (IConfigurationSection item in sections)
            {
                if (string.IsNullOrEmpty(item.Value))
                {
                    continue;
                }
                dlls.Add(item.Value);
            }
            dlls = dlls.Distinct().ToList();

            clientModuleManager = new ClientModuleManager();

            foreach (string dll in dlls)
            {
                clientModuleManager.Add(Assembly.LoadFrom(dll));
                System.Console.WriteLine("加载DLL：" + dll);
            }
            services.AddScoped(typeof(ClientModuleManager), p => clientModuleManager);
        }

        /// <summary>
        /// 加载模块
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static Task LoadModules(this IServiceProvider serviceProvider)
        {
            RootComponentMappingCollection rootComponents=serviceProvider.GetRequiredService<RootComponentMappingCollection>();
            IEnumerable<IModule> modules= serviceProvider.GetServices<IModule>();
            List<Task> tasks = new List<Task>();
            foreach (IModule module in modules)
            {
                clientModuleManager?.Add(module);

                foreach (var component in module.GetAutoRegisterComponents())
                {
                    rootComponents.Add(component.Component, component.Selector);
                }
                tasks.Add(module.Load());
            }
            return Task.WhenAll(tasks.ToArray());
        }
    }
}
