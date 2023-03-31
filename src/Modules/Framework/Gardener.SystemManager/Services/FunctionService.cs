// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.FriendlyException;
using Gardener.Base;
using Gardener.Base.Entity;
using Gardener.EntityFramwork;
using Gardener.SystemManager.Dtos;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;
using HttpMethod = Gardener.Enums.HttpMethod;

namespace Gardener.SystemManager.Services
{
    /// <summary>
    /// 功能服务
    /// </summary>
    [ApiDescriptionSettings("SystemBaseServices")]
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
            entity.UpdatedTime = DateTimeOffset.UtcNow;
            await _repository.UpdateIncludeAsync(entity, new[] { nameof(Function.EnableAudit), nameof(Function.UpdatedTime) });
            //发送通知
            await EntityEventNotityUtil.NotifyUpdateAsync(entity);
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

            return await _repository.Where(x => x.Method.Equals(method) && path.Equals(x.Path),tracking:false).AnyAsync();
        }

        /// <summary>
        /// 根据key获取
        /// </summary>
        /// <remarks>
        /// 根据key获取 功能点
        /// </remarks>
        /// <param name="key">key</param>
        /// <returns></returns>
        public async Task<FunctionDto?> GetByKey(string key)
        {
            Function? function= await _repository.Where(x => x.Key.Equals(key), tracking: false).FirstOrDefaultAsync();
            return function?.Adapt<FunctionDto>();
        }
    }
}
