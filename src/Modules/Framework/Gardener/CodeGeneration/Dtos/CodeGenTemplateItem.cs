// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.CodeGeneration.Dtos
{
    /// <summary>
    /// CodeGenTemplateItem
    /// </summary>
    public class CodeGenTemplateItem
    {
        /// <summary>
        /// CodeGenTemplateItem
        /// </summary>
        /// <param name="model"></param>
        public CodeGenTemplateItem(CodeGenNameModel model)
        {
            Model = model;
        }
        /// <summary>
        /// TemplatePath
        /// </summary>
        public string? TemplatePath { get; set; }
        /// <summary>
        /// GenPath
        /// </summary>
        public string? GenPath { get; set; }
        /// <summary>
        /// Model
        /// </summary>
        public CodeGenNameModel Model { get; }
    }
}
