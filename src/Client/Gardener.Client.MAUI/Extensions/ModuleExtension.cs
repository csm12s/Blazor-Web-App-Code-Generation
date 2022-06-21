// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Client.MAUI.Extensions
{
    public static class ModuleExtension
    {
        private static ClientModuleContext moduleContext;

        public static ClientModuleContext GetModuleContext()
        {
            return moduleContext;
        }

        public static async Task AddModuleLoader(this MauiAppBuilder builder)
        {
            IEnumerable<IConfigurationSection> sections = builder.Configuration.GetSection("ModuleSettings:Dlls").GetChildren();
            List<string> dlls = new List<string> {
            "Gardener.Client.Base.dll",
            "Gardener.Client.Core.dll"
            };
            foreach (IConfigurationSection configuration in sections)
            {
                dlls.Add(configuration.Value);
            }
            dlls = dlls.Distinct().ToList();
            List<Assembly> assemblies = new List<Assembly>();
            foreach (string dll in dlls)
            {
                var rdll = await GetFromFileAsync(dll);
                if(rdll != null)
                    assemblies.Add(rdll);
                System.Console.WriteLine("加载DLL：" + dll);
            }
            moduleContext = new ClientModuleContext
            {
                ModeuleDlls = dlls,
                ModeuleAssemblies = assemblies.ToArray()
            };
            builder.Services.AddScoped(typeof(ClientModuleContext), p => moduleContext);
        }

        private static async Task<Assembly> GetFromFileAsync(string fileName)
        {
            var stream = FileSystem.OpenAppPackageFileAsync(fileName).Result;

            if(stream != null)
            {
                using (stream)
                {
                    byte[] data = new byte[stream.Length];
                    await stream.ReadAsync(data, 0, data.Length);
                    return Assembly.Load(data);
                }
            }
            return null;
        }
    }
}
