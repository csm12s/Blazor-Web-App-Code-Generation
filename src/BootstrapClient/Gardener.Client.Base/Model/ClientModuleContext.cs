// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection;

namespace Gardener.Client.Base
{
    public class ClientModuleContext
    {
        public List<string> ModeuleDlls { get; set; }

        public Assembly[] ModeuleAssemblies { get; set; }
    }
}
