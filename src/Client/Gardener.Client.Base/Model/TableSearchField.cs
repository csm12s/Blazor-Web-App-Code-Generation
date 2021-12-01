// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Gardener.Client.Base
{
    public class TableSearchField
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public Type Type { get; set; }

        public string Value { get; set; }

        public IEnumerable<string> Values { get; set; }

        public bool IsSearchable { get; set; }

        public bool Multiple { get; set; }
    }
}
