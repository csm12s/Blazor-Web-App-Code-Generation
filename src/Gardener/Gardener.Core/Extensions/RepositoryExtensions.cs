// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Common;
using Gardener.Core.Entites;
using System.Threading.Tasks;

namespace Gardener.Core
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
        public static async Task FakeDeleteAsync<TEntity>(this IRepository<TEntity> repository, TEntity entity) where TEntity : class, IPrivateEntity, new()
        {
            if (entity != null && entity.SetPropertyValue(nameof(GardenerEntityBase.IsDeleted), true))
            {
                await repository.UpdateIncludeAsync(entity, new[] { nameof(GardenerEntityBase.IsDeleted) });
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
            TEntity entity = await repository.FindAsync(id);
            await repository.FakeDeleteAsync<TEntity>(entity);
        }
    }
}
