using Gardener.Base;
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

public abstract class BaseClientController<T, TKey> 
    : IBaseController<T, TKey>, 
    IServiceBase<T, TKey> // Gardener base controller, _service in ListTableBase need this
    where T : class, new()
{
    public readonly string controller;
    public readonly IApiCaller apiCaller;
    protected BaseClientController(IApiCaller apiCaller, string controller)
    {
        this.apiCaller = apiCaller;
        this.controller = controller;
    }

    //Normal Delete, add in controller: [FromBody] TKey id
    //public virtual Task<bool> DeleteNormal(TKey id)
    //{
    //    var url = $"{controller}/Delete";
    //    return apiCaller.PostAsync<TKey, bool>(url, request: id);
    //}

    public virtual Task<bool> Delete(TKey id)
    {
        var url = $"{controller}/Delete/{id}";
        return apiCaller.PostWithoutBodyAsync<bool>(url);
    }

    public virtual Task<bool> Deletes(TKey[] ids)
    {
        var url = $"{controller}/{System.Reflection.MethodBase.GetCurrentMethod().Name}";
        return apiCaller.PostAsync<TKey[], bool>(url, request: ids);
    }

    public virtual Task<bool> FakeDelete(TKey id)
    {
        var url = $"{controller}/FakeDelete/{id}";
        return apiCaller.PostWithoutBodyAsync<bool>(url);
    }

    public virtual Task<bool> FakeDeletes(TKey[] ids)
    {
        var url = $"{controller}/{System.Reflection.MethodBase.GetCurrentMethod().Name}";
        return apiCaller.PostAsync<TKey[], bool>(url, request: ids);
    }

    public virtual Task<T> Get(TKey id)
    {
        return apiCaller.GetAsync<T>($"{controller}/Get/{id}");
    }

    public virtual Task<List<T>> GetAll()
    {
        var url = $"{controller}/{System.Reflection.MethodBase.GetCurrentMethod().Name}";
        return apiCaller.GetAsync<List<T>>(url);
    }

    public virtual Task<List<T>> GetAllUsable()
    {
        return apiCaller.GetAsync<List<T>>($"{controller}/AllUsable");
    }

    public virtual Task<PagedList<T>> GetPage(int pageIndex = 1, int pageSize = 10)
    {
        return apiCaller.GetAsync<PagedList<T>>($"{controller}/page/{pageIndex}/{pageSize}");
    }

    public virtual Task<T> Insert(T input)
    {
        var url = $"{controller}/{System.Reflection.MethodBase.GetCurrentMethod().Name}";
        return apiCaller.PostAsync<T, T>(url, request: input);
    }

    public virtual Task<bool> Update(T item)
    {
        var url = $"{controller}/{System.Reflection.MethodBase.GetCurrentMethod().Name}";
        return apiCaller.PostAsync<T, bool>(url, request: item);
    }

    
    public virtual Task<bool> Lock(TKey id, bool islocked = true)
    {
        //todo check
        var url = $"{controller}/Lock/{id}/{islocked}";
        return apiCaller.PostWithoutBodyAsync<bool>(url);
    }

    public virtual Task<PagedList<T>> Search(PageRequest request)
    {
        var url = $"{controller}/{System.Reflection.MethodBase.GetCurrentMethod().Name}";
        return apiCaller.PostAsync<PageRequest, PagedList<T>>(url, request);
    }

    //todo check
    public virtual Task<string> GenerateSeedData(PageRequest request)
    {
        var url = $"{controller}/{System.Reflection.MethodBase.GetCurrentMethod().Name}";
        return apiCaller.PostAsync<PageRequest, string>(url, request);
    }

    public virtual async Task<string> Export(PageRequest request)
    {
        var url = $"{controller}/{System.Reflection.MethodBase.GetCurrentMethod().Name}";
        return await apiCaller.PostAsync<PageRequest, string>(url, request);
    }

    // End
}
