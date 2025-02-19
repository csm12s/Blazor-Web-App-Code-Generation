﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attributes;
using Gardener.Authentication.Dtos;
using Gardener.Authentication.Enums;
using Gardener.Base.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gardener.Base
{
    /// <summary>
    /// dto基础类
    /// </summary>
    public abstract class BaseDtoEmptyNoKey
    {
    }

    /// <summary>
    /// dto基础类
    /// </summary>
    public abstract class BaseDtoEmpty<TKey> : BaseDtoEmptyNoKey, IModelId<TKey>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Id), ResourceType = typeof(SharedLocalResource))]
        [Order(1000)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; } = default!;
    }

    /// <summary>
    /// dto基础类
    /// </summary>
    public abstract class BaseDto : BaseDtoEmptyNoKey, IModelCreated, IModelLocked, IModelDeleted, IModelUpdated
    {
        /// <summary>
        /// 是否锁定
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.IsLocked), ResourceType = typeof(SharedLocalResource))]
        [Order(int.MaxValue - 1000)]
        public virtual bool IsLocked { get; set; }
        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.IsDeleted), ResourceType = typeof(SharedLocalResource))]
        [DisabledSearchField]
        public virtual bool IsDeleted { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.CreatedTime), ResourceType = typeof(SharedLocalResource))]
        [Order(int.MaxValue - 900)]
        public virtual DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 创建者编号
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.CreateBy), ResourceType = typeof(SharedLocalResource))]
        [Order(int.MaxValue - 800)]
        public virtual string? CreateBy { get; set; }
        /// <summary>
        /// 创建者身份类型
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.CreateIdentityType), ResourceType = typeof(SharedLocalResource))]
        [Order(int.MaxValue - 700)]
        public virtual IdentityType? CreateIdentityType { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.UpdatedTime), ResourceType = typeof(SharedLocalResource))]
        [Order(int.MaxValue - 600)]
        public virtual DateTimeOffset? UpdatedTime { get; set; }
        /// <summary>
        /// 修改者编号
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.UpdateBy), ResourceType = typeof(SharedLocalResource))]
        [Order(int.MaxValue - 500)]
        public virtual string? UpdateBy { get; set; }
        /// <summary>
        /// 修改者身份类型
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.UpdateIdentityType), ResourceType = typeof(SharedLocalResource))]
        [Order(int.MaxValue - 400)]
        public virtual IdentityType? UpdateIdentityType { get; set; }

        /// <summary>
        /// 设置创建者身份
        /// </summary>
        /// <param name="identity"></param>
        public void SetCreatedIdentity(Identity identity)
        {
            this.CreateBy = identity.Id;
            this.CreateIdentityType = identity.IdentityType;
        }
        /// <summary>
        /// 设置更新者身份
        /// </summary>
        /// <param name="identity"></param>
        public void SetUpdatedIdentity(Identity identity)
        {
            this.UpdateBy = identity.Id;
            this.UpdateIdentityType = identity.IdentityType;
        }
    }

    /// <summary>
    /// dto基础类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseDto<TKey> : BaseDto, IModelId<TKey>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Id), ResourceType = typeof(SharedLocalResource))]
        [Order(1000)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; } = default!;

    }

    /// <summary>
    /// 多租户Dto基类
    /// </summary>
    public abstract class TenantBaseDtoEmptyNoKey : BaseDtoEmptyNoKey, IModelTenant
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.TenantId), ResourceType = typeof(SharedLocalResource))]
        [Order(1001)]
        public virtual Guid? TenantId { get; set; }
        /// <summary>
        /// 租户
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Tenant), ResourceType = typeof(SharedLocalResource))]
        [NotMapped]
        public virtual ITenant? Tenant { get; set; }
    }

    /// <summary>
    /// 多租户Dto基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class TenantBaseDtoEmpty<TKey> : TenantBaseDtoEmptyNoKey, IModelId<TKey>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Id), ResourceType = typeof(SharedLocalResource))]
        [Order(1000)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; } = default!;
    }

    /// <summary>
    /// 多租户Dto基类
    /// </summary>
    public abstract class TenantBaseDtoNoKey : BaseDto, IModelTenant
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.TenantId), ResourceType = typeof(SharedLocalResource))]
        [Order(1001)]
        public virtual Guid? TenantId { get; set; }
        /// <summary>
        /// 租户
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Tenant), ResourceType = typeof(SharedLocalResource))]
        [NotMapped]
        public virtual ITenant? Tenant { get; set; }
    }

    /// <summary>
    /// 多租户Dto基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class TenantBaseDto<TKey> : TenantBaseDtoNoKey, IModelId<TKey>
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = nameof(SharedLocalResource.Id), ResourceType = typeof(SharedLocalResource))]
        [Order(1000)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TKey Id { get; set; } = default!;
    }
}
