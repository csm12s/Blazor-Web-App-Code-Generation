// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Shared
{
    public partial class MainLayout
    {
        bool collapsed;

        void toggle()
        {
            collapsed = !collapsed;
        }
        void OnCollapse(bool isCollapsed)
        {
            Console.WriteLine($"Collapsed: {isCollapsed}");
        }
    }
}
