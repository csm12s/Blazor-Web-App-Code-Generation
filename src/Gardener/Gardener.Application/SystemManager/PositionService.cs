// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Application.Dtos;
using Gardener.Application.Interfaces.SystemManager;
using Gardener.Core.Entites;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Application
{
    /// <summary>
    /// 岗位管理服务
    /// </summary>
    [ApiDescriptionSettings("SystemManagerServices")]
    public class PositionService : ApplicationServiceBase<Position, PositionDto, Guid>, IPositionService
    {

        private readonly IRepository<Position> _positionRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionRepository"></param>
        public PositionService(IRepository<Position> positionRepository) : base(positionRepository)
        {
            this._positionRepository = positionRepository;
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <remarks>
        /// 搜索角色数据
        /// </remarks>
        /// <param name="name">角色名称</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Dtos.PagedList<PositionDto>> Search([FromQuery] string name,
            int pageIndex = 1,
            int pageSize = 10)
        {
            return await _positionRepository
                .Where(!string.IsNullOrEmpty(name), x => x.Name.Contains(name))
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.CreatedTime)
                .Select(x => x.Adapt<PositionDto>())
                .ToPagedListAsync<PositionDto>(pageIndex, pageSize);
        }
    }
}
