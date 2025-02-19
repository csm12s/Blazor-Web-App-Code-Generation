﻿using Gardener.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Base;

public abstract class BaseClientController<T>
    : BaseClientController<T, int> where T : class, new()
{
    protected BaseClientController(IApiCaller apiCaller, string controller) : base(apiCaller, controller)
    {
    }
}

public abstract class BaseClientController<TEntityDto, TKey>
    : IBaseController<TEntityDto, TKey>,
    IServiceBase<TEntityDto, TKey> // Gardener base controller, _service in ListTableBase need this
    where TEntityDto : class, new()
{
    public readonly string controller;
    public readonly IApiCaller apiCaller;
    protected BaseClientController(IApiCaller apiCaller, string controller)
    {
        this.apiCaller = apiCaller;
        this.controller = controller;
    }

    //Normal Delete, add in controller: [FromBody] TData id
    //public virtual Task<bool> DeleteNormal(TData id)
    //{
    //    var url = $"{controller}/Delete";
    //    return apiCaller.PostAsync<TData, bool>(url, request: id);
    //}

    public virtual Task<bool> Delete(TKey id)
    {
        var url = $"{controller}/Delete/{id}";
        return apiCaller.PostWithoutBodyAsync<bool>(url);
    }

    public virtual Task<bool> Deletes(TKey[] ids)
    {
        var url = $"{controller}/{nameof(Deletes)}";
        // 不支持异步方法:
        //var url = $"{controller}/{System.Reflection.MethodBase.GetCurrentMethod().Name}";
        return apiCaller.PostAsync<TKey[], bool>(url, request: ids);
    }

    public virtual Task<bool> FakeDelete(TKey id)
    {
        var url = $"{controller}/FakeDelete/{id}";
        return apiCaller.PostWithoutBodyAsync<bool>(url);
    }

    public virtual Task<bool> FakeDeletes(TKey[] ids)
    {
        var url = $"{controller}/{System.Reflection.MethodBase.GetCurrentMethod()?.Name}";
        return apiCaller.PostAsync<TKey[], bool>(url, request: ids);
    }

    public virtual Task<TEntityDto> Get(TKey id)
    {
        var url = $"{controller}/Get/{id}";
        return apiCaller.GetAsync<TEntityDto>(url);
    }

    public virtual Task<List<TEntityDto>> GetAll()
    {
        var url = $"{controller}/{System.Reflection.MethodBase.GetCurrentMethod()?.Name}";
        return apiCaller.GetAsync<List<TEntityDto>>(url);
    }
    /// <summary>
    /// 查询所有可以用的
    /// </summary>
    /// <param name="tenantId">租户编号</param>
    /// <param name="includLocked">是否包含锁定的</param>
    /// <remarks>
    /// 查询所有可以用的记录，(实现<see cref="IModelDeleted"/> <see cref="IModelLocked"/>时会自动过滤)
    /// </remarks>
    /// <returns></returns>
    public virtual Task<List<TEntityDto>> GetAllUsable(Guid? tenantId = null, bool includLocked = false)
    {
        IDictionary<string, object?> queryString = new Dictionary<string, object?>();
        queryString.Add($"{nameof(tenantId)}", tenantId);
        queryString.Add($"{nameof(includLocked)}", includLocked);
        var url = $"{controller}/{System.Reflection.MethodBase.GetCurrentMethod()?.Name}";
        return apiCaller.GetAsync<List<TEntityDto>>(url, queryString);
    }

    public virtual Task<PagedList<TEntityDto>> GetPage(int pageIndex = 1, int pageSize = 10)
    {
        var url = $"{controller}/GetPage/{pageIndex}/{pageSize}";
        return apiCaller.GetAsync<PagedList<TEntityDto>>(url);
    }

    public virtual Task<TEntityDto> Insert(TEntityDto input)
    {
        var url = $"{controller}/{System.Reflection.MethodBase.GetCurrentMethod()?.Name}";
        return apiCaller.PostAsync<TEntityDto, TEntityDto>(url, request: input);
    }

    public virtual Task<bool> Update(TEntityDto item)
    {
        var url = $"{controller}/{System.Reflection.MethodBase.GetCurrentMethod()?.Name}";
        return apiCaller.PostAsync<TEntityDto, bool>(url, request: item);
    }


    public virtual Task<bool> Lock(TKey id, bool islocked = true)
    {
        var url = $"{controller}/Lock/{id}/{islocked}";
        return apiCaller.PostWithoutBodyAsync<bool>(url);
    }

    public virtual Task<PagedList<TEntityDto>> Search(PageRequest request)
    {
        var url = $"{controller}/Search";
        return apiCaller.PostAsync<PageRequest, PagedList<TEntityDto>>(url, request);
    }

    public virtual Task<List<TEntityDto>> GetList(PageRequest request)
    {
        var url = $"{controller}/GetList";
        return apiCaller.PostAsync<PageRequest, List<TEntityDto>>(url, request);
    }

    public virtual Task<string> GenerateSeedData(PageRequest request)
    {
        var url = $"{controller}/{System.Reflection.MethodBase.GetCurrentMethod()?.Name}";
        return apiCaller.PostAsync<PageRequest, string>(url, request);
    }

    public virtual async Task<string> Export(PageRequest request)
    {
        var url = $"{controller}/{System.Reflection.MethodBase.GetCurrentMethod()?.Name}";
        return await apiCaller.PostAsync<PageRequest, string>(url, request);
    }

    // End
}
