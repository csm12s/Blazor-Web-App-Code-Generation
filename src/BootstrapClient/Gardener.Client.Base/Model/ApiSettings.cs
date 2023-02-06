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
    /// 如何装配需要看<see cref="ApiSettingExtension"/>
    /// </summary>
    public class ApiSettings
    {

        public String BaseAddres { get { return this.Host + ":" + this.Port+"/"+this.BasePath+"/"; } }
        public String Host { get; set; }
        public String Port { get; set; }
        public String BasePath { get; set; }
        public String UploadPath { get; set; }

    }
}
