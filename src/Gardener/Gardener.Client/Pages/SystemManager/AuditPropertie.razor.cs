// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Application.Dtos;
using Gardener.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Pages.SystemManager
{
    public partial class AuditPropertie : DrawerTemplate<AuditEntityDto, bool>
    {
        private ICollection<AuditPropertyDto> auditProperties;
        private OperationType operationType;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            if (this.Options != null)
            {
                operationType = this.Options.OperationType;
                auditProperties = this.Options.AuditProperties;
            }
        }

    }
}
