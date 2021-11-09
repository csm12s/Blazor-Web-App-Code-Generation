// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Email.Dtos;
using Gardener.Email.Enums;
using System;
using System.Threading.Tasks;

namespace Gardener.Email.Services
{
    /// <summary>
    /// 邮件服务
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> Send(SendEmailInputDto input);
    }
}
