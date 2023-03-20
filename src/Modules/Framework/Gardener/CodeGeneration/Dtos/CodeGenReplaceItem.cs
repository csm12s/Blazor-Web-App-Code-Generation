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
        /// 默认前缀
        /// </summary>
        public string OriginText { get; set; }//SYS_

        /// <summary>
        /// 替换前缀
        /// </summary>
        public string ReplacedText { get; set; }//Sys
    }
}
