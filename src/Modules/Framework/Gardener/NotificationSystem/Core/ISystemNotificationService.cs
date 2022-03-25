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
        /// <returns></returns>
        Task SendToAllClient<TData>(NotificationDataType dataType, TData data) where TData : NotificationDataBase;

        /// <summary>
        /// 向所有客户端发送信息
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="dataType"></param>
        /// <param name="data"></param>
        /// <param name="Identity"></param>
        /// <returns></returns>
        Task SendToAllClient<TData>(NotificationDataType dataType, TData data, Identity Identity) where TData: NotificationDataBase;

        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="userId"></param>
        /// <param name="dataType"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        Task SendToUser<TData>(int userId, NotificationDataType dataType, TData data) where TData : NotificationDataBase;

        /// <summary>
        /// 向指定用户发送信息
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="userId"></param>
        /// <param name="dataType"></param>
        /// <param name="data"></param>
        /// <param name="Identity"></param>
        /// <returns></returns>
        Task SendToUser<TData>(int userId, NotificationDataType dataType, TData data, Identity Identity) where TData : NotificationDataBase;
    }
}
