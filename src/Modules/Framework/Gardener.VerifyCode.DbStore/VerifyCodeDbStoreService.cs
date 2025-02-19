﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.VerifyCode.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gardener.VerifyCode.DbStore.Domain;
using Gardener.VerifyCode.Enums;

namespace Gardener.VerifyCode.DbStore
{
    /// <summary>
    /// 图片验证码数据库存储服务
    /// </summary>
    public class VerifyCodeDbStoreService : IVerifyCodeStoreService
    {
        private readonly IRepository<VerifyCodeLog> _repository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public VerifyCodeDbStoreService(IRepository<VerifyCodeLog> repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="verifyCodeType"></param>
        /// <param name="key"></param>
        /// <param name="code"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        public async Task Add(VerifyCodeTypeEnum verifyCodeType, string key, string code, TimeSpan expire)
        {
            VerifyCodeLog verifyCode = new VerifyCodeLog();
            verifyCode.VerifyCodeType = verifyCodeType;
            verifyCode.Key = key;
            verifyCode.Code = code;
            verifyCode.EndTime = DateTimeOffset.Now.Add(expire);
            verifyCode.CreatedTime = DateTimeOffset.Now;

            await _repository.InsertAsync(verifyCode);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="verifyCodeType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> GetCode(VerifyCodeTypeEnum verifyCodeType, string key)
        {
            VerifyCodeLog verifyCode = await _repository.AsQueryable(false)
                .Where(x =>x.VerifyCodeType.Equals(verifyCodeType) && x.IsDeleted == false && x.IsLocked == false && x.Key.Equals(key))
                .FirstOrDefaultAsync();
            if (verifyCode == null) 
            {
                return null;
            }
            await _repository.DeleteNowAsync(verifyCode);
            if (verifyCode.EndTime.CompareTo(DateTimeOffset.Now) <= 0)
            {
                return null;
            }
            return verifyCode.Code;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="verifyCodeType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task Remove(VerifyCodeTypeEnum verifyCodeType, string key)
        {
            List<VerifyCodeLog> verifyCodes =await _repository.AsQueryable(false).Where(x => x.VerifyCodeType.Equals(verifyCodeType) && x.Key.Equals(key)).ToListAsync();
            await _repository.DeleteNowAsync(verifyCodes);
        }
    }
}
