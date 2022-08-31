// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Audit.Dtos;
using Gardener.Client.Base.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Audit.Client.Pages
{
    public partial class AuditEntity : ListTableBase<AuditEntityDto, Guid>
    {
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditEntity"></param>
        /// <returns></returns>
        private async Task OnDetailClick(AuditEntityDto auditEntity)
        {
          await OpenOperationDialogAsync<AuditEntityDetailDrawer, ICollection<AuditEntityDto>, bool>(localizer["字段变更详情"], new[] { auditEntity }, width: 960);
        }
    }
}
