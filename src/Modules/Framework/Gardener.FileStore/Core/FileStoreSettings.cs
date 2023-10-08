// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.ConfigurableOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gardener.FileStore.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class FileStoreSettings : IConfigurableOptions
    {
        /// <summary>
        /// 默认服务类型
        /// </summary>
        public string DefaultFileStoreService { get; set; } = null!;
        /// <summary>
        /// 服务配置集合
        /// </summary>
        public List<Dictionary<string,object>> Services { get; set; }=new ();
    }
}
