// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using Microsoft.EntityFrameworkCore;
using Mapster;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Gardener.Common;
using System;
using Gardener.Application.Interfaces;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;
using Gardener.Core;
using Gardener.Core.Entites;

namespace Gardener.Application
{
    /// <summary>
    /// 继承此类即可实现基础方法
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    /// <typeparam name="TEntityDto">数据实体对应DTO类型</typeparam>
    /// <typeparam name="TKey">数据实体主键类型</typeparam>
    public abstract class ApplicationServiceBase<TEntity, TEntityDto, TKey> : IDynamicApiController, IApplicationServiceBase<TEntityDto, TKey> where TEntity : class, IPrivateEntity, new() where TEntityDto : class, new()
    {
        private readonly IRepository<TEntity> _repository;
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        protected ApplicationServiceBase(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <remarks>
        /// 添加一条数据
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<TEntityDto> Insert(TEntityDto input)
        {
            DateTimeOffset defaultValue = input.GetPropertyValue<TEntityDto, DateTimeOffset>("CreatedTime");

            if (defaultValue.Equals(default(DateTimeOffset)))
            {
                input.SetPropertyValue("CreatedTime", DateTimeOffset.Now);
            }

            var newEntity = await _repository.InsertNowAsync(input.Adapt<TEntity>());
            return newEntity.Entity.Adapt<TEntityDto>();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <remarks>
        /// 更新一条数据
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<bool> Update(TEntityDto input)
        {
            input.SetPropertyValue("UpdatedTime", DateTimeOffset.Now);
            await _repository.UpdateExcludeAsync(input.Adapt<TEntity>(), new[] { "CreatedTime" });
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <remarks>
        /// 根据主键删除一条数据
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<bool> Delete(TKey id)
        {
            await _repository.DeleteAsync(id);
            return true;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <remarks>
        /// 根据多个主键批量删除
        /// </remarks>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Summary = "批量删除",Description = "根据多个主键批量删除")]
        public virtual async Task<bool> Deletes([FromBody] TKey[] ids)
        {
            foreach (TKey id in ids)
            {
                await _repository.DeleteAsync(id);
            }
            return true;
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <remarks>
        /// 根据主键逻辑删除
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public virtual async Task<bool> FakeDelete(TKey id)
        {
            await _repository.FakeDeleteByKeyAsync(id);
            return true;
        }
        
        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <remarks>
        /// 根据多个主键批量逻辑删除
        /// </remarks>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Summary = "批量逻辑删除", Description = "根据多个主键批量逻辑删除")]
        public virtual async Task<bool> FakeDeletes([FromBody] TKey[] ids)
        {
            foreach (TKey id in ids)
            {
                await _repository.FakeDeleteByKeyAsync(id);
            }
            return true;
        }

        /// <summary>
        /// 根据主键获取
        /// </summary>
        /// <remarks>
        /// 根据主键查找一条数据
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntityDto> Get(TKey id)
        {
            var person = await _repository.FindAsync(id);
            return person.Adapt<TEntityDto>();
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <remarks>
        /// 查找到所有数据
        /// </remarks>
        /// <returns></returns>
        public virtual async Task<List<TEntityDto>> GetAll()
        {
            var persons = _repository.AsQueryable().Select(x => x.Adapt<TEntityDto>());
            return await persons.ToListAsync();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <remarks>
        /// 根据分页参数，分页获取数据
        /// </remarks>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<Dtos.PagedList<TEntityDto>> GetPage(int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = _repository.AsQueryable();

            var result= await pageResult.ToPagedListAsync(pageIndex, pageSize);
            
            return result.Adapt<Dtos.PagedList<TEntityDto>>();
        }

        /// <summary>
        /// 锁定
        /// </summary>
        /// <remarks>
        /// 根据主键锁定或解锁数据
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="isLocked"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> Lock([ApiSeat(ApiSeats.ActionStart)] TKey id, bool isLocked = true)
        {
            var entity = await _repository.FindAsync(id);
            if (entity != null && entity.SetPropertyValue(nameof(GardenerEntityBase.IsLocked), isLocked))
            {
                await _repository.UpdateIncludeAsync(entity, new[] { nameof(GardenerEntityBase.IsLocked) });
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// 继承此类即可实现基础方法
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    public abstract class ApplicationServiceBase<TEntity> : ApplicationServiceBase<TEntity, TEntity, int> where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        protected ApplicationServiceBase(IRepository<TEntity> repository) : base(repository)
        {
        }
    }
   
    /// <summary>
    /// 继承此类即可实现基础方法
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    /// <typeparam name="TEntityDto">数据实体对应DTO类型</typeparam>
    public abstract class ApplicationServiceBase<TEntity, TEntityDto> : ApplicationServiceBase<TEntity, TEntityDto, int> where TEntity : class, IPrivateEntity, new() where TEntityDto : class, new()
    {
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        protected ApplicationServiceBase(IRepository<TEntity> repository) : base(repository)
        {
        }
    }
}
