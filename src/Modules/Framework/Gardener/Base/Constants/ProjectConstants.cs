// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.IO;

namespace Gardener.Base
{
    /// <summary>
    /// ProjectConstants
    /// </summary>
    public class ProjectConstants
    {
        /// <summary>
        /// Gardener
        /// </summary>
        public const string AppName = "Gardener";
        /// <summary>
        /// ExeFolder
        /// </summary>
        public static string? ExeFolder = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        /// <summary>
        /// ProjectFolder
        /// </summary>
        public static string ProjectFolder = ExeFolder==null?string.Empty:ExeFolder;
        /// <summary>
        /// CodeGenPath
        /// </summary>
        public static string CodeGenPath = Path.Combine(ProjectFolder, "codeGen");
    }
}
