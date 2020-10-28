// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Fur.DatabaseAccessor;
using Fur.DynamicApiController;
using Microsoft.EntityFrameworkCore;
using Mapster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Application
{
    /// <summary>
    /// 继承此类即可实现基础方法
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TEntityDto"></typeparam>
    public abstract class ServiceBase<TEntity,TEntityDto> : IDynamicApiController where TEntity : class, IPrivateEntity, new() where TEntityDto : class, IPrivateEntity, new()
    {
        private readonly IRepository<TEntity> _repository;
        /// <summary>
        /// 
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
        public virtual async Task<TEntity> Insert(TEntityDto input)
        {
            var newEntity = await _repository.InsertNowAsync(input.Adapt<TEntity>());
            return newEntity.Entity;
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

        /// <summary>
        /// 查询一条
        /// </summary>
        /// <param name="id"></param>
        public virtual async Task<TEntityDto> Find(int id)
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
            var persons = _repository.AsQueryable().ProjectToType<TEntityDto>();
            return await persons.ToListAsync();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<PagedList<TEntityDto>> GetAllByPage(int pageIndex = 1, int pageSize = 10)
        {
            var pageResult = _repository.AsQueryable().ProjectToType<TEntityDto>();

            return await pageResult.ToPagedListAsync(pageIndex, pageSize);
        }
    }
    /// <summary>
    /// 继承此类即可实现基础方法
    /// CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class ServiceBase<TEntity> : ServiceBase<TEntity, TEntity> where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// 继承此类即可实现基础方法
        /// CURD、获取全部、分页获取 </summary>
        /// <param name="repository"></param>
        protected ServiceBase(IRepository<TEntity> repository) : base(repository)
        {
        }
    }
}
