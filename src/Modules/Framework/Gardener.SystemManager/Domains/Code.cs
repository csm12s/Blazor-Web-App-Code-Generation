// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using Gardener.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.SystemManager.Domains
{
    /// <summary>
    /// 字典信息
    /// </summary>
    [Description("Code")]
    public class Code : GardenerEntityBase, IEntitySeedData<Code>
    {
        /// <summary>
        /// 字段类型编号
        /// </summary>
        [DisplayName("CodeTypeId")]
        [Required]
        public int CodeTypeId { get; set; }
        /// <summary>
        /// 字典值
        /// </summary>
        [DisplayName("CodeValue")]
        [Required, MaxLength(50)]
        public string CodeValue { get; set; } = null!;
        /// <summary>
        /// 字典名称
        /// </summary>
        [DisplayName("CodeName")]
        [Required, MaxLength(50)]
        public string CodeName { get; set; } = null!;
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("Order")]
        [Required]
        public int Order { get; set; }
        /// <summary>
        /// 扩展参数
        /// </summary>
        [DisplayName("ExtendParams")]
        [MaxLength(500)]
        public string? ExtendParams { get;set; }
        /// <summary>
        /// 颜色
        /// </summary>
        [DisplayName("Color")]
        [MaxLength(50)]
        public string? Color { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        public CodeType CodeType { get; set; } = null!;

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Code> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
                new Code() {CodeTypeId=1,CodeValue="1",CodeName="难过",Order=1,Id=1,IsLocked=false,IsDeleted=false,CreateBy="2",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-10 15:41:07"),},
                new Code() {CodeTypeId=1,CodeValue="2",CodeName="高兴",Order=2,Id=2,IsLocked=false,IsDeleted=false,CreateBy="2",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-10 15:40:54"),},
         };
        }
    }
}
