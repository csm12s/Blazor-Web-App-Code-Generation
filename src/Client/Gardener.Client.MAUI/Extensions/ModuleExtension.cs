// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Gardener.Client.MAUI.Extensions
{
    public static class ModuleExtension
    {
        private static ClientModuleContext moduleContext;

        public static ClientModuleContext GetModuleContext()
        {
            return moduleContext;
        }

        public static void AddModuleLoader(this MauiAppBuilder builder)
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
                var rdll =  GetFromFile(dll);
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

        private static Assembly GetFromFile(string fileName)
        {
            var ss = Environment.ProcessPath.Replace("Gardener.Client.MAUI.exe", "");
            
            try
            {
                return Assembly.LoadFile(ss + fileName);
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
    }
}
