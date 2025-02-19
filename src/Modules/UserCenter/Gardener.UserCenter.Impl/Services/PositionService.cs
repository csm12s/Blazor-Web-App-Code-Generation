﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.UserCenter.Impl.Domains;
using Gardener.UserCenter.Dtos;
using Microsoft.AspNetCore.Mvc;
using Gardener.UserCenter.Services;
using Gardener.EntityFramwork;
using Gardener.Base.Entity;

namespace Gardener.UserCenter.Impl.Services
{
    /// <summary>
    /// 岗位管理服务
    /// </summary>
    [ApiDescriptionSettings("UserCenterServices")]
    public class PositionService : ServiceBase<Position, PositionDto, int, GardenerMultiTenantDbContextLocator>, IPositionService
    {

        private readonly IRepository<Position, GardenerMultiTenantDbContextLocator> _positionRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionRepository"></param>
        public PositionService(IRepository<Position, GardenerMultiTenantDbContextLocator> positionRepository) : base(positionRepository)
        {
            this._positionRepository = positionRepository;
        }
    }
}
