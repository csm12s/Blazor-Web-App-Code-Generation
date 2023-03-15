// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.CodeGeneration.Dtos
{
    /// <summary>
    /// 表字段重命名配置
    /// </summary>
    public class CodeGenReplaceItem
    {
        /// <summary>
        /// OriginText
        /// </summary>
        public string? OriginText { get; set; }
        /// <summary>
        /// ReplacedText
        /// </summary>
        public string? ReplacedText { get; set; }
    }
}
