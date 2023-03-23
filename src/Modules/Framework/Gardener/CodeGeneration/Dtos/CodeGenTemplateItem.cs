// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.CodeGeneration.Dtos
{
    /// <summary>
    /// 代码生成模板项
    /// </summary>
    public class CodeGenTemplateItem
    {
        /// <summary>
        /// 代码生成模板项
        /// </summary>
        /// <param name="model"></param>
        public CodeGenTemplateItem(CodeGenNameModel model)
        {
            Model = model;
        }

        /// <summary>
        /// 模板路径
        /// </summary>
        public string TemplatePath { get; set; }
        /// <summary>
        /// 生成路径
        /// </summary>
        public string GenPath { get; set; }
        /// <summary>
        /// 代码生成命名模型
        /// </summary>
        public CodeGenNameModel Model { get; }
    }
}
