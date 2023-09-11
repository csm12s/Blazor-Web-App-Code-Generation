// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Audit.Dtos;
using Gardener.Audit.Resources;
using Gardener.Audit.Services;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Audit.Client.Pages
{
    public partial class AuditOperation : ListTableBase<AuditOperationDto, Guid, AuditLocalResource>
    {
        [Inject]
        public IAuditOperationService auditOperationService { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditEntity"></param>
        /// <returns></returns>
        private async Task OnDetailClick(Guid id)
        {
            List<AuditEntityDto>  auditEntityDtos= await auditOperationService.GetAuditEntity(id);

            await OpenOperationDialogAsync<AuditEntityDetailDrawer, ICollection<AuditEntityDto>, bool>(Localizer[SharedLocalResourceKeys.Detail],auditEntityDtos, width: 960);
        }
    }
}
