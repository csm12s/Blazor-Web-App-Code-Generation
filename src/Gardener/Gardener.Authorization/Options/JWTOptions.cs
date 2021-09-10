// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;
using Gardener.Authorization.Enums;
using System.Collections.Generic;

namespace Gardener.Authorization.Options
{
    public class JWTOptions : JWTSettingsOptions,IConfigurableOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<IdentityType, JWTSettingsOptions> Settings { get; set; }
    }
}
