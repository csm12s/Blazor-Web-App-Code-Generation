// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.WoChat.Dtos;

namespace Gardener.WoChat.Services
{
    /// <summary>
    /// Im聊天服务
    /// </summary>
    public interface IImService
    {
        /// <summary>
        /// 获取会话列表
        /// </summary>
        /// <returns></returns>
        Task<List<ImSessionDto>> GetImSessions();

        /// <summary>
        /// 获取我的会话列表
        /// </summary>
        /// <returns></returns>
        Task<List<ImSessionDto>> GetMyImSessions();

        /// <summary>
        /// 获取会话消息列表
        /// </summary>
        /// <param name="imSessionId"></param>
        /// <param name="maxDateTime"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<ImSessionMessageDto>> GetSessionMessages(Guid imSessionId,DateTimeOffset? maxDateTime,int? pageSize=100);

        /// <summary>
        /// 发送消息到会话
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<bool> SendMessage(ImSessionMessageDto message);

        /// <summary>
        /// 添加会话
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> AddImSession(ImSessionAddInput input);

        /// <summary>
        /// 移除会话
        /// </summary>
        /// <param name="imSessionId"></param>
        /// <returns></returns>
        /// <remarks>
        /// 私聊直接解散，群里就退出
        /// </remarks>
        Task<bool> RemoveImSession(Guid imSessionId);

    }
}
