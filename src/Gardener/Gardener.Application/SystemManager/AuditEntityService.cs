// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Gardener.Core.Entites;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Application.SystemManager
{
    /// <summary>
    /// 审计数据服务
    /// </summary>
    [ApiDescriptionSettings("SystemManagerServices")]
    public class AuditEntityService : ApplicationServiceBase<AuditEntity, AuditEntityDto, Guid>, IAuditEntityService
    {
        private readonly IRepository<AuditEntity> _repository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public AuditEntityService(IRepository<AuditEntity> repository) : base(repository)
        {
            this._repository = repository;
        }
    }
}
