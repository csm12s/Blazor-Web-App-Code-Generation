// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Core.Entites
{
    /// <summary>
    /// 部门信息
    /// </summary>
    [Description("部门信息")]
    public class Dept: GardenerEntityBase, IEntityTypeBuilder<Dept>, IEntitySeedData<Dept>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Required, MaxLength(30)]
        public string Name { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [DisplayName("联系人")]
        [MaxLength(20)]
        public string Contacts { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [DisplayName("电话")]
        [MaxLength(20)]
        public string Tel { get; set; }

        /// <summary>
        /// 资源排序
        /// </summary>
        [Required, DefaultValue(0)]
        [DisplayName("排序")]
        public int Order { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [MaxLength(100)]
        public string Remark { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        [DisplayName("父级编号")]
        public int? ParentId { get; set; }

        /// <summary>
        /// 父级
        /// </summary>
        public Dept Parent { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public ICollection<Dept> Children { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<User> Users { get; set; }
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

            entityBuilder
              .HasMany(x => x.Users)
              .WithOne(x => x.Dept)
              .HasForeignKey(x => x.DeptId);
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
                new Dept{ Id=1,Name="珠穆拉玛峰分部",Contacts="凡尔赛",Tel="400-8888888",Order=1,Remark="这是我们的大本营",IsDeleted=false,IsLocked=false,CreatedTime=DateTimeOffset.UtcNow }
            };
        }
    }
}
