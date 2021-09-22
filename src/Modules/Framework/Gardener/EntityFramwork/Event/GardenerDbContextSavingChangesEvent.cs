// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;

namespace Gardener.EntityFramwork.Event
{
    public class GardenerDbContextSavingChangesEvent
    {
        public Object Data { get; set; }
    }
}
