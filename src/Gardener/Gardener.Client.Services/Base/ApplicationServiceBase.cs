// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Application.Dtos;
using Gardener.Application.Interfaces;
using Gardener.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Services
{
    public abstract class ApplicationServiceBase<T> : ApplicationServiceBase<T, int> where T : class, new()
    {
        protected ApplicationServiceBase(IApiCaller apiCaller, string controller) : base(apiCaller, controller)
        {
        }
    }
    public abstract class ApplicationServiceBase<T,Tkey> : IApplicationServiceBase<T, Tkey> where T : class, new()
    {
        private string controller;
        private readonly IApiCaller apiCaller;
        protected ApplicationServiceBase(IApiCaller apiCaller, string controller)
        {
            this.apiCaller = apiCaller;
            this.controller = controller;
        }

        public async Task<bool> Delete(Tkey id)
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/{id}");
        }

        public async Task<bool> Deletes(Tkey[] ids)
        {
            return await apiCaller.PostAsync<Tkey[], bool>($"{controller}/deletes", ids);
        }

        public async Task<bool> FakeDelete(Tkey id)
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/fake-delete/{id}");
        }

        public async Task<bool> FakeDeletes(Tkey[] ids)
        {
            return await apiCaller.PostAsync<Tkey[], bool>($"{controller}/fake-deletes", ids);
        }

        public async Task<T> Get(Tkey id)
        {
            return await apiCaller.GetAsync<T>($"{controller}/{id}");
        }

        public async Task<List<T>> GetAll()
        {
            return await apiCaller.GetAsync<List<T>>($"{controller}");
        }

        public async Task<PagedList<T>> GetPage(int pageIndex = 1, int pageSize = 10)
        {
            return await apiCaller.GetAsync<PagedList<T>>($"{controller}/page/{pageIndex}/{pageSize}");
        }

        public async Task<T> Insert(T input)
        {
            return await apiCaller.PostAsync<T, T>(controller, request: input);
        }

        public async Task<bool> Update(T input)
        {
            return await apiCaller.PutAsync<T, bool>(controller, request: input);
        }

    }
}
