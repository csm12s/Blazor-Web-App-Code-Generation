// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base.Entity;
using Gardener.UserCenter.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Gardener.UserCenter.Impl.Domains
{
    /// <summary>
    /// 岗位信息
    /// </summary>
    public class Position : PositionDto, IEntity<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<Position, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntitySeedData<Position, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<User> Users { get; set; } = new List<User>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<Position> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Position> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
            new Position(){ Id=1, Name="董事长",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311) },
            new Position(){ Id=2, Name="总经理",CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311) }
            };
        }
    }
}
