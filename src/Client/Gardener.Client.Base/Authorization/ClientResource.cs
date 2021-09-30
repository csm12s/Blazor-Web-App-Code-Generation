// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Client.Base.Authorization
{
    public class ClientResource
    {
        public string[] Keys { get; set; }

        /// <summary>
        /// 并且关系
        /// 默认 true 是 and关系,想使用 or 置为 false
        /// </summary>
        public bool AndCondition { get; set; } = true;
    }
}
