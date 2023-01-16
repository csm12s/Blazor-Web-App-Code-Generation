// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Audit.Dtos;
using Gardener.Audit.Services;
using Gardener.Base.Resources;
using Gardener.Client.Base.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Audit.Client.Pages
{
    public partial class AuditOperation : ListTableBase<AuditOperationDto, Guid>
    {
        [Inject]
        public IAuditOperationService auditOperationService { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditEntity"></param>
        /// <returns></returns>
        private async Task OnDetailClick(Guid id)
        {
            List<AuditEntityDto>  auditEntityDtos= await auditOperationService.GetAuditEntity(id);

            await OpenOperationDialogAsync<AuditEntityDetailDrawer, ICollection<AuditEntityDto>, bool>(localizer[SharedLocalResource.Detail],auditEntityDtos, width: 960);
        }
    }
}
