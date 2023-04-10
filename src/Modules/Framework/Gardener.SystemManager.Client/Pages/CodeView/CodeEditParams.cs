// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;

namespace Gardener.SystemManager.Client.Pages.CodeView
{
    public class CodeEditParams: OperationDialogInput<int>
    {
        public int? CodeTypeId { get; set; }
    }
}
