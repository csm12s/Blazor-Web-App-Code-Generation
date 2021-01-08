﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public class AuditOperationService : ApplicationServiceBase<AuditOperationDto, Guid>, IAuditOperationService
    {
        private readonly static string controller = "audit-operation";
        private readonly IApiCaller apiCaller;
        public AuditOperationService(IApiCaller apiCaller) : base(apiCaller, controller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<List<AuditEntityDto>> GetAuditEntity(Guid operationId)
        {
            return await apiCaller.GetAsync<List<AuditEntityDto>>($"{controller}/{operationId}/audit-entity");
        }

        public async Task<PagedList<AuditOperationDto>> Search(AuditOperationSearchInput searchInput)
        {
            return await apiCaller.PostAsync<AuditOperationSearchInput, PagedList<AuditOperationDto>>($"{controller}/search", searchInput);
        }
    }
}