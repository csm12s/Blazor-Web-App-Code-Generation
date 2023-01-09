// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.IO;

namespace Gardener.Base
{
    public class ProjectConstants
    {
        /// <summary>
        /// Gardener
        /// </summary>
        public const string AppName = "Gardener";
        public static string ExeFolder = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        public static string ProjectFolder = ExeFolder;
        public static string CodeGenPath = Path.Combine(ProjectFolder, "codeGen");
    }
}
