// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using Gardener.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public abstract class ServiceBase<T> : IServiceBase<T> where T : class, new()
    {
        private string controller;
        private readonly IApiCaller apiCaller;
        protected ServiceBase(IApiCaller apiCaller, string controller)
        {
            this.apiCaller = apiCaller;
            this.controller = controller;
        }

        public async Task<ApiResult<bool?>> Delete(int id)
        {
            return await apiCaller.DeleteAsync<bool?>($"{controller}/{id}");
        }

        public async Task<ApiResult<bool?>> Deletes(int[] ids)
        {
            return await apiCaller.PostAsync<int[], bool?>($"{controller}/deletes", ids);
        }

        public async Task<ApiResult<bool?>> FakeDelete(int id)
        {
            return await apiCaller.DeleteAsync<bool?>($"{controller}/fake-delete/{id}");
        }

        public async Task<ApiResult<bool?>> FakeDeletes(int[] ids)
        {
            return await apiCaller.PostAsync<int[], bool?>($"{controller}/fake-deletes", ids);
        }

        public async Task<ApiResult<T>> Get(int id)
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

        public async Task<ApiResult<bool?>> Lock(int id, bool islocked = true)
        {
            return await apiCaller.PutAsync<object, bool?>($"{controller}/{id}/lock/{islocked}");
        }
    }
}
