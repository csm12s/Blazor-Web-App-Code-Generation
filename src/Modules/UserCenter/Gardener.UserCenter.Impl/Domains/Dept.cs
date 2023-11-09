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
    /// 部门信息
    /// </summary>
    public class Dept : DeptDto, IEntityBase<MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntityTypeBuilder<Dept, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>, IEntitySeedData<Dept, MasterDbContextLocator, GardenerMultiTenantDbContextLocator>
    {
        /// <summary>
        /// 父级
        /// </summary>
        public Dept? Parent { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public new ICollection<Dept>? Children { get; set; }

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
        public void Configure(EntityTypeBuilder<Dept> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
              .HasMany(x => x.Children)
              .WithOne(x => x.Parent)
              .HasForeignKey(x => x.ParentId)
              .OnDelete(DeleteBehavior.ClientSetNull); // 必须设置这一行
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Dept> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new Dept[]
            {
                new Dept{ Id=1,Name="北京分部",Contacts="老A",Tel="400-8888888",Order=1,Remark="北京分部",IsDeleted=false,IsLocked=false,CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311) },
                new Dept{ Id=2,ParentId=1, Name="昌平办事处",Contacts="老B",Tel="400-8888888",Order=1,Remark="昌平办事处",IsDeleted=false,IsLocked=false,CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311) },
                new Dept{ Id=3,ParentId=1,Name="海淀办事处",Contacts="老C",Tel="400-8888888",Order=1,Remark="海淀办事处",IsDeleted=false,IsLocked=false,CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311) },
                new Dept{ Id=4,Name="河北分部",Contacts="老D",Tel="400-8888888",Order=1,Remark="河北分部",IsDeleted=false,IsLocked=false,CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311) },
                new Dept{ Id=5,ParentId=4,Name="石家庄办事处",Contacts="老E",Tel="400-8888888",Order=1,Remark="石家庄办事处",IsDeleted=false,IsLocked=false,CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311) },
            };
        }
    }
}
