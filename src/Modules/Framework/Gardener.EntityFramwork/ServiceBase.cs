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
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;
using Furion;
using System.Linq.Expressions;
using Gardener.Base;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text;
using System.IO;
using MiniExcelLibs;
using Gardener.FileStore;

namespace Gardener.EntityFramwork
{
    /// <summary>
    /// 继承此类即可实现基础方法
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    /// <typeparam name="TEntityDto">数据实体对应DTO类型</typeparam>
    /// <typeparam name="TKey">数据实体主键类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    public abstract class ServiceBase<TEntity, TEntityDto, TKey, TDbContextLocator> : IDynamicApiController, IServiceBase<TEntityDto, TKey> where TEntity : class, IPrivateEntity, new() where TEntityDto : class, new() where TDbContextLocator : class, IDbContextLocator
    {
        /// <summary>
        /// TEntity Repository
        /// </summary>
        public readonly IRepository<TEntity, TDbContextLocator> _repository;
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        protected ServiceBase(IRepository<TEntity, TDbContextLocator> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        protected ServiceBase(IRepository<TEntity> repository)
        {
            _repository = (IRepository<TEntity, TDbContextLocator>)repository;
        }

        /// <summary>
        /// 获取可读仓库对象
        /// </summary>
        /// <returns></returns>
        protected virtual IPrivateReadableRepository<TEntity> GetReadableRepository()
        {
            return _repository;
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
            DateTimeOffset defaultValue = input.GetPropertyValue<TEntityDto, DateTimeOffset>(nameof(GardenerEntityBase.CreatedTime));

            if (defaultValue.Equals(default(DateTimeOffset)))
            {
                input.SetPropertyValue(nameof(GardenerEntityBase.CreatedTime), DateTimeOffset.Now);
            }
            TEntity entity = input.Adapt<TEntity>();
            if (entity is GardenerEntityBase<TKey> ge1)
            {
                ge1.CreateBy = IdentityUtil.GetIdentityId();
                ge1.CreateIdentityType = IdentityUtil.GetIdentityType();
            }
            var newEntity = await _repository.InsertNowAsync(entity);
            //发送通知
            await EntityEventNotityUtil.NotifyInsertAsync(newEntity.Entity);
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
            input.SetPropertyValue(nameof(GardenerEntityBase.UpdatedTime), DateTimeOffset.Now);
            EntityEntry<TEntity> entityEntry = await _repository.UpdateExcludeAsync(input.Adapt<TEntity>(), new[] { nameof(GardenerEntityBase.CreatedTime), nameof(GardenerEntityBase.CreateBy), nameof(GardenerEntityBase.CreateIdentityType) });
            //发送通知
            await EntityEventNotityUtil.NotifyUpdateAsync(entityEntry.Entity);
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
            //发送删除通知
            await EntityEventNotityUtil.NotifyDeleteAsync<TEntity, TKey>(id);
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
        [SwaggerOperation(Summary = "批量删除", Description = "根据多个主键批量删除")]
        public virtual async Task<bool> Deletes([FromBody] TKey[] ids)
        {
            foreach (TKey id in ids)
            {
                await _repository.DeleteAsync(id);

            }
            await EntityEventNotityUtil.NotifyDeletesAsync<TEntity, TKey>(ids);
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
            await EntityEventNotityUtil.NotifyFakeDeleteAsync<TEntity, TKey>(id);
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
            await EntityEventNotityUtil.NotifyFakeDeletesAsync<TEntity, TKey>(ids);
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
            var person = await GetReadableRepository().FindAsync(id);
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
            var persons = GetReadableRepository().AsQueryable().Select(x => x.Adapt<TEntityDto>());
            return await persons.ToListAsync();
        }

        /// <summary>
        /// 查询所有可以用的
        /// </summary>
        /// <remarks>
        /// 查询所有可以用的(在有IsDelete、IsLock字段时会自动过滤)
        /// </remarks>
        /// <returns></returns>
        public virtual async Task<List<TEntityDto>> GetAllUsable()
        {

            System.Text.StringBuilder where = new StringBuilder();
            where.Append(" 1==1 ");
            //判断是否有IsDelete、IsLock
            if (typeof(TEntity).ExistsProperty(nameof(GardenerEntityBase.IsDeleted)))
            {
                where.Append($"and {nameof(GardenerEntityBase.IsDeleted)}==false ");
            }
            if (typeof(TEntity).ExistsProperty(nameof(GardenerEntityBase.IsLocked)))
            {
                where.Append($"and {nameof(GardenerEntityBase.IsLocked)}==false ");
            }
            var persons = GetReadableRepository().AsQueryable().Where(where.ToString()).Select(x => x.Adapt<TEntityDto>());
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
        public virtual async Task<Base.PagedList<TEntityDto>> GetPage(int pageIndex = 1, int pageSize = 10)
        {
            var queryable = GetReadableRepository().AsQueryable();

            var result = await queryable.ToPageAsync(pageIndex, pageSize);

            return result.Adapt<Base.PagedList<TEntityDto>>();
        }

        /// <summary>
        /// 锁定
        /// </summary>
        /// <remarks>
        /// 根据主键锁定或解锁数据（必须有IsLock才能生效）
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="isLocked"></param>
        /// <returns></returns>
        [HttpPut]
        public virtual async Task<bool> Lock([ApiSeat(ApiSeats.ActionStart)] TKey id, bool isLocked = true)
        {
            var entity = await _repository.FindAsync(id);
            if (entity != null && entity.SetPropertyValue(nameof(GardenerEntityBase.IsLocked), isLocked))
            {
                entity.SetPropertyValue(nameof(GardenerEntityBase.UpdatedTime), DateTimeOffset.Now);
                await _repository.UpdateIncludeAsync(entity, new[] { nameof(GardenerEntityBase.IsLocked), nameof(GardenerEntityBase.UpdatedTime) });
                await EntityEventNotityUtil.NotifyLockAsync(entity);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <remarks>
        /// 搜索数据
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<Base.PagedList<TEntityDto>> Search(PageRequest request)
        {
            IDynamicFilterService filterService = App.GetService<IDynamicFilterService>();
            if (typeof(TEntity).ExistsProperty(nameof(GardenerEntityBase.IsDeleted)))
            {
                FilterGroup defaultFilterGroup = new FilterGroup();
                defaultFilterGroup.AddRule(new FilterRule(nameof(GardenerEntityBase.IsDeleted), false, FilterOperate.Equal));
                request.FilterGroups.Add(defaultFilterGroup);
            }
            Expression<Func<TEntity, bool>> expression = filterService.GetExpression<TEntity>(request.FilterGroups);

            IQueryable<TEntity> queryable = GetReadableRepository().AsQueryable(false).Where(expression);
            return await queryable
                .OrderConditions(request.OrderConditions.ToArray())
                .Select(x => x.Adapt<TEntityDto>())
                .ToPageAsync(request.PageIndex, request.PageSize);
        }

        /// <summary>
        /// 生成种子数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>
        /// 根据搜索条叫生成种子数据
        /// </remarks>
        public virtual async Task<string> GenerateSeedData(PageRequest request)
        {
            IDynamicFilterService filterService = App.GetService<IDynamicFilterService>();
            if (typeof(TEntity).ExistsProperty(nameof(GardenerEntityBase.IsDeleted)))
            {
                FilterGroup defaultFilterGroup = new FilterGroup();
                defaultFilterGroup.AddRule(new FilterRule(nameof(GardenerEntityBase.IsDeleted), false, FilterOperate.Equal));
                request.FilterGroups.Add(defaultFilterGroup);
            }
            Expression<Func<TEntity, bool>> expression = filterService.GetExpression<TEntity>(request.FilterGroups);

            IQueryable<TEntity> queryable = GetReadableRepository().AsQueryable(false).Where(expression);
            Base.PagedList<TEntity> result = await queryable.OrderConditions(request.OrderConditions.ToArray()).ToPageAsync(request.PageIndex, request.PageSize);
            return SeedDataGenerateTool.Generate(result.Items, typeof(TEntity).Name);
        }


        /// <summary>
        /// 导出
        /// </summary>
        /// <remarks>
        /// 导出数据
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual async Task<string> Export(PageRequest request)
        {
            IDynamicFilterService filterService = App.GetService<IDynamicFilterService>();
            if (typeof(TEntity).ExistsProperty(nameof(GardenerEntityBase.IsDeleted)))
            {
                FilterGroup defaultFilterGroup = new FilterGroup();
                defaultFilterGroup.AddRule(new FilterRule(nameof(GardenerEntityBase.IsDeleted), false, FilterOperate.Equal));
                request.FilterGroups.Add(defaultFilterGroup);
            }
            Expression<Func<TEntity, bool>> expression = filterService.GetExpression<TEntity>(request.FilterGroups);

            IQueryable<TEntity> queryable = GetReadableRepository().AsQueryable(false).Where(expression);
            var list = await queryable
                 .OrderConditions(request.OrderConditions.ToArray())
                 .Select(x => x.Adapt<TEntityDto>()).ToListAsync();

            var memoryStream = new MemoryStream();
            memoryStream.SaveAs(list);
            memoryStream.Seek(0, SeekOrigin.Begin);
            string fileName = typeof(TEntityDto).GetDescription() + DateTimeOffset.UtcNow.ToLocalTime().ToString("yyyyMMddHHmmss") + ".xlsx";
            var fileService = App.GetService<IFileStoreService>();
            
            return await fileService.Save(memoryStream, "export/" + fileName);
        }

    }

    /// <summary>
    /// 继承此类即可实现基础方法
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    public abstract class ServiceBase<TEntity> : ServiceBase<TEntity, TEntity, int, MasterDbContextLocator> where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        protected ServiceBase(IRepository<TEntity, MasterDbContextLocator> repository) : base(repository)
        {
        }
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        protected ServiceBase(IRepository<TEntity> repository) : base(repository)
        {
        }
    }

    /// <summary>
    /// 继承此类即可实现基础方法
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    /// <typeparam name="TEntityDto">数据实体对应DTO类型</typeparam>
    public abstract class ServiceBase<TEntity, TEntityDto> : ServiceBase<TEntity, TEntityDto, int, MasterDbContextLocator> where TEntity : class, IPrivateEntity, new() where TEntityDto : class, new()
    {
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        protected ServiceBase(IRepository<TEntity, MasterDbContextLocator> repository) : base(repository)
        {
        }
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        protected ServiceBase(IRepository<TEntity> repository) : base(repository)
        {
        }
    }

    /// <summary>
    /// 继承此类即可实现基础方法
    /// 方法包括：CURD、获取全部、分页获取 
    /// </summary>
    /// <typeparam name="TEntity">数据实体类型</typeparam>
    /// <typeparam name="TEntityDto">数据实体对应DTO类型</typeparam>
    /// <typeparam name="TKey">数据实体主键</typeparam>
    public abstract class ServiceBase<TEntity, TEntityDto, TKey> : ServiceBase<TEntity, TEntityDto, TKey, MasterDbContextLocator> where TEntity : class, IPrivateEntity, new() where TEntityDto : class, new()
    {
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        protected ServiceBase(IRepository<TEntity, MasterDbContextLocator> repository) : base(repository)
        {
        }
        /// <summary>
        /// 继承此类即可实现基础方法
        /// 方法包括：CURD、获取全部、分页获取 
        /// </summary>
        /// <param name="repository"></param>
        protected ServiceBase(IRepository<TEntity> repository) : base(repository)
        {
        }
    }

}
