// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.EasyJob.Resources
{
    /// <summary>
    /// EasyJob 本地化资源
    /// </summary>
    public class EasyJobLocalResource : SharedLocalResource
    {
        /// <summary>
        /// 任务编号
        /// </summary>
        public const string JobId= nameof(JobId);
    }
}
