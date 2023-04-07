// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.UserCenter.Impl.Domains
{
    /// <summary>
    /// 岗位信息
    /// </summary>
    [Description("岗位信息")]
    public class Position : GardenerEntityBase, IEntityTypeBuilder<Position>, IEntitySeedData<Position>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required, MaxLength(100)]
        [DisplayName("名称")]
        public string Name { get; set; } = null!;
        /// <summary>
        /// 设置该岗位的目标
        /// </summary>
        [MaxLength(500)]
        [DisplayName("目标")]
        public string? Target { get; set; }

        /// <summary>
        /// 职责
        /// </summary>
        [DisplayName("职责")]
        [MaxLength(500)]
        public string? Duty { get; set; }

        /// <summary>
        /// 权利
        /// </summary>
        [DisplayName("权利")]
        [MaxLength(500)]
        public string? Right { get; set; }

        /// <summary>
        /// 岗位等级
        /// </summary>
        [DisplayName("岗位等级")]
        [MaxLength(500)]
        public string? Grade { get; set; }

        /// <summary>
        /// 岗位薪资
        /// </summary>
        [DisplayName("岗位薪资")]
        [MaxLength(500)]
        public string? Salary { get; set; }


        /// <summary>
        /// 任职资格
        /// </summary>
        [DisplayName("任职资格")]
        [MaxLength(500)]
        public string? Qualifications { get; set; }

        /// <summary>
        /// 多对多
        /// </summary>
        public ICollection<User> Users { get; set; }=new List<User>();
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
