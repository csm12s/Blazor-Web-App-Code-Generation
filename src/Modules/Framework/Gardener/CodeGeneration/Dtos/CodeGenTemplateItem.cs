// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.CodeGeneration.Dtos
{
    public class CodeGenTemplateItem
    {
        public CodeGenTemplateItem(CodeGenNameModel model)
        {
            Model = model;
        }

        public string TemplatePath { get; set; }
        public string GenPath { get; set; }
        public CodeGenNameModel Model { get; }
    }
}
