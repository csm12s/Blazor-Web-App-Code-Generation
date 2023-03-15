// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Client.Base
{
    /// <summary>
    /// 系统配置
    /// </summary>
    public class SystemConfig
    {
        /// <summary>
        /// Copyright内容
        /// </summary>
        public string Copyright { get; set; } = null!;
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; } = null!;
        /// <summary>
        /// 系统描述
        /// </summary>
        public string SystemDescription { get; set; } = null!;
    }
}
