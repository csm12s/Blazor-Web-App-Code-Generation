// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using Gardener.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.SystemManager.Domains
{
    /// <summary>
    /// 字典类型
    /// </summary>
    [Description("CodeType")]
    public class CodeType : GardenerEntityBase, IEntitySeedData<CodeType>
    {
        /// <summary>
        /// 字典类型名称
        /// </summary>
        [DisplayName("CodeTypeName")]
        [Required, MaxLength(50)]
        public string CodeTypeName { get; set; } = null!;
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("Remark")]
        [MaxLength(200)]
        public string? Remark { get; set; }
        /// <summary>
        /// 字典集合
        /// </summary>
        public ICollection<Code> Codes { get; set; }= new List<Code>();

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<CodeType> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
                new CodeType() {CodeTypeName="心情",Remark="心情",Id=1,IsLocked=false,IsDeleted=false,CreateBy="2",CreateIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2023-04-10 15:39:45"),},
         };
        }
    }
}
