// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;
using Gardener.Authentication.Enums;
using System.Collections.Generic;

namespace Gardener.Authentication.Options
{
    public class JWTOptions : IConfigurableOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<IdentityType, JWTSettingsOptions> Settings { get; set; }
    }
}
