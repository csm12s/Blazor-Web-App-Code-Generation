using Gardener.Base;
using Gardener.Common;
using Gardener.FileStore;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Furion.DatabaseAccessor;
using Furion;
using MiniExcelLibs;
using Furion.DependencyInjection;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace Gardener;

public abstract partial class BaseController<TEntity, TEntityDto, TKey> :
    IBaseController<TEntityDto, TKey>, ITransient
    where TEntity : class, IPrivateEntity, new()
    where TEntityDto : class, new()
{
    #region Init
    public readonly IBaseService<TEntity> _baseService;

    protected BaseController(IBaseService<TEntity> service)
    {
        this._baseService = service;
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
            ge1.CreatorId = IdentityUtil.GetIdentityId();
            ge1.CreatorIdentityType = IdentityUtil.GetIdentityType();
        }
        var newEntity = await _baseService.InsertReturnEntityAsync(entity);
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
        var res = await _baseService
            .UpdateExcludeAsync(input.Adapt<TEntity>());
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
        await _baseService.DeleteByIdAsync(id);
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
            await _baseService.DeleteByIdAsync(id);

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
        await _baseService.FakeDeleteByIdAsync(id);
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
            await _baseService.FakeDeleteByIdAsync(id);
        }
        await EntityEventNotityUtil.NotifyFakeDeletesAsync<TEntity, TKey>(ids);
        return true;
    }
    #endregion

    #region Search
    [HttpPost]
    public virtual async Task<Base.PagedList<TEntityDto>> Search(PageRequest request)
    {
        var list = await _baseService.GetListAsync(request);
        var listDto = list.MapTo<TEntityDto>();

        return listDto.ToPageList(request);
    }
    #endregion

    #region Get
    [HttpGet]
    public virtual async Task<TEntityDto> Get(TKey id)
    {
        var person = await _baseService.GetByIdAsync(id);
        return person.Adapt<TEntityDto>();
    }


    [HttpGet]
    public virtual async Task<List<TEntityDto>> GetAll()
    {
        var list = await _baseService.GetAllAsync();
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

        var persons = _baseService.GetReadableRepository()
            .AsQueryable().Where(where.ToString()).Select(x => x.Adapt<TEntityDto>());
        return await persons.ToListAsync();

        //var persons = _baseService.GetSugarContext()
        //   .Queryable<TEntity>()
        //   .Where(where.ToString()).Select(x => x.Adapt<TEntityDto>());
        //return await persons.ToListAsync();
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
        var entity = await _baseService.GetByIdAsync(id);
        if (entity != null && entity.SetPropertyValue(nameof(GardenerEntityBase.IsLocked), isLocked))
        {
            entity.SetPropertyValue(nameof(GardenerEntityBase.UpdatedTime), DateTimeOffset.Now);
            await _baseService.UpdateIncludeAsync(entity, new[] { nameof(GardenerEntityBase.IsLocked), nameof(GardenerEntityBase.UpdatedTime) });
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
        //var pagedList = await this.Search(request);
        var pagedList = await _baseService.GetPageAsync(request);

        return SeedDataGenerateTool.Generate(pagedList.Items, typeof(TEntity).Name);
    }
    #endregion

    #region TODO: Import, Export Data
    [HttpPost]
    public virtual async Task<string> Export(PageRequest request)
    {
        var list = await _baseService.GetListAsync(request);

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
