using Gardener.Base;
using Gardener.Common;
using Gardener.FileStore;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Text;
using Mapster;
using Furion.DatabaseAccessor;
using Furion;
using MiniExcelLibs;
using Furion.DependencyInjection;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace Gardener;

/// <summary>
/// BaseController，用于API转发
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TEntityDto"></typeparam>
/// <typeparam name="TKey"></typeparam>
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

    #region Gardener API

    #region Insert
    [HttpPost]
    public virtual async Task<TEntityDto> Insert(TEntityDto input)
    {
        var entity = input.Adapt<TEntity>();
        var newEntity = await _baseService.InsertAsync(entity);
        return newEntity.Adapt<TEntityDto>();
    }
    #endregion

    #region Update
    [HttpPost]
    public virtual async Task<bool> Update(TEntityDto input)
    {
        var entity = input.Adapt<TEntity>();
        return await _baseService.UpdateAsync(entity);
    }

    #endregion

    #region Delete
    [HttpPost]
    public virtual async Task<bool> Delete(TKey id)
    {
        return await _baseService.DeleteAsync(id);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "批量删除", Description = "根据多个主键批量删除")]
    public virtual async Task<bool> Deletes([FromBody] TKey[] ids)
    {
        return await _baseService.DeletesAsync(ids);
    }

    [HttpPost]
    public virtual async Task<bool> FakeDelete(TKey id)
    {
        return await (_baseService.FakeDeleteAsync(id));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "批量逻辑删除", Description = "根据多个主键批量逻辑删除")]
    public virtual async Task<bool> FakeDeletes([FromBody] TKey[] ids)
    {
        return await _baseService.FakeDeletesAsync(ids);
    }
    #endregion

    #region Search
    [HttpPost]
    public virtual async Task<Base.PagedList<TEntityDto>> Search(PageRequest request)
    {
        var list = await _baseService.GetListAsync(request);
        var listDto = list.MapTo<TEntityDto>();

        // 这里进行数据操作

        return listDto.ToPageList(request);
    }
    #endregion

    #region Get
    [HttpGet]
    public virtual async Task<TEntityDto> Get(TKey id)
    {
        var entity = await _baseService.GetByIdAsync(id);
        return entity.Adapt<TEntityDto>();
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
        var list = await _baseService.GetAllUsableAsync();
        return list.MapTo<TEntityDto>();
    }

    [HttpGet]
    public virtual async Task<Base.PagedList<TEntityDto>> GetPage(int pageIndex = 1, int pageSize = 10)
    {
        var list = await _baseService.GetAllAsync();
        var listDto = list.MapTo<TEntityDto>();

        var request = new PageRequest() { PageIndex = pageIndex, PageSize = pageSize };
        return listDto.ToPageList(request);
    }
    #endregion

    #region Lock
    [HttpPost]
    public virtual async Task<bool> Lock(TKey id, bool isLocked = true)
    {
        return await _baseService.LockAsync(id, isLocked);
    }
    #endregion

    #region Seed
    [HttpPost]
    public virtual async Task<string> GenerateSeedData(PageRequest request)
    {
        var list = await _baseService.GetListAsync(request);
        var pagedList = list.ToPageList(request);

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
