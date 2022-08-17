// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Domains;
using System.Collections.Generic;

namespace Gardener.UserCenter.Impl.Domains
{
    /// <summary>
    /// 功能信息-扩展
    /// </summary>
    public class FunctionExtend : Function
    {
        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<ClientFunction> ClientFunctions { get; set; }
    }
}
