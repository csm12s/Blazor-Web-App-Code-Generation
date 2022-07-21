// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.TaskScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.SysTimer.Impl.Demo
{
    /// <summary>
    /// 
    /// </summary>
    public class DomeWorker : ISpareTimeWorker
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timer"></param>
        /// <param name="count"></param>
        [SpareTime(100)]
        public void DoSomething(SpareTimer timer, long count) 
        { 
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")); 
        }
    }
}
