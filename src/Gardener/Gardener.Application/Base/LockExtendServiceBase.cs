// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gardener.Application.Interfaces;
using Gardener.Core.Entites;

namespace Gardener.Application
{
    /// <summary>
    /// 继承此类即可实现基础方法
    /// 方法包括：CURD、获取全部、分页获取、锁定
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    /// <typeparam name="TEntityDto">数据实体对应DTO类型</typeparam>
    /// <typeparam name="TKey">数据实体主键类型</typeparam>
    public abstract class LockExtendServiceBase<TEntity,TEntityDto, TKey> : 
        ApplicationServiceBase<TEntity, TEntityDto, TKey>, 
        IDynamicApiController,
        IApplicationServiceBase<TEntityDto, TKey> ,
        IApplicationLockServiceBase<TKey> where TEntity : class, IPrivateEntity,ILockEntity, new() where TEntityDto : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IRepository<TEntity> _repository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        protected LockExtendServiceBase(IRepository<TEntity> repository) :base(repository)
        {
            this._repository = repository;
        }
        /// <summary>
        /// 锁定
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isLocked"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> Lock([ApiSeat(ApiSeats.ActionStart)] TKey id,bool isLocked = true)
        {
            var entity =await _repository.FindAsync(id);
            if (entity == null) return false;
            entity.IsLocked = isLocked;
            await _repository.UpdateIncludeExistsAsync(entity, new[] { nameof(ILockEntity.IsLocked) });
            return true;
        }
    }

    /// <summary>
    /// 继承此类即可实现基础方法
    /// 方法包括：CURD、获取全部、分页获取、锁定
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    public abstract class LockExtendServiceBase<TEntity> : LockExtendServiceBase<TEntity, TEntity, int> where TEntity : class, IPrivateEntity, ILockEntity, new()
    {
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取、锁定
        /// </summary>
        /// <param name="repository"></param>
        protected LockExtendServiceBase(IRepository<TEntity> repository) : base(repository)
        {
        }
    }

    /// <summary>
    /// 继承此类即可实现基础方法
    /// 方法包括：CURD、获取全部、分页获取、锁定
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    /// <typeparam name="TEntityDto">数据实体对应DTO类型</typeparam>
    public abstract class LockExtendServiceBase<TEntity, TEntityDto> : LockExtendServiceBase<TEntity, TEntityDto, int> where TEntity : class, IPrivateEntity, ILockEntity, new() where TEntityDto : class, new()
    {
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取、锁定
        /// </summary>
        /// <param name="repository"></param>
        protected LockExtendServiceBase(IRepository<TEntity> repository) : base(repository)
        {
        }
    }
}
