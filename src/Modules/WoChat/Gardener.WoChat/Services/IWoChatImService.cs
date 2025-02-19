﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Dtos;
using Gardener.WoChat.Dtos;

namespace Gardener.WoChat.Services
{
    /// <summary>
    /// Im聊天服务
    /// </summary>
    public interface IWoChatImService
    {
        /// <summary>
        /// 获取IM群组会话列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ImSessionDto>> GetImGroupSessions();

        /// <summary>
        /// 获取我的会话列表
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ImSessionDto>> GetMyImSessions();

        /// <summary>
        /// 获取会话消息列表
        /// </summary>
        /// <param name="imSessionId"></param>
        /// <param name="maxDateTime"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IEnumerable<ImSessionMessageDto>> GetMySessionMessages(Guid imSessionId,DateTimeOffset? maxDateTime=null,int pageSize=100);

        /// <summary>
        /// 发送消息到会话
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task<bool> SendMessage(ImSessionMessageDto message);

        /// <summary>
        /// 发送消息到会话
        /// </summary>
        /// <param name="message"></param>
        /// <param name="identity"></param>
        /// <returns></returns>
        Task<bool> SendMessage(ImSessionMessageDto message, Identity identity);

        /// <summary>
        /// 添加会话
        /// </summary>
        /// <param name="input"></param>
        /// <returns>会话编号</returns>
        Task<Guid?> AddMyImSession(ImSessionAddInput input);

        /// <summary>
        /// 退出会话
        /// </summary>
        /// <param name="imSessionId"></param>
        /// <returns></returns>
        /// <remarks>
        /// 私聊隐藏，群聊自己创建的话直接解散，不是就退出
        /// </remarks>
        Task<bool> QuitMyImSession(Guid imSessionId);

        /// <summary>
        /// 禁言会话
        /// </summary>
        /// <param name="imSessionId"></param>
        /// <returns></returns>
        Task<bool> DisableSessionSendMessage(Guid imSessionId);

        /// <summary>
        /// 解除会话禁言
        /// </summary>
        /// <param name="imSessionId"></param>
        /// <returns></returns>
        Task<bool> EnableSessionSendMessage(Guid imSessionId);

        /// <summary>
        /// 根据会话编号获取会话
        /// </summary>
        /// <param name="imSessionId">会话编号</param>
        /// <returns></returns>
        Task<ImSessionDto?> GetImSession(Guid imSessionId);
    }
}
