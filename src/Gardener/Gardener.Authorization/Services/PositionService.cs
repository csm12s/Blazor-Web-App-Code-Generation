// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authorization.Domains;
using Gardener.Authorization.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Gardener.Authorization.Services
{
    /// <summary>
    /// 岗位管理服务
    /// </summary>
    [ApiDescriptionSettings("SystemManagerServices")]
    public class PositionService : ServiceBase<Position, PositionDto, int>, IPositionService
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

    }
}
