// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Client.Core
{
    /// <summary>
    /// 授权失败的api调用
    /// </summary>
    public class UnauthorizedApiCallEvent : EventBase
    {
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
