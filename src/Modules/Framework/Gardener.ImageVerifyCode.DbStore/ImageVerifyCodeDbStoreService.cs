// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.ImageVerifyCode.Core;
using Gardener.ImageVerifyCode.DbStore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.ImageVerifyCode.DbStore
{
    /// <summary>
    /// 图片验证码数据库存储服务
    /// </summary>
    public class ImageVerifyCodeDbStoreService : IImageVerifyCodeStoreService
    {
        private readonly IRepository<VerifyCode> _repository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public ImageVerifyCodeDbStoreService(IRepository<VerifyCode> repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="code"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        public async Task Add(string key, string code, TimeSpan expire)
        {
            VerifyCode verifyCode = new VerifyCode();
            verifyCode.Key = key;
            verifyCode.Code = code;
            verifyCode.EndTime = DateTimeOffset.Now.Add(expire);
            verifyCode.CreatedTime = DateTimeOffset.Now;

            await _repository.InsertAsync(verifyCode);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> GetCode(string key)
        {
            VerifyCode verifyCode = await _repository.AsQueryable(false)
                .Where(x => x.IsDeleted == false && x.IsLocked == false && x.Key.Equals(key))
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
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task Remove(string key)
        {
            List<VerifyCode> verifyCodes =await _repository.AsQueryable(false).Where(x => x.Key.Equals(key)).ToListAsync();
            await _repository.DeleteNowAsync(verifyCodes);
        }
    }
}
