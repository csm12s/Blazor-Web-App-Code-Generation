// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Audit.Dtos;
using Gardener.Audit.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Audit.Client.Pages
{
    public partial class AuditEntity : ListTableBase<AuditEntityDto, Guid, AuditLocalResource>
    {
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditEntity"></param>
        /// <returns></returns>
        private async Task OnDetailClick(AuditEntityDto auditEntity)
        {
          await OpenOperationDialogAsync<AuditEntityDetailDrawer, ICollection<AuditEntityDto>, bool>(Localizer["FieldChangeDetails"], new[] { auditEntity }, width: 960);
        }
    }
}
