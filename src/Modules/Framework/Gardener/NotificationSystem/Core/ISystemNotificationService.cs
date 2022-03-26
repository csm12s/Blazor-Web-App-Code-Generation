// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Dtos;
using Gardener.NotificationSystem.Dtos;
using Gardener.NotificationSystem.Enums;
using System.Threading.Tasks;

namespace Gardener.NotificationSystem.Core
{
    /// <summary>
    /// 系统通知服务
    /// </summary>
    public interface ISystemNotificationService
    {

        /// <summary>
        /// 向所有客户端发送信息
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="dataType"></param>
        /// <param name="data"></param>
        /// <param name="Identity"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        Task SendToAllClient<TData>(NotificationDataType dataType, TData data, Identity Identity = null, string ip = null) where TData : NotificationDataBase;
        // <summary>
        /// 向所有客户端发送信息
        /// </summary>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToAllClient(NotificationData notifyData);
        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="userId"></param>
        /// <param name="dataType"></param>
        /// <param name="data"></param>
        /// <param name="Identity"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        Task SendToUser<TData>(int userId, NotificationDataType dataType, TData data, Identity Identity = null, string ip = null) where TData : NotificationDataBase;
        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notifyData"></param>
        /// <returns></returns>
        Task SendToUser(int userId, NotificationData notifyData);
    }
}
