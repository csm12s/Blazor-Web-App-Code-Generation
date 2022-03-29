// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    /// <summary>
    /// 客户端服务基类
    /// </summary>
    /// <typeparam name="T">Dto</typeparam>
    public abstract class ClientServiceBase<T> : ClientServiceBase<T, int> where T : class, new()
    {
        /// <summary>
        /// 客户端服务基类
        /// </summary>
        /// <param name="apiCaller"></param>
        /// <param name="controller"></param>
        protected ClientServiceBase(IApiCaller apiCaller, string controller) : base(apiCaller, controller)
        {
        }
    }
    /// <summary>
    /// 客户端服务基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Tkey"></typeparam>
    public abstract class ClientServiceBase<T,Tkey> : IServiceBase<T, Tkey> where T : class, new()
    {
        public readonly string controller;
        public readonly IApiCaller apiCaller;
        protected ClientServiceBase(IApiCaller apiCaller, string controller)
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
            return await apiCaller.GetAsync<List<T>>($"{controller}/all");
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
        public async Task<bool> Lock(Tkey id, bool islocked = true)
        {
            return await apiCaller.PutAsync<object, bool>($"{controller}/{id}/lock/{islocked}");
        }

        public async Task<List<T>> GetAllUsable()
        {
            return await apiCaller.GetAsync<List<T>>($"{controller}/all-usable");
        }

        public async Task<PagedList<T>> Search(PageRequest request)
        {
            return await apiCaller.PostAsync<PageRequest, PagedList<T>>($"{controller}/search",request);
        }

        public async Task<string> GenerateSeedData(PageRequest request)
        {
            return await apiCaller.PostAsync<PageRequest, string>($"{controller}/generate-seed-data", request);
        }
    }
}
