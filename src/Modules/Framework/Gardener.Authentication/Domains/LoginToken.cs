// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Attributes;
using Gardener.Authentication.Dtos;
using Gardener.Base.Entity;

namespace Gardener.Authentication.Domains
{
    /// <summary>
    /// 登录Token信息
    /// </summary>
    [IgnoreAudit]
    public class LoginToken : LoginTokenDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
    }
}
