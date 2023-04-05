// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Client.Base.Authorization
{
    /// <summary>
    /// 客户端资源
    /// </summary>
    public class ClientResource
    {
        /// <summary>
        /// 资源Key集合
        /// </summary>
        public string[] Keys { get; set; }=new string[0];

        /// <summary>
        /// 并且关系
        /// 默认 true 是 and关系,想使用 or 置为 false
        /// </summary>
        public bool AndCondition { get; set; } = true;
    }
}
