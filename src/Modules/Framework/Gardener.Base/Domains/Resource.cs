// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using Gardener.Base.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Base.Domains
{
    /// <summary>
    /// 资源表
    /// </summary>
    [Description("资源信息")]
    public class Resource : GardenerEntityBase<Guid>, IEntitySeedData<Resource>
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        [Required, MaxLength(100)]
        [DisplayName("名称")]
        public string Name { get; set; }

        /// <summary>
        /// 资源名称简写-唯一
        /// 内部鉴权使用
        /// </summary>
        [Required, MaxLength(100)]
        [DisplayName("唯一标示")]
        public string Key { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        [DisplayName("备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 资源地址 菜单：页面路由地址
        /// </summary>
        [MaxLength(200)]
        [DisplayName("路径")]
        public string Path { get; set; }

        /// <summary>
        /// 资源图标
        /// </summary>
        [MaxLength(50)]
        [DisplayName("图标")]
        public string Icon { get; set; }

        /// <summary>
        /// 资源排序
        /// </summary>
        [Required, DefaultValue(0)]
        [DisplayName("排序")]
        public int Order { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        [DisplayName("父级编号")]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 父级
        /// </summary>
        public Resource Parent { get; set; }

        /// <summary>
        /// 子集
        /// </summary>
        public ICollection<Resource> Children { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        [Required, DefaultValue(ResourceType.Menu)]
        public ResourceType Type { get; set; }

        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<ResourceFunction> ResourceFunctions { get; set; }

        public IEnumerable<Resource> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[]{
                new Resource() {Name="DB First",Key="system_tool_code_gen",Path="/system_tool/code_gen",Icon="code-sandbox",Order=41,ParentId=Guid.Parse("c2090656-8a05-4e67-b7ea-62f178639620"),Type=Enum.Parse<ResourceType>("Menu"),IsLocked=false,IsDeleted=false,CreatorId="1",CreatorIdentityType=Enum.Parse<IdentityType>("User"),CreatedTime=DateTimeOffset.Parse("2022-11-01 14:32:13"),UpdatedTime=DateTimeOffset.Parse("2022-11-01 14:33:10"),Id=Guid.Parse("3b5a2330-081b-4c9b-95a3-0e36ba9dda65"),},
            };
        }
    }
}