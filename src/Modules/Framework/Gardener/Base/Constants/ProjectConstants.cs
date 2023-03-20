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
    /// 项目常量
    /// </summary>
    public class ProjectConstants
    {
        /// <summary>
        /// 项目名
        /// Gardener
        /// </summary>
        public const string AppName = "Gardener";
        /// <summary>
        /// 输出exe的文件夹
        /// </summary>
        public static string ExeFolder = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        /// <summary>
        /// 项目文件夹
        /// </summary>
        public static string ProjectFolder = ExeFolder;
        /// <summary>
        /// 代码生成路径
        /// </summary>
        public static string CodeGenPath = Path.Combine(ProjectFolder, "codeGen");
    }
}
