// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.UserCenter.Impl.Domains;
using Gardener.UserCenter.Dtos;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gardener.UserCenter.Services;

namespace Gardener.UserCenter.Impl.Services
{
    /// <summary>
    /// 部门服务
    /// </summary>
    [ApiDescriptionSettings("UserCenterServices")]
    public class DeptService : ServiceBase<Dept, DeptDto, int>,IDeptService
    {
        /// <summary>
        /// 部门服务
        /// </summary>
        /// <param name="repository"></param>
        public DeptService(IRepository<Dept> repository) : base(repository)
        {
        }
        /// <summary>
        /// 获取种子数据
        /// </summary>
        /// <returns></returns>
        public Task<string> GetSeedData()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取所有部门数据，以树形结构返回
        /// </summary>
        /// <returns></returns>
        public async Task<List<DeptDto>> GetTree()
        {
            List<DeptDto> depts = new List<DeptDto>();

            var list =await _repository
                .Where(x => x.IsDeleted == false && x.IsLocked==false)
                .OrderBy(x => x.Order)
                .ToListAsync();
            return list.Where(x => !x.ParentId.HasValue).Select(x => x.Adapt<DeptDto>()).ToList();
        }
    }
}
