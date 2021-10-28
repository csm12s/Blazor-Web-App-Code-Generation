// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using Gardener.Email.Dtos;


namespace Gardener.Email.Services
{
    /// <summary>
    /// 邮件服务器配置服务
    /// </summary>
    public interface IEmailServerConfigService : IServiceBase<EmailServerConfigDto,int>
    {

    }
}
