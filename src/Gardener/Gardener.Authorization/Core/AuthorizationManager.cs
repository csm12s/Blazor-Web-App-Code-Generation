// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.FriendlyException;
using Gardener.Authorization.Domains;
using Gardener.Authorization.Dtos;
using Gardener.Authorization.Enums;
using Gardener.Authorization.Services;
using Gardener.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gardener.Authorization.Core
{
    /// <summary>
    /// 当前请求的权限管理 
    /// </summary>
    public class AuthorizationManager : IAuthorizationManager
    {
        /// <summary>
        /// 请求上下文访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// 功能仓储
        /// </summary>
        private readonly IFunctionService _functionService;
        /// <summary>
        /// 身份权限服务
        /// </summary>
        private readonly IIdentityPermissionService _identityPermissionService;
        /// <summary>
        /// 当前登录身份
        /// </summary>
        private readonly Identity _identity;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="functionRepository"></param>
        /// <param name="identityPermissionService"></param>
        public AuthorizationManager(IHttpContextAccessor httpContextAccessor,
            IFunctionService functionRepository, 
            IIdentityPermissionService identityPermissionService)
        {
            this._httpContextAccessor = httpContextAccessor;
            if (this._httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                //当前请求的用户
                this._identity = GetIdentityFromContext();

            }
            this._functionService = functionRepository;
            this._identityPermissionService = identityPermissionService;
        }
        
        
        /// <summary>
        /// 获取当前请求的功能
        /// </summary>
        /// <returns></returns>
        public async Task<FunctionDto> GetFunction()
        {
            return await GetFunctionFromContext();
        }
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ChecktContenxtFunction()
        {
            FunctionDto function =await GetFunctionFromContext();

            return await _identityPermissionService.Check(this._identity,function);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Identity GetIdentity()
        {
            return this._identity;
        }

        #region private

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Identity GetIdentityFromContext()
        {
            string tokenTypeKey = _httpContextAccessor.HttpContext.User.FindFirstValue(AuthKeyConstants.TokenTypeKey);
            if (JwtTokenType.RefreshToken.ToString().Equals(tokenTypeKey))
            {
                throw Oops.Oh(ExceptionCode.REFRESHTOKEN_CANNOT_USED_IN_AUTHENTICATION);
            }
            Identity identity = new Identity();
            identity.Id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            identity.Name = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            identity.GivenName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.GivenName);
            string identityType = _httpContextAccessor.HttpContext.User.FindFirstValue(AuthKeyConstants.IdentityType);
            identity.IdentityType = Enum.Parse<IdentityType>(identityType, true);
            identity.ClientId = _httpContextAccessor.HttpContext.User.FindFirstValue(AuthKeyConstants.ClientIdKeyName);
            string loginClientType = _httpContextAccessor.HttpContext.User.FindFirstValue(AuthKeyConstants.ClientTypeKeyName);
            identity.LoginClientType = Enum.Parse<LoginClientType>(loginClientType, true);
            return identity;
        }

        /// <summary>
        /// 获取功能Key
        /// </summary>
        /// <returns></returns>
        private string GetContextFunctionKey()
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
            //没有特性的可以通过路由+请求方法查找
            HttpMethod method =Enum.Parse< HttpMethod>(_httpContextAccessor.HttpContext.Request.Method.ToUpper());
            string path = ((Microsoft.AspNetCore.Routing.RouteEndpoint)_httpContextAccessor.HttpContext.GetEndpoint()).RoutePattern.RawText;
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
        private async Task<FunctionDto> GetFunctionFromContext()
        {
            string functionKey = GetContextFunctionKey();
            if (!string.IsNullOrEmpty(functionKey)) return await _functionService.Get(Guid.Parse(functionKey));
            var (method, path) = GetContextEndpoint();
            return await _functionService.Query(path, method);
        }

        public object GetIdentityId()
        {
            return _identityPermissionService.GetIdentityId(_identity);
        }

        #endregion
    }
}