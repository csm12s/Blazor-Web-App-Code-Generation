// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using Gardener.Core;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Application.UserCenter
{
    /// <summary>
    /// 角色服务
    /// </summary>
    [AppAuthorize, ApiDescriptionSettings("UserAuthorizationServices")]
    public class RoleService : IDynamicApiController
    {
        private readonly IRepository<Role> _roleRepository;
        /// <summary>
        /// 角色服务
        /// </summary>
        /// <param name="roleRepository"></param>
        public RoleService(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        [ApiSecurityDefine("新增角色")]
        public async Task<int> InsertRole(RoleInput input)
        {
            var entity= await _roleRepository.InsertAsync(input.Adapt<Role>());

            return entity.Entity.Id;
        }
        /// <summary>
        /// 更新一条
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task Update(TEntityDto input)
        {
            // 还可以直接操作
            await input.Adapt<TEntity>().UpdateAsync();
        }

        /// <summary>
        /// 删除一条
        /// </summary>
        /// <param name="id"></param>
        public virtual async Task Delete(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
