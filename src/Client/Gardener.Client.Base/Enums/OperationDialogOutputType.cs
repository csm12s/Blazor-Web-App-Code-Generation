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

namespace Gardener.Client.Base
{
    /// <summary>
    /// 抽屉返回结果类型
    /// </summary>
    public enum OperationDialogOutputType
    {
        /// <summary>
        /// 成功
        /// </summary>
        Succeeded = 0,
        /// <summary>
        /// 失败
        /// </summary>
        Failed = 1,
        /// <summary>
        /// 取消
        /// </summary>
        Canceled = 2
    }
}
