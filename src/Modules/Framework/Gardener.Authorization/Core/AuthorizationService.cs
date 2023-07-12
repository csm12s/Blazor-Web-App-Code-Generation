// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Core;
using Gardener.Authentication.Dtos;
using Gardener.Authorization.Dtos;
using Gardener.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Gardener.Authorization.Core
{
    /// <summary>
    /// 当前请求的权限管理 
    /// </summary>
    public class AuthorizationService : IAuthorizationService
    {
        /// <summary>
        /// 请求上下文访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// 功能仓储
        /// </summary>
        private readonly IApiEndpointQueryService _apiEndpointStoreService;
        /// <summary>
        /// 身份权限服务
        /// </summary>
        private readonly IIdentityPermissionService _identityPermissionService;
        /// <summary>
        /// 
        /// </summary>
        private readonly IIdentityService _identityService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="apiEndpointStoreService"></param>
        /// <param name="identityPermissionService"></param>
        /// <param name="identityService"></param>
        public AuthorizationService(IHttpContextAccessor httpContextAccessor,
            IApiEndpointQueryService apiEndpointStoreService,
            IIdentityPermissionService identityPermissionService,
            IIdentityService identityService)
        {
            _httpContextAccessor = httpContextAccessor;
            _apiEndpointStoreService = apiEndpointStoreService;
            _identityPermissionService = identityPermissionService;
            _identityService = identityService;
        }


        /// <summary>
        /// 获取当前请求的功能
        /// </summary>
        /// <returns></returns>
        public async Task<ApiEndpoint?> GetApiEndpoint()
        {
            return await GetApiEndpointFromContext();
        }
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ChecktContenxtApiEndpoint()
        {
            Identity? identity = this._identityService.GetIdentity();
            if (identity == null)
            {
                return false;
            }
            //LoginId 已不可用
            if (!await _identityPermissionService.CheckLoginIdUsable(identity.LoginId))
            {
                return false;
            }

            ApiEndpoint? api = await GetApiEndpointFromContext();

            return await _identityPermissionService.Check(identity, api);
        }

        #region private

        /// <summary>
        /// 获取功能Key
        /// </summary>
        /// <returns></returns>
        private string? GetApiEndpointKeyFromContext()
        {
            // 获取权限特性
            var securityDefineAttribute = _httpContextAccessor.HttpContext.GetMetadata<SecurityDefineAttribute>();
            if (securityDefineAttribute != null) return securityDefineAttribute.ResourceId;
            return null;
        }
        /// <summary>
        /// 获取节点信息
        /// </summary>
        /// <returns></returns>
        private (HttpMethod, string) GetContextEndpoint()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                throw new InvalidOperationException("HttpContext is null");
            }
            //没有特性的可以通过路由+请求方法查找
            HttpMethod method = Enum.Parse<HttpMethod>(context.Request.Method.ToUpper());
            var point = context.GetEndpoint();
            if (point == null)
            {
                throw new InvalidOperationException("Endpoint is null");
            }
            string? path = ((Microsoft.AspNetCore.Routing.RouteEndpoint)point).RoutePattern.RawText;
            if (path == null)
            {
                throw new InvalidOperationException("RoutePattern.RawText is null");
            }
            if (!path.StartsWith("/"))
            {
                path = "/" + path;
            }
            return (method, path);
        }

        /// <summary>
        /// 获取当前请求的功能
        /// </summary>
        /// <returns></returns>
        public async Task<ApiEndpoint?> GetApiEndpointFromContext()
        {
            string? functionKey = GetApiEndpointKeyFromContext();
            if (!string.IsNullOrEmpty(functionKey))
            {
                return await _apiEndpointStoreService.Query(functionKey);
            }
            var (method, path) = GetContextEndpoint();
            return await _apiEndpointStoreService.Query(path, method);
        }
        #endregion

        /// <summary>
        /// 获取身份
        /// </summary>
        /// <returns></returns>
        public Identity? GetIdentity()
        {
            return _identityService.GetIdentity();
        }

        /// <summary>
        /// 获取身份的编号
        /// </summary>
        /// <returns></returns>
        public object? GetIdentityId()
        {
            var identity= GetIdentity();
            if (identity == null)
            {
                return null;
            }
            return _identityPermissionService.GetIdentityId(identity);
        }

        /// <summary>
        /// 判断是否是超级管理员
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> IsSuperAdministrator()
        {
            return _identityPermissionService.IsSuperAdministrator(GetIdentity());
        }

        /// <summary>
        /// 判断当前登录对象是否有该资源
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<bool> CheckCurrentIdentityHaveResource(string resourceKey)
        {
            return _identityPermissionService.Check(GetIdentity(),resourceKey);
        }
    }
}