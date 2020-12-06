﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Application
{
    public interface IRoleService
    {
        Task<bool> DeleteResource([ApiSeat(ApiSeats.ActionStart)] int roleId);
        Task<bool> Resource([ApiSeat(ApiSeats.ActionStart)] int roleId, Guid[] resourceIds);
        Task<PagedList<RoleDto>> Search([FromQuery] string name,  int pageIndex = 1,  int pageSize = 10);
        /// <summary>
        /// 获取有效的角色
        /// </summary>
        /// <returns></returns>
        Task<List<RoleDto>> GetEffective();
        /// <summary>
        /// 获取角色所有资源
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<ResourceDto>> GetResource(int roleId);
    }
}