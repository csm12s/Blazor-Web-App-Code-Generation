// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using Gardener.Base.Entity;
using Microsoft.EntityFrameworkCore;
using HttpMethod = Gardener.Enums.HttpMethod;
namespace Gardener.WoChat.Impl.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class WoChatImFunctionSeedData : IEntitySeedData<Function>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Function> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
                new Function() {Group="通知系统服务",Service="Im聊天服务",Summary="根据会话编号获取会话",Key="B064921D09886F3BD49621B6C2A81ACC",Description="{imsessionid}",Path="/api/wo-chat-im/im-session/{imsessionid}",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("ea2a64ac-bde9-45e5-a879-10b161b3f825"),IsLocked=false,IsDeleted=false,CreateBy="4",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-20 15:54:03"),},
                new Function() {Group="通知系统服务",Service="Im聊天服务",Summary="开启会话消息发送权限",Key="ED0E0A2262D113044CA7B4539AE1B1EA",Description="{imsessionid}",Path="/api/wo-chat-im/enable-session-send-message",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("97b24035-cd96-4e28-bc42-103ac7e5fe3f"),IsLocked=false,IsDeleted=false,CreateBy="4",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-20 15:54:02"),},
                new Function() {Group="通知系统服务",Service="Im聊天服务",Summary="关闭会话消息发送权限",Key="DE3E8E8CE4BC746D5DF73DA399E0D30D",Description="disable-session-send-message",Path="/api/wo-chat-im/disable-session-send-message",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("27d997d7-e691-4dbe-b5e0-74acbca53d98"),IsLocked=false,IsDeleted=false,CreateBy="4",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-20 15:54:02"),},
                new Function() {Group="通知系统服务",Service="Im聊天服务",Summary="发送消息到会话",Key="4AB01742CF4A20C9238B7BA892732792",Description="send-message",Path="/api/wo-chat-im/send-message",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("333a3802-f8d2-4625-9476-dee8bf43fd0d"),IsLocked=false,IsDeleted=false,CreateBy="4",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-20 15:54:01"),},
                new Function() {Group="通知系统服务",Service="Im聊天服务",Summary="退出会话",Key="9614EDD6363C8F365B3728C29B22CF50",Description="私聊隐藏，群聊自己创建的话直接解散，不是就退出",Path="/api/wo-chat-im/quit-my-im-session",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("42636f5d-d5f6-4f64-bd2b-e77d80e51ff2"),IsLocked=false,IsDeleted=false,CreateBy="4",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-20 15:54:01"),},
                new Function() {Group="通知系统服务",Service="Im聊天服务",Summary="获取会话消息列表",Key="4CC387634C5025C3FEE188125B33C9D2",Description="my-session-messages",Path="/api/wo-chat-im/my-session-messages",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("1adcacf3-33ae-4b36-b5c9-dcd95151ef3a"),IsLocked=false,IsDeleted=false,CreateBy="4",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-20 15:54:00"),},
                new Function() {Group="通知系统服务",Service="Im聊天服务",Summary="获取我的会话列表",Key="317257C6321887B9223ECEF8D1BA8100",Description="my-im-sessions",Path="/api/wo-chat-im/my-im-sessions",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("d481028f-d67e-4ee0-b237-a4883c618486"),IsLocked=false,IsDeleted=false,CreateBy="4",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-20 15:54:00"),},
                new Function() {Group="通知系统服务",Service="Im聊天服务",Summary="获取会话列表",Key="5DC2499F878F522E4C0DD78321F444DE",Description="im-group-sessions",Path="/api/wo-chat-im/im-group-sessions",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("752b1623-8898-431f-ab3f-db9ebffae4e6"),IsLocked=false,IsDeleted=false,CreateBy="4",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-20 15:53:59"),},
                new Function() {Group="通知系统服务",Service="Im聊天服务",Summary="添加会话",Key="925B4B0A0CD782790D2A5F3EDA5A3C25",Description="my-im-session",Path="/api/wo-chat-im/my-im-session",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("f78ea06a-4c55-4445-9e16-bdc92c9b9fa6"),IsLocked=false,IsDeleted=false,CreateBy="4",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-20 15:53:59"),},
         };
        }
    }

}
