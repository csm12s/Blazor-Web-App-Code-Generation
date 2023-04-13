﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.EventBus;
using System.Net;

namespace Gardener.Client.Base
{
    /// <summary>
    /// 授权失败的api调用
    /// </summary>
    /// <remarks>
    /// 此消息标志着httpcode=401
    /// </remarks>
    public class UnauthorizedApiCallEvent : EventBase
    {
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
