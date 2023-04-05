// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;

namespace Gardener.Client.Base
{
    /// <summary>
    /// api的配置
    /// 如何装配需要看<see cref="Gardener.Client.Core.ApiSettingExtension"/>
    /// </summary>
    public class ApiSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public String BaseAddres { get { return this.Host + ":" + this.Port+"/"+this.BasePath+"/"; } }
        /// <summary>
        /// 
        /// </summary>
        public String? Host { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String? Port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String? BasePath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public String? UploadPath { get; set; }

    }
}
