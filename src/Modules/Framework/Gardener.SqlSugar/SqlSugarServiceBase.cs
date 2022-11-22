using Gardener.Common;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Mapster;
using Furion.DatabaseAccessor;
using Furion;
using MiniExcelLibs;
using Furion.DependencyInjection;
using Gardener.Base;
using Gardener.FileStore;
using Swashbuckle.AspNetCore.Annotations;

namespace Gardener.SqlSugar;

/// <summary>
/// 继承此类即可实现基础方法
/// 方法包括：CURD、获取全部、分页获取 
/// </summary>
/// <typeparam name="TEntity">数据实体类型</typeparam>
/// <typeparam name="TEntityDto">数据实体对应DTO类型</typeparam>
/// <typeparam name="TKey">数据实体主键类型</typeparam>
public abstract partial class SqlSugarServiceBase<TEntity, TEntityDto, TKey> :
    IServiceBase<TEntityDto, TKey>, ITransient
    where TEntity : class, IPrivateEntity, new()
    where TEntityDto : class, new()
{
    #region Init
    /// <summary>
    /// Sugar Repository
    /// </summary>
    public readonly SqlSugarRepository<TEntity> _sugarRepository;

    protected SqlSugarServiceBase(SqlSugarRepository<TEntity> sugarRepository)
    {
        _sugarRepository = sugarRepository;
    }
    #endregion

    #region CRUD

    #region Insert
    /// <summary>
    /// 添加
    /// </summary>
    /// <remarks>
    /// 添加一条数据
    /// </remarks>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
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
        var newEntity = await _sugarRepository.InsertReturnEntityAsync(entity);
        //发送通知
        await EntityEventNotityUtil.NotifyInsertAsync(newEntity);
        return newEntity.Adapt<TEntityDto>();
    }

    #endregion

    #region Update
    /// <summary>
    /// 更新
    /// </summary>
    /// <remarks>
    /// 更新一条数据
    /// </remarks>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public virtual async Task<bool> Update(TEntityDto input)
    {
        input.SetPropertyValue(nameof(GardenerEntityBase.UpdatedTime), DateTimeOffset.Now);
        var res = await _sugarRepository
            .UpdateExcludeAsync(input.Adapt<TEntity>(), new[] { nameof(GardenerEntityBase.CreatedTime), nameof(GardenerEntityBase.CreateBy), nameof(GardenerEntityBase.CreateIdentityType) });
        //发送通知
        await EntityEventNotityUtil.NotifyUpdateAsync(input.Adapt<TEntity>());
        return true;
    }

    #endregion

    #region Delete

    /// <summary>
    /// 删除
    /// </summary>
    /// <remarks>
    /// 根据主键删除一条数据
    /// </remarks>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost]
    public virtual async Task<bool> Delete(TKey id)
    {
        await _sugarRepository.DeleteByIdAsync(id);
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
            await _sugarRepository.DeleteByIdAsync(id);

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
    [HttpPost]
    public virtual async Task<bool> FakeDelete(TKey id)
    {
        await _sugarRepository.FakeDeleteByIdAsync(id);
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
            await _sugarRepository.FakeDeleteByIdAsync(id);
        }
        await EntityEventNotityUtil.NotifyFakeDeletesAsync<TEntity, TKey>(ids);
        return true;
    }
    #endregion

    #region Search
    [HttpPost]
    public virtual async Task<Base.PagedList<TEntityDto>> Search(PageRequest request)
    {
        var list = await _sugarRepository.GetListAsync(request);
        var listDto = list.MapTo<TEntityDto>();

        return listDto.ToPageList(request);
    }
    #endregion

    #region Get
    [HttpGet]
    public virtual async Task<TEntityDto> Get(TKey id)
    {
        var person = await _sugarRepository.GetByIdAsync(id);
        return person.Adapt<TEntityDto>();
    }


    [HttpGet]
    public virtual async Task<List<TEntityDto>> GetAll()
    {
        var list = await _sugarRepository.GetAllAsync();
        return list.MapTo<TEntityDto>();
    }

    [HttpGet]
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

        var persons = _sugarRepository.Context
           .Queryable<TEntity>()
           .Where(where.ToString()).Select(x => x.Adapt<TEntityDto>());
        return await persons.ToListAsync();
    }

    [HttpGet]
    public virtual async Task<Base.PagedList<TEntityDto>> GetPage(int pageIndex = 1, int pageSize = 10)
    {
        var request = new PageRequest() { PageIndex = pageIndex, PageSize = pageSize };
        return await this.Search(request);
    }
    #endregion

    #region Lock
    [HttpPost]
    public virtual async Task<bool> Lock(TKey id, bool isLocked = true)
    {
        var entity = await _sugarRepository.GetByIdAsync(id);
        if (entity != null && entity.SetPropertyValue(nameof(GardenerEntityBase.IsLocked), isLocked))
        {
            entity.SetPropertyValue(nameof(GardenerEntityBase.UpdatedTime), DateTimeOffset.Now);
            await _sugarRepository.UpdateIncludeAsync(entity, new[] { nameof(GardenerEntityBase.IsLocked), nameof(GardenerEntityBase.UpdatedTime) });
            await EntityEventNotityUtil.NotifyLockAsync(entity);
            return true;
        }
        return false;
    }
    #endregion

    #region Seed
    [HttpPost]
    public virtual async Task<string> GenerateSeedData(PageRequest request)
    {
        var pagedList = await _sugarRepository.GetPageAsync(request);

        return SeedDataGenerateTool.Generate(pagedList.Items, typeof(TEntity).Name);
    }
    #endregion

    #region TODO: Import, Export Data
    [HttpPost]
    public virtual async Task<string> Export(PageRequest request)
    {
        var list = await _sugarRepository.GetListAsync(request);

        // MiniExcel
        var memoryStream = new MemoryStream();
        memoryStream.SaveAs(list.MapTo<TEntityDto>());
        memoryStream.Seek(0, SeekOrigin.Begin);
        string fileName = typeof(TEntityDto).GetDescription() + DateTimeOffset.UtcNow.ToLocalTime().ToString("yyyyMMddHHmmss") + ".xlsx";
        var fileService = App.GetService<IFileStoreService>();

        return await fileService.Save(memoryStream, "export/" + fileName);
    }
    #endregion
    #endregion
}
