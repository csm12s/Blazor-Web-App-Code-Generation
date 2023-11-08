// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Audit.Dtos;
using Gardener.Audit.Resources;
using Gardener.Audit.Services;
using Gardener.Base;
using Gardener.Base.Resources;
using Gardener.Client.AntDesignUi.Base.Components;
using Gardener.Common;
using Gardener.Enums;
using Gardener.SystemManager.Dtos;
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
            List<AuditEntityDto> auditEntityDtos = await auditOperationService.GetAuditEntity(id);

            await OpenOperationDialogAsync<AuditEntityDetailDrawer, ICollection<AuditEntityDto>, bool>(Localizer[nameof(SharedLocalResource.Detail)], auditEntityDtos, width: 960);
        }

        /// <summary>
        /// 查看参数
        /// </summary>
        /// <returns></returns>
        private Task OnShowParametersClick(AuditOperationDto dto)
        {
            return OpenOperationDialogAsync<ShowCode, ShowCodeOptions, bool>(
                        Localizer[nameof(AuditLocalResource.Parameters)],
                       new ShowCodeOptions()
                       {
                           Code = Task.FromResult(dto.Parameters ?? string.Empty),
                           Language = "json"
                       },
                        width: 1300); ;
        }
    }
}
