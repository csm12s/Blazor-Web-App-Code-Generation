// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.EasyJob.Dtos
{
    /// <summary>
    /// Job操作参数
    /// </summary>
    public class JobDetailInput
    {
        /// <summary>
        /// 任务编号
        /// </summary>
        public string JobId { get; set; } = null!;
    }
}
