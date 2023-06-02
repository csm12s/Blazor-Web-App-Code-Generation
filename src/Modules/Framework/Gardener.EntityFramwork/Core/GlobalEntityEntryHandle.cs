// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.FriendlyException;
using Gardener.Authentication.Dtos;
using Gardener.Base;
using Gardener.Base.Entity.Domains;
using Gardener.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gardener.EntityFramwork.Core
{
    /// <summary>
    /// 全局实体基础信息处理
    /// </summary>
    public static class GlobalEntityEntryHandle
    {
        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="entityEntries"></param>
        /// <param name="handleTenant">是否处理租户信息</param>
        /// <remarks>
        /// <para> 添加时：设置<see cref="IModelCreated"/></para>
        /// <para>修改时：设置<see cref="IModelUpdated"/>中相关字段的值</para>
        /// <paramref name="handleTenant"/>=true 将处理租户相关字段和租户越权拦截
        /// </remarks>
        public static void Handle(IEnumerable<EntityEntry>? entityEntries, bool handleTenant = false)
        {
            if (entityEntries == null || !entityEntries.Any())
            {
                return;
            }
            Identity? identity = IdentityUtil.GetIdentity();
            if (identity == null)
            {
                return;
            }
            #region CRUD Filter
            // 获取所有更改，删除，新增的实体，但排除审计实体（避免死循环）
            entityEntries = entityEntries
                  .Where(u => u.Entity.GetType() != typeof(AuditEntity)
                  && u.Entity.GetType() != typeof(AuditOperation)
                  && u.Entity.GetType() != typeof(AuditProperty)
                  && (u.State == EntityState.Added || u.State == EntityState.Modified || u.State == EntityState.Deleted)).ToList();
            if (entityEntries == null || !entityEntries.Any())
            {
                return;
            }

            foreach (var entity in entityEntries)
            {

                // 没ID track走不进来
                #region 雪花ID
                //var idProperty = Entry(entity.Entity).Property(nameof(GardenerEntityBase.Id));
                //// Long
                //var obj = entity.Entity as GardenerEntityBase<long>;
                //if (obj != null)
                //{
                //    obj.Id = obj.Id == 0 ? IdUtil.GetNextId() : obj.Id;
                //}
                //// String 雪花ID
                //var obj2 = entity.Entity as GardenerEntityBase<string>;
                //if (obj2 != null)
                //{
                //    obj2.Id = string.IsNullOrEmpty(obj2.Id) ? IdUtil.GetNextId().ToString() : obj2.Id;
                //}
                #endregion
                Type type = entity.Entity.GetType();
                // 新增
                if (entity.State == EntityState.Added || entity.GetType().IsAssignableTo(typeof(IModelCreated)))
                {
                    if (type.IsAssignableTo(typeof(IModelCreated)))
                    {
                        //创建者信息
                        entity.Property(nameof(IModelCreated.CreateBy)).CurrentValue = identity.Id;
                        entity.Property(nameof(IModelCreated.CreateIdentityType)).CurrentValue = identity.IdentityType;
                        entity.Property(nameof(IModelCreated.CreatedTime)).CurrentValue = DateTimeOffset.Now;
                    }
                    if (handleTenant && type.IsAssignableTo(typeof(IModelTenantId)) && ((IModelTenantId)identity).IsTenant)
                    {
                        //租户信息
                        entity.Property(nameof(IModelTenantId.TenantId)).CurrentValue = identity.TenantId;

                    }
                }
                // 修改
                else if (entity.State == EntityState.Modified)
                {
                    if (type.IsAssignableTo(typeof(IModelCreated)))
                    {
                        //排除创建者信息
                        entity.Property(nameof(IModelCreated.CreateBy)).IsModified = false;
                        entity.Property(nameof(IModelCreated.CreateIdentityType)).IsModified = false;
                        entity.Property(nameof(IModelCreated.CreatedTime)).IsModified = false;
                    }
                    if (handleTenant && type.IsAssignableTo(typeof(IModelTenantId)) && ((IModelTenantId)identity).IsTenant)
                    {
                        //租户修改其它租户数据时抛出异常
                        CheckIsCurrentTenantData(entity, identity);
                        //排除租户信息
                        entity.Property(nameof(IModelTenantId.TenantId)).IsModified = false;
                    }
                    if (type.IsAssignableTo(typeof(IModelUpdated)))
                    {
                        //更新者信息
                        entity.Property(nameof(IModelUpdated.UpdateBy)).CurrentValue = identity.Id;
                        entity.Property(nameof(IModelUpdated.UpdateIdentityType)).CurrentValue = identity.IdentityType;
                        entity.Property(nameof(IModelUpdated.UpdatedTime)).CurrentValue = DateTimeOffset.Now;

                        entity.Property(nameof(IModelUpdated.UpdateBy)).IsModified = true;
                        entity.Property(nameof(IModelUpdated.UpdateIdentityType)).IsModified = true;
                        entity.Property(nameof(IModelUpdated.UpdatedTime)).IsModified = true;
                    }
                }
                else if (entity.State == EntityState.Deleted)
                {
                    if (handleTenant && type.IsAssignableTo(typeof(IModelTenantId)) && ((IModelTenantId)identity).IsTenant)
                    {
                        //租户删除其它租户数据时抛出异常
                        CheckIsCurrentTenantData(entity, identity);
                    }
                }


                //#region Entity fields filter
                //// 参考 https://furion.baiqian.ltd/docs/dbcontext-audit?_highlight=savingchangesevent#92231-%E6%95%B0%E6%8D%AE%E5%BA%93%E5%AE%A1%E8%AE%A1%E6%97%A5%E5%BF%97
                //// 获取所有实体有效属性，排除 [NotMapper] 属性
                //var props = entity.OriginalValues.Properties;
                //// 获取数据库中实体的值
                //var databaseValues = entity.GetDatabaseValues();
                //// 获取实体当前（现在）的值
                //var currentValues = entity.CurrentValues;
                //// 遍历所有属性
                //foreach (var prop in props)
                //{
                //    // 获取属性名
                //    var propName = prop.Name;
                //    var propType = prop.ClrType;
                //    // 获取现在的实体值
                //    var newValue = currentValues[propName];
                //}
                //#endregion
            }
            #endregion
        }
        /// <summary>
        /// 判断是否是当前租户数据，如果不是将抛出异常
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="identity"></param>
        /// <exception cref="AppFriendlyException"></exception>
        private static void CheckIsCurrentTenantData(EntityEntry entity, Identity identity)
        {
            PropertyValues? propertyValues = entity.GetDatabaseValues();
            if (propertyValues != null)
            {
                Guid? tenantId;
                if (propertyValues.TryGetValue(nameof(IModelTenantId.TenantId), out tenantId))
                {
                    //租户修改其它租户数据时抛出异常
                    if (tenantId == null || !tenantId.Equals(identity.TenantId))
                    {
                        throw Oops.Bah(ExceptionCode.No_Permission_Modify_The_Data);
                    }
                }
            }
        }
    }
}
