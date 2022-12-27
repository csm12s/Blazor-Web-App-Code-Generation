// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base.Entity;
using Microsoft.EntityFrameworkCore;
using HttpMethod = Gardener.Enums.HttpMethod;

namespace Gardener.SystemManager.SeedDatas
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class ResourceFunctionServiceFunctionSeedData : IEntitySeedData<Function>
    {
        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Function> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
            new Function()
 {Group="用户中心服务",Service="资源与接口关系服务",Summary="添加资源与接口关系",Key="43844F96A173330CECD6470FD62A8A76",Description="",Path="/api/resource-function",Method=Enum.Parse<HttpMethod>("POST"),EnableAudit=true,Id=Guid.Parse("c1e7fa06-b759-4bb0-9545-7265e3798d28"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function()
 {Group="用户中心服务",Service="资源与接口关系服务",Summary="获取种子数据",Key="DDE05A70BD80F948C9AEAFB9708090F3",Description="",Path="/api/resource-function/seed-data",Method=Enum.Parse<HttpMethod>("GET"),EnableAudit=false,Id=Guid.Parse("c56d6a82-abc8-4b17-bc28-27b1904116c9"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
                new Function()
 {Group="用户中心服务",Service="资源与接口关系服务",Summary="删除资源与接口关系",Key="FE150D4F1EE3DDDE5BD78C718100A247",Description="",Path="/api/resource-function/{resourceid}/{functionid}",Method=Enum.Parse<HttpMethod>("DELETE"),EnableAudit=true,Id=Guid.Parse("ffef6a8e-3f80-4a39-97c6-5b2b81582830"),IsLocked=false,IsDeleted=false,CreatedTime=DateTimeOffset.Parse("2022-08-08 08:08:08"),},
            };
        }
    }

}
