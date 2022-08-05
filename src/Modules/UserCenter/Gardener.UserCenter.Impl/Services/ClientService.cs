﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.DataEncryption;
using Furion.FriendlyException;
using Gardener.Attributes;
using Gardener.Authentication.Core;
using Gardener.Authentication.Dtos;
using Gardener.Authentication.Enums;
using Gardener.Authorization.Dtos;
using Gardener.Enums;
using Gardener.SystemManager.Dtos;
using Gardener.UserCenter.Dtos;
using Gardener.UserCenter.Impl.Domains;
using Gardener.UserCenter.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.UserCenter.Impl.Services
{
    /// <summary>
    /// 客户端服务
    /// </summary>
    [ApiDescriptionSettings("UserCenterServices")]
    public class ClientService : ServiceBase<Client, ClientDto, Guid>, IClientService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtService _jwtBearerService;
        private readonly IRepository<ClientFunction> _clientFunctionRespository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jwtBearerService"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="repository"></param>
        /// <param name="clientFunctionRespository"></param>
        public ClientService(IJwtService jwtBearerService, IHttpContextAccessor httpContextAccessor, IRepository<Client> repository, IRepository<ClientFunction> clientFunctionRespository) : base(repository)
        {
            _jwtBearerService = jwtBearerService;
            _httpContextAccessor = httpContextAccessor;
            _clientFunctionRespository = clientFunctionRespository;
        }
        /// <summary>
        /// 根据客户端编号获取绑定的接口列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<List<FunctionDto>> GetFunctions([ApiSeat(ApiSeats.ActionStart)] Guid id)
        {
            return await _clientFunctionRespository.AsQueryable(false)
                  .Include(x => x.Function)
                  .Where(x => x.ClientId.Equals(id))
                  .Select(x => x.Function)
                  .Where(x => x.IsDeleted == false && x.IsLocked == false)
                  .Select(x => x.Adapt<FunctionDto>())
                  .ToListAsync();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <remarks>登录接口</remarks>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<TokenOutput> Login(ClientLoginInput input)
        {
            long currentTimespan = DateTimeOffset.Now.ToUnixTimeSeconds();

            //校验时间戳
            if (input.Timespan <= 0 || input.Timespan > currentTimespan || input.Timespan < (currentTimespan - 120)) 
            {
                throw Oops.Bah(ExceptionCode.TIMESPAN_IS_EXPIRED);
            }

            Client client = _repository.AsQueryable(false).Where(x => x.Id.Equals(input.ClientId) && x.IsDeleted == false && x.IsLocked == false).FirstOrDefault();
            if (client == null)
            {
                throw Oops.Bah(ExceptionCode.CLIENT_NO_FIND);
            }

            //加密对比
            bool flag= MD5Encryption.Compare((input.ClientId + client.SecretKey + input.Timespan).ToUpper(), input.EncryptionValue.ToUpper(), true);
            if (!flag)
            {
                throw Oops.Bah(ExceptionCode.CLIENT_LOGIN_FAIL);
            }

            Identity identity = new Identity
            {
                Id = client.Id.ToString(),
                LoginId = Guid.NewGuid().ToString(),
                LoginClientType = LoginClientType.Server,
                IdentityType = IdentityType.Client,
                Name = client.Name,
                GivenName = client.Name
            };

            var token = await _jwtBearerService.CreateToken(identity);
            // 设置 Swagger 刷新自动授权
            _httpContextAccessor.HttpContext.SigninToSwagger(token.AccessToken);
            return token.Adapt<TokenOutput>();
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <remarks>
        /// 通过刷新token获取新的token
        /// </remarks>
        /// <returns></returns>
        [AllowAnonymous, IgnoreAudit]
        public async Task<TokenOutput> RefreshToken(RefreshTokenInput input)
        {
            var token = await _jwtBearerService.RefreshToken(input.RefreshToken);
            return token.Adapt<TokenOutput>();
        }
    }
}
