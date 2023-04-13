// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Gardener.Client.Core
{
    public static class AssemblyHelper
    {
        private static List<Assembly> _loadedAssemblies = new List<Assembly>();

        public static List<Assembly> GetLoadedAssemblies(params string[] scanAssembliesStartsWith)
        {
            if (_loadedAssemblies.Any())
            {
                return _loadedAssemblies;
            }

            LoadAssemblies(scanAssembliesStartsWith);
            return _loadedAssemblies;
        }

        [SuppressMessage("Maintainability", "CA1508:Avoid dead conditional code", Justification = "Wrong analysis")]
        private static void LoadAssemblies(params string[] scanAssembliesStartsWith)
        {
            HashSet<Assembly> loadedAssemblies = new HashSet<Assembly>();

            List<string> assembliesToBeLoaded = new List<string>();

            string appDllsDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (scanAssembliesStartsWith?.Any() == true)
            {
                if (scanAssembliesStartsWith.Length == 1)
                {
                    string searchPattern = $"{scanAssembliesStartsWith.First()}*.dll";
                    string[] assemblyPaths = Directory.GetFiles(appDllsDirectory, searchPattern, SearchOption.AllDirectories);
                    assembliesToBeLoaded.AddRange(assemblyPaths);
                }

                if (scanAssembliesStartsWith.Length > 1)
                {
                    foreach (string starsWith in scanAssembliesStartsWith)
                    {
                        string searchPattern = $"{starsWith}*.dll";
                        string[] assemblyPaths = Directory.GetFiles(appDllsDirectory, searchPattern, SearchOption.AllDirectories);
                        assembliesToBeLoaded.AddRange(assemblyPaths);
                    }
                }
            }
            else
            {
                string[] assemblyPaths = Directory.GetFiles(appDllsDirectory, "*.dll");
                assembliesToBeLoaded.AddRange(assemblyPaths);
            }

            foreach (string path in assembliesToBeLoaded)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(path);
                    loadedAssemblies.Add(assembly);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            _loadedAssemblies = loadedAssemblies.ToList();
        }
    }
}
