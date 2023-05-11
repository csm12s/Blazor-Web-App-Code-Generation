// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gardener.Client.Base
{
    /// <summary>
    /// 客户端服务基类
    /// </summary>
    /// <typeparam name="T">Dto</typeparam>
    /// <remarks>
    /// 实体主键类型是<see cref="int"/>
    /// </remarks>
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

        public virtual Task<bool> Delete(Tkey id)
        {
            return apiCaller.DeleteAsync<bool>($"{controller}/{id}");
        }

        public virtual Task<bool> Deletes(Tkey[] ids)
        {
            return apiCaller.PostAsync<Tkey[], bool>($"{controller}/deletes", ids);
        }

        public virtual Task<bool> FakeDelete(Tkey id)
        {
            return apiCaller.DeleteAsync<bool>($"{controller}/fake-delete/{id}");
        }

        public virtual Task<bool> FakeDeletes(Tkey[] ids)
        {
            return apiCaller.PostAsync<Tkey[], bool>($"{controller}/fake-deletes", ids);
        }

        public virtual Task<T> Get(Tkey id)
        {
            return apiCaller.GetAsync<T>($"{controller}/{id}");
        }

        public virtual Task<List<T>> GetAll()
        {
            return apiCaller.GetAsync<List<T>>($"{controller}/all");
        }

        public virtual Task<PagedList<T>> GetPage(int pageIndex = 1, int pageSize = 10)
        {
            return apiCaller.GetAsync<PagedList<T>>($"{controller}/page/{pageIndex}/{pageSize}");
        }

        public virtual Task<T> Insert(T input)
        {
            return apiCaller.PostAsync<T, T>(controller, request: input);
        }

        public virtual Task<bool> Update(T input)
        {
            return apiCaller.PutAsync<T, bool>(controller, request: input);
        }
        public virtual Task<bool> Lock(Tkey id, bool islocked = true)
        {
            return apiCaller.PutAsync<object, bool>($"{controller}/{id}/lock/{islocked}");
        }

        public virtual Task<List<T>> GetAllUsable(Guid? tenantId = null)
        {
            IDictionary<string, object?> queryString = new Dictionary<string, object?>();
            queryString.Add($"{nameof(tenantId)}", tenantId);
            return apiCaller.GetAsync<List<T>>($"{controller}/all-usable", queryString);
        }

        public virtual Task<PagedList<T>> Search(PageRequest request)
        {
            return apiCaller.PostAsync<PageRequest, PagedList<T>>($"{controller}/search",request);
        }

        public virtual Task<string> GenerateSeedData(PageRequest request)
        {
            return apiCaller.PostAsync<PageRequest, string>($"{controller}/generate-seed-data", request);
        }

        public virtual Task<string> Export(PageRequest request)
        {
            return apiCaller.PostAsync<PageRequest, string>($"{controller}/export", request);
        }
    }
}
