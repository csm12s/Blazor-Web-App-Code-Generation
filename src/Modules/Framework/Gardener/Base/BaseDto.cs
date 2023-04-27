// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Attributes;
using Gardener.Authentication.Dtos;
using Gardener.Authentication.Enums;
using System;
using System.ComponentModel;

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
        [DisplayName("Id")]
        [Order(1000)]
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
        [DisplayName("IsLocked")]
        [Order(int.MaxValue-1000)]
        public virtual bool IsLocked { get; set; }
        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        [DisplayName("IsDeleted")]
        [DisabledSearchField]
        public virtual bool IsDeleted { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [DisplayName("CreatedTime")]
        [Order(int.MaxValue - 900)]
        public virtual DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 创建者编号
        /// </summary>
        [DisplayName("CreateBy")]
        [Order(int.MaxValue - 800)]
        public virtual string? CreateBy { get; set; }
        /// <summary>
        /// 创建者身份类型
        /// </summary>
        [DisplayName("CreateIdentityType")]
        [Order(int.MaxValue - 700)]
        public virtual IdentityType? CreateIdentityType { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        [DisplayName("UpdatedTime")]
        [Order(int.MaxValue - 600)]
        public virtual DateTimeOffset? UpdatedTime { get; set; }
        /// <summary>
        /// 修改者编号
        /// </summary>
        [DisplayName("UpdateBy")]
        [Order(int.MaxValue - 500)]
        public virtual string? UpdateBy { get; set; }
        /// <summary>
        /// 修改者身份类型
        /// </summary>
        [DisplayName("UpdateIdentityType")]
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
        [DisplayName("Id")]
        [Order(1000)]
        public TKey Id { get; set; } = default!;

    }


    /// <summary>
    /// 多租户Dto基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class TenantBaseDtoEmpty<TKey> : BaseDtoEmpty<TKey>, IModelTenantId
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [DisplayName("TenantId")]
        [Order(1001)]
        public virtual Guid? TenantId { get; set; }
    }
    /// <summary>
    /// 多租户Dto基类
    /// </summary>
    public abstract class TenantBaseDtoEmptyNoKey : BaseDtoEmptyNoKey, IModelTenantId
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [DisplayName("TenantId")]
        [Order(1001)]
        public virtual Guid? TenantId { get; set; }
    }

    /// <summary>
    /// 多租户Dto基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class TenantBaseDto<TKey> : BaseDto<TKey>, IModelTenantId
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [DisplayName("TenantId")]
        [Order(1001)]
        public virtual Guid? TenantId { get; set; }
    }
    /// <summary>
    /// 多租户Dto基类
    /// </summary>
    public abstract class TenantBaseDtoNoKey : BaseDto, IModelTenantId
    {
        /// <summary>
        /// 租户编号
        /// </summary>
        [DisplayName("TenantId")]
        [Order(1001)]
        public virtual Guid? TenantId { get; set; }
    }
}
