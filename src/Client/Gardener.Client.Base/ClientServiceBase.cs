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
    /// <para>继承后，实现 <see cref="IServiceBase{T,int}"/> 基础方法 </para>
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
    /// <remarks>
    /// <para>继承后，实现 <see cref="IServiceBase{T,Tkey}"/> 基础方法 </para>
    /// </remarks>
    public abstract class ClientServiceBase<T,Tkey> : IServiceBase<T, Tkey> where T : class, new()
    {
        public readonly string controller;
        public readonly IApiCaller apiCaller;
        /// <summary>
        /// 客户端服务基类
        /// </summary>
        /// <param name="apiCaller"></param>
        /// <param name="controller"></param>
        protected ClientServiceBase(IApiCaller apiCaller, string controller)
        {
            this.apiCaller = apiCaller;
            this.controller = controller;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// 根据主键删除一条数据
        /// </remarks>
        /// <returns></returns>
        public virtual Task<bool> Delete(Tkey id)
        {
            return apiCaller.DeleteAsync<bool>($"{controller}/{id}");
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <remarks>
        /// 根据多个主键批量删除
        /// </remarks>
        /// <returns></returns>
        public virtual Task<bool> Deletes(Tkey[] ids)
        {
            return apiCaller.PostAsync<Tkey[], bool>($"{controller}/deletes", ids);
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// 根据主键逻辑删除
        /// </remarks>
        /// <returns></returns>
        public virtual Task<bool> FakeDelete(Tkey id)
        {
            return apiCaller.DeleteAsync<bool>($"{controller}/fake-delete/{id}");
        }
        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="ids"></param>
        /// <remarks>
        /// 根据多个主键批量逻辑删除
        /// </remarks>
        /// <returns></returns>
        public virtual Task<bool> FakeDeletes(Tkey[] ids)
        {
            return apiCaller.PostAsync<Tkey[], bool>($"{controller}/fake-deletes", ids);
        }
        /// <summary>
        /// 根据主键获取
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>
        /// 根据主键查找一条数据
        /// </remarks>
        /// <returns></returns>
        public virtual Task<T> Get(Tkey id)
        {
            return apiCaller.GetAsync<T>($"{controller}/{id}");
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <remarks>
        /// 查找到所有数据
        /// </remarks>
        /// <returns></returns>
        public virtual Task<List<T>> GetAll()
        {
            return apiCaller.GetAsync<List<T>>($"{controller}/all");
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <remarks>分页返回数据</remarks>
        /// <returns></returns>
        public virtual Task<PagedList<T>> GetPage(int pageIndex = 1, int pageSize = 10)
        {
            return apiCaller.GetAsync<PagedList<T>>($"{controller}/page/{pageIndex}/{pageSize}");
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>
        /// 添加一条数据
        /// </remarks>
        /// <returns></returns>
        public virtual Task<T> Insert(T input)
        {
            return apiCaller.PostAsync<T, T>(controller, request: input);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>
        /// 更新一条数据
        /// </remarks>
        /// <returns></returns>
        public virtual Task<bool> Update(T input)
        {
            return apiCaller.PutAsync<T, bool>(controller, request: input);
        }
        /// <summary>
        /// 锁定
        /// </summary>
        /// <param name="id"></param>
        /// <param name="islocked"></param>
        /// <remarks>
        /// 根据主键锁定或解锁数据（必须实现<see cref="IModelLocked"/>才能生效）
        /// </remarks>
        /// <returns></returns>
        public virtual Task<bool> Lock(Tkey id, bool islocked = true)
        {
            return apiCaller.PutAsync<object, bool>($"{controller}/{id}/lock/{islocked}");
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
        public virtual Task<List<T>> GetAllUsable(Guid? tenantId = null, bool includLocked = false)
        {
            IDictionary<string, object?> queryString = new Dictionary<string, object?>();
            queryString.Add($"{nameof(tenantId)}", tenantId);
            queryString.Add($"{nameof(includLocked)}", includLocked);
            return apiCaller.GetAsync<List<T>>($"{controller}/all-usable", queryString);
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        /// 搜索功能数据
        /// </remarks>
        /// <returns></returns>
        public virtual Task<PagedList<T>> Search(PageRequest request)
        {
            return apiCaller.PostAsync<PageRequest, PagedList<T>>($"{controller}/search",request);
        }
        /// <summary>
        /// 生成种子数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <remarks>
        /// 根据搜索条叫生成种子数据
        /// </remarks>
        public virtual Task<string> GenerateSeedData(PageRequest request)
        {
            return apiCaller.PostAsync<PageRequest, string>($"{controller}/generate-seed-data", request);
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        /// 导出搜索结果
        /// </remarks>
        /// <returns></returns>
        public virtual Task<string> Export(PageRequest request)
        {
            return apiCaller.PostAsync<PageRequest, string>($"{controller}/export", request);
        }
    }
}
