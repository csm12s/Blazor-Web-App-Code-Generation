﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface IEventSubscriber
    {

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="e"></param>
        Task CallBack(object e);
    }
}
