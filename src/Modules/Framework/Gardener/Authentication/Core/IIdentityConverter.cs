// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Authentication.Core
{
    /// <summary>
    /// 身份转换器
    /// </summary>
    public interface IIdentityConverter
    {
        /// <summary>
        /// 从请求主体信息解析出身份信息
        /// </summary>
        /// <param name="principal"></param>
        /// <returns></returns>
        Identity? ClaimsPrincipalToIdentity(ClaimsPrincipal principal);
    }
}
