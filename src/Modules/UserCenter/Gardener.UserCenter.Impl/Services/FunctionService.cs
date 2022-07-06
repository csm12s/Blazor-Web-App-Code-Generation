﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.UserCenter.Impl.Domains;
using Gardener.UserCenter.Dtos;
using Gardener.Enums;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Gardener.UserCenter.Services;

namespace Gardener.UserCenter.Impl.Services
{
    /// <summary>
    /// 功能服务
    /// </summary>
    [ApiDescriptionSettings("UserCenterServices")]
    public class FunctionService : ServiceBase<Function, FunctionDto, Guid>, IFunctionService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public FunctionService(IRepository<Function> repository) : base(repository)
        {
        }

        /// <summary>
        /// 启用或禁用
        /// </summary>
        /// <remarks>
        /// 启用或禁用功能
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="enableAudit"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> EnableAudit([ApiSeat(ApiSeats.ActionStart)] Guid id,bool enableAudit = true)
        {
            var entity = await _repository.FindAsync(id);
            if (entity == null) return false;
            entity.EnableAudit = enableAudit;
            await _repository.UpdateIncludeAsync(entity, new[] { nameof(Function.EnableAudit) });
            return true;
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <remarks>
        /// 根据 HttpMethod 和 path 判断是否存在
        /// </remarks>
        /// <param name="method"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> Exists(HttpMethod method, string path)
        {
            path = HttpUtility.UrlDecode(path);

            return await _repository.Where(x => x.Method.Equals(method) && x.Path.Equals(path),tracking:false).AnyAsync();
        }

        /// <summary>
        /// 根据key获取
        /// </summary>
        /// <remarks>
        /// 根据key获取 功能点
        /// </remarks>
        /// <param name="key">key</param>
        /// <returns></returns>
        public async Task<FunctionDto> GetByKey(string key)
        {
            Function function= await _repository.Where(x => x.Key.Equals(key), tracking: false).FirstOrDefaultAsync();
            return function?.Adapt<FunctionDto>();
        }


        /// <summary>
        /// 获取种子数据
        /// </summary>
        /// <remarks>
        /// 获取种子数据
        /// </remarks>
        /// <returns></returns>
        public async Task<string> GetSeedData()
        {
            List<Function> list = await _repository.AsQueryable(false).Where(x => x.IsDeleted == false && x.IsLocked == false).ToListAsync();
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append($"\r\n new {nameof(Function)}()");
                sb.Append("{");
                sb.Append($"{nameof(Function.Id)} = Guid.Parse(\"{item.Id}\"),");
                sb.Append($"{nameof(Function.EnableAudit)}={item.EnableAudit.ToString().ToLower()},");
                sb.Append($"{nameof(Function.Group)}=\"{item.Group}\",");
                sb.Append($"{nameof(Function.Service)}=\"{item.Service}\",");
                sb.Append($"{nameof(Function.Summary)}=\"{item.Summary}\",");
                sb.Append($"{nameof(Function.Key)}=\"{item.Key}\",");
                sb.Append($"{nameof(Function.Description)}=\"{item.Description}\",");
                sb.Append($"{nameof(Function.Path)}=\"{item.Path}\",");
                sb.Append($"{nameof(Function.Method)}=({nameof(HttpMethod)}){(int)item.Method},");
                sb.Append($"{nameof(Function.CreatedTime)}= DateTimeOffset.FromUnixTimeSeconds({DateTimeOffset.Now.ToUnixTimeSeconds()})");
                sb.Append("},");
            }
            return sb.ToString().TrimEnd(',');
        }

    }
}
