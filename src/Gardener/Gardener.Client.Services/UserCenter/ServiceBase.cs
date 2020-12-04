// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public abstract class ServiceBase<T> : ServiceBase<T, int> where T : class, new()
    {
        protected ServiceBase(IApiCaller apiCaller, string controller) : base(apiCaller, controller)
        {
        }
    }
    public abstract class ServiceBase<T,Tkey> : IServiceBase<T, Tkey> where T : class, new()
    {
        private string controller;
        private readonly IApiCaller apiCaller;
        protected ServiceBase(IApiCaller apiCaller, string controller)
        {
            this.apiCaller = apiCaller;
            this.controller = controller;
        }

        public async Task<ApiResult<bool?>> Delete(Tkey id)
        {
            return await apiCaller.DeleteAsync<bool?>($"{controller}/{id}");
        }

        public async Task<ApiResult<bool?>> Deletes(Tkey[] ids)
        {
            return await apiCaller.PostAsync<Tkey[], bool?>($"{controller}/deletes", ids);
        }

        public async Task<ApiResult<bool?>> FakeDelete(Tkey id)
        {
            return await apiCaller.DeleteAsync<bool?>($"{controller}/fake-delete/{id}");
        }

        public async Task<ApiResult<bool?>> FakeDeletes(Tkey[] ids)
        {
            return await apiCaller.PostAsync<Tkey[], bool?>($"{controller}/fake-deletes", ids);
        }

        public async Task<ApiResult<T>> Get(Tkey id)
        {
            return await apiCaller.GetAsync<T>($"{controller}/{id}");
        }

        public async Task<ApiResult<List<T>>> GetAll()
        {
            return await apiCaller.GetAsync<List<T>>($"{controller}");
        }

        public async Task<ApiResult<PagedList<T>>> GetPage(int pageIndex = 1, int pageSize = 10)
        {
            return await apiCaller.GetAsync<PagedList<T>>($"{controller}/page/{pageIndex}/{pageSize}");
        }

        public async Task<ApiResult<T>> Insert(T input)
        {
            return await apiCaller.PostAsync<T, T>(controller, request: input);
        }

        public async Task<ApiResult<bool?>> Update(T input)
        {
            return await apiCaller.PutAsync<T, bool?>(controller, request: input);
        }

        public async Task<ApiResult<bool?>> Lock(Tkey id, bool islocked = true)
        {
            return await apiCaller.PutAsync<object, bool?>($"{controller}/{id}/lock/{islocked}");
        }
    }
}
