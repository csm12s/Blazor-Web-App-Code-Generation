// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using Microsoft.EntityFrameworkCore;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Gardener.Application
{
    /// <summary>
    /// 继承此类即可实现基础方法
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    /// <typeparam name="TEntityDto">数据实体对应DTO类型</typeparam>
    /// <typeparam name="TKey">数据实体主键类型</typeparam>
    public abstract class ServiceBase<TEntity, TEntityDto, TKey> : IDynamicApiController, IServiceBase<TEntityDto, TKey> where TEntity : class, IPrivateEntity, new() where TEntityDto : class, new()
    {
        private readonly IRepository<TEntity> _repository;
        /// <summary>
        /// 继承此类即可实现基础方法
        /// CURD、获取全部、分页获取
        /// </summary>
        /// <param name="repository"></param>
        protected ServiceBase(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 新增一条
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<TEntityDto> Insert(TEntityDto input)
        {
            var newEntity = await _repository.InsertNowAsync(input.Adapt<TEntity>());
            return newEntity.Entity.Adapt<TEntityDto>();
        }
          
        /// <summary>
        /// 更新一条
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<bool> Update(TEntityDto input)
        {
            // 还可以直接操作
            await input.Adapt<TEntity>().UpdateExcludeAsync("CreatedTime");
            return true;
        }

        /// <summary>
        /// 删除一条
        /// </summary>
        /// <param name="id"></param>
        public virtual async Task<bool> Delete(TKey id)
        {
            await _repository.DeleteAsync(id);
            return true;
        }
        /// <summary>
        /// 删除多条
        /// </summary>
        /// <param name="ids"></param>
        [HttpPost]
        public virtual async Task<bool> Deletes(TKey [] ids)
        {
            foreach (TKey id in ids)
            {
                await _repository.DeleteAsync(id);
            }
            return true;
        }
        /// <summary>
        /// 删除一条(逻辑删除)
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public virtual async Task<bool> FakeDelete(TKey id)
        {
            await _repository.FakeDeleteAsync(id);
            return true;
        }
        /// <summary>
        /// 删除一条(逻辑删除)
        /// </summary>
        /// <param name="ids"></param>
        [HttpPost]
        public virtual async Task<bool> FakeDeletes(TKey [] ids)
        {
            foreach (TKey id in ids)
            {
                await _repository.FakeDeleteAsync(id);
            }
            return true;
        }
        /// <summary>
        /// 查询一条
        /// </summary>
        /// <param name="id"></param>
        public virtual async Task<TEntityDto> Get(TKey id)
        {
            var person = await _repository.FindAsync(id);
            return person.Adapt<TEntityDto>();
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<TEntityDto>> GetAll()
        {
            var persons = _repository.AsQueryable().Select(x=>x.Adapt<TEntityDto>());
            return await persons.ToListAsync();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<PagedList<TEntityDto>> GetPage(int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = _repository.AsQueryable().Select(x => x.Adapt<TEntityDto>());

            return await pageResult.ToPagedListAsync(pageIndex, pageSize);
        }
    }
    /// <summary>
    /// 继承此类即可实现基础方法
    /// CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    public abstract class ServiceBase<TEntity> : ServiceBase<TEntity, TEntity,int> where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// 继承此类即可实现基础方法
        /// CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        protected ServiceBase(IRepository<TEntity> repository) : base(repository)
        {
        }
    }

    /// <summary>
    /// 继承此类即可实现基础方法
    /// CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    /// <typeparam name="TEntityDto">数据实体对应DTO类型</typeparam>
    public abstract class ServiceBase<TEntity, TEntityDto> : ServiceBase<TEntity, TEntityDto, int> where TEntity : class, IPrivateEntity, new() where TEntityDto : class, new()
    {
        /// <summary>
        /// 继承此类即可实现基础方法
        /// CURD、获取全部、分页获取
        /// </summary>
        /// <param name="repository"></param>
        protected ServiceBase(IRepository<TEntity> repository) : base(repository)
        {
        }
    }
}
