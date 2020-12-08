// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class AuthKeyConstants
    {
        public static readonly string ClientIdKeyName = "clientId";
        public static readonly string ClientTypeKeyName = "clientType";
        public static readonly string UserIsSuperAdministratorKey = "IsSuperAdministrator";
    }
}
