// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.FriendlyException;
using Gardener.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Base
{
    /// <summary>
    /// 分部拓展类
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static async Task FakeDeleteAsync<TEntity>(this IRepository<TEntity> repository, TEntity entity) where TEntity : class, IPrivateEntity, new()
        {
            if (entity is IModelDeleted temp)
            {
                List<string> includeFields = new List<string> { nameof(IModelDeleted.IsDeleted) };
                temp.IsDeleted = true;
                await repository.UpdateIncludeAsync(entity, includeFields);
            }
            else
            {
                throw Oops.Oh($"{typeof(TEntity).Name} no implement {nameof(IModelDeleted)}");
            }
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static async Task FakeDeleteNowAsync<TEntity>(this IRepository<TEntity> repository, TEntity entity) where TEntity : class, IPrivateEntity, new()
        {
            if (entity is IModelDeleted temp)
            {
                List<string> includeFields = new List<string> { nameof(IModelDeleted.IsDeleted) };
                temp.IsDeleted = true;
                await repository.UpdateIncludeNowAsync(entity, includeFields);
            }
            else
            {
                throw Oops.Oh($"{typeof(TEntity).Name} no implement {nameof(IModelDeleted)}");
            }
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDbContextLocator"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static async Task FakeDeleteAsync<TEntity, TDbContextLocator>(this IRepository<TEntity, TDbContextLocator> repository, TEntity entity) where TEntity : class, IPrivateEntity, new() where TDbContextLocator : class, IDbContextLocator
        {
            if (entity is IModelDeleted temp)
            {
                List<string> includeFields = new List<string> { nameof(IModelDeleted.IsDeleted) };
                temp.IsDeleted = true;
                await repository.UpdateIncludeAsync(entity, includeFields);
            }
            else
            {
                throw Oops.Oh($"{typeof(TEntity).Name} no implement {nameof(IModelDeleted)}");
            }
            
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDbContextLocator"></typeparam>
        /// <param name="repository"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        private static async Task FakeDeleteNowAsync<TEntity, TDbContextLocator>(this IRepository<TEntity, TDbContextLocator> repository, TEntity entity) where TEntity : class, IPrivateEntity, new() where TDbContextLocator : class, IDbContextLocator
        {
            if (entity is IModelDeleted temp)
            {
                List<string> includeFields = new List<string> { nameof(IModelDeleted.IsDeleted) };
                temp.IsDeleted = true;
                await repository.UpdateIncludeNowAsync(entity, includeFields);
            }
            else
            {
                throw Oops.Oh($"{typeof(TEntity).Name} no implement {nameof(IModelDeleted)}");
            }
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="repository"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task FakeDeleteByKeyAsync<TEntity, TKey>(this IRepository<TEntity> repository, TKey id) where TEntity : class, IPrivateEntity, new()
        {
            TEntity? entity = await repository.FindOrDefaultAsync(id);
            if (entity == null)
            {
                throw Oops.Oh(ExceptionCode.Data_Not_Find);
            }
            await repository.FakeDeleteAsync<TEntity>(entity);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="repository"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task FakeDeleteNowByKeyAsync<TEntity, TKey>(this IRepository<TEntity> repository, TKey id) where TEntity : class, IPrivateEntity, new()
        {
            TEntity? entity = await repository.FindOrDefaultAsync(id);
            if (entity == null)
            {
                throw Oops.Oh(ExceptionCode.Data_Not_Find);
            }
            await repository.FakeDeleteNowAsync<TEntity>(entity);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TDbContextLocator"></typeparam>
        /// <param name="repository"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task FakeDeleteByKeyAsync<TEntity, TKey, TDbContextLocator>(this IRepository<TEntity, TDbContextLocator> repository, TKey id) where TEntity : class, IPrivateEntity, new() where TDbContextLocator : class, IDbContextLocator
        {
            TEntity? entity = await repository.FindOrDefaultAsync(id);
            if(entity == null)
            {
                throw Oops.Oh(ExceptionCode.Data_Not_Find);
            }
            await repository.FakeDeleteAsync<TEntity, TDbContextLocator>(entity);
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TDbContextLocator"></typeparam>
        /// <param name="repository"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task FakeDeleteNowByKeyAsync<TEntity, TKey, TDbContextLocator>(this IRepository<TEntity, TDbContextLocator> repository, TKey id) where TEntity : class, IPrivateEntity, new() where TDbContextLocator : class, IDbContextLocator
        {
            TEntity? entity = await repository.FindOrDefaultAsync(id);
            if (entity == null)
            {
                throw Oops.Oh(ExceptionCode.Data_Not_Find);
            }
            await repository.FakeDeleteNowAsync<TEntity, TDbContextLocator>(entity);
        }
    }
}
