// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Furion.FriendlyException;
using Gardener.Authentication.Dtos;
using Gardener.Base;
using Gardener.Common;
using Gardener.EntityFramwork.Audit.Core;
using Gardener.EntityFramwork.Audit.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Linq;

namespace Gardener.EntityFramwork.DbContexts
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    [AppDbContext("Default")]
    public class GardenerDbContext : AppDbContext<GardenerDbContext>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="options"></param>
        public GardenerDbContext(DbContextOptions<GardenerDbContext> options) : base(options)
        {
        }
        /// <summary>
        /// 解决sqlite 不支持datetimeoffset问题（损失很小的精度）
        /// https://blog.dangl.me/archive/handling-datetimeoffset-in-sqlite-with-entity-framework-core/
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                // SQLite does not have proper support for DateTimeOffset via Entity Framework Core, see the limitations
                // here: https://docs.microsoft.com/en-us/ef/core/providers/sqlite/limitations#query-limitations
                // To work around this, when the Sqlite database provider is used, all model properties of type DateTimeOffset
                // use the DateTimeOffsetToBinaryConverter
                // Based on: https://github.com/aspnet/EntityFrameworkCore/issues/10784#issuecomment-415769754
                // This only supports millisecond precision, but should be sufficient for most use cases.
                foreach (var entityType in builder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTimeOffset)
                                                                                || p.PropertyType == typeof(DateTimeOffset?));
                    foreach (var property in properties)
                    {
                        builder
                            .Entity(entityType.Name)
                            .Property(property.Name)
                            .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        protected override void SavingChangesEvent(DbContextEventData eventData, InterceptionResult<int> result)
        {
            IOrmAuditService ormAuditService = App.GetService<IOrmAuditService>();
            ormAuditService.SavingChangesEvent(eventData.Context.ChangeTracker.Entries());

            #region CRUD Filter
            var dbContext = eventData.Context;
            // 获取所有更改，删除，新增的实体，但排除审计实体（避免死循环）
            var entityEntries = dbContext.ChangeTracker.Entries()
                  .Where(u => u.Entity.GetType() != typeof(AuditEntity) 
                  && u.Entity.GetType() != typeof(AuditOperation) 
                  && u.Entity.GetType() != typeof(AuditProperty) 
                  && (u.State == EntityState.Added || u.State == EntityState.Modified || u.State == EntityState.Deleted)).ToList();
            if (entityEntries == null || entityEntries.Count < 1)
            { 
                return;
            } 

            foreach (var entity in entityEntries)
            {
                #region Entity filter
                // Entity filter, 这里不判断GardenerEntityBase，有的表可能不继承这个
                if (true)//entry.Entity.GetType().IsSubclassOf(typeof(GardenerEntityBase))
                {
                    // 参考 Admin.Net\backend\Admin.NET.EntityFramework.Core\DbContexts\DefaultDbContext.cs
                    // Tenant id
                    if (entity.Entity.GetType().IsSubclassOf(typeof(GardenerTenantEntityBase)))
                    {
                    }

                    #region 雪花ID
                    var idProperty = Entry(entity.Entity).Property(nameof(GardenerEntityBase.Id));
                    // Long
                    var obj = entity.Entity as GardenerEntityBase<long>;
                    if (obj != null)
                    {
                        obj.Id = obj.Id == 0 ? IdUtil.GetNextId() : obj.Id;
                    }
                    // String 雪花ID
                    var obj2 = entity.Entity as GardenerEntityBase<string>;
                    if (obj2 != null)
                    {
                        obj2.Id = string.IsNullOrEmpty(obj2.Id) ? IdUtil.GetNextId().ToString() : obj2.Id;
                    }
                    #endregion

                    // 新增
                    if (entity.State == EntityState.Added)
                    {
                        Entry(entity.Entity).Property(nameof(GardenerEntityBase.CreateBy))
                            .CurrentValue = IdentityUtil.GetIdentityId();
                        Entry(entity.Entity).Property(nameof(GardenerEntityBase.CreatedTime))
                            .CurrentValue = DateTimeOffset.Now;
                        Entry(entity.Entity).Property(nameof(GardenerEntityBase.CreateIdentityType))
                            .CurrentValue = IdentityUtil.GetIdentityType();
                    }
                    // 修改
                    else if (entity.State == EntityState.Modified)
                    {
                        // 排除创建人
                        entity.Property(nameof(GardenerEntityBase.CreateBy)).IsModified = false;
                        entity.Property(nameof(GardenerEntityBase.CreatedTime)).IsModified = false;
                        entity.Property(nameof(GardenerEntityBase.CreateIdentityType)).IsModified = false;

                        Entry(entity.Entity).Property(nameof(GardenerEntityBase.UpdateBy))
                            .CurrentValue = IdentityUtil.GetIdentityId();
                        Entry(entity.Entity).Property(nameof(GardenerEntityBase.UpdatedTime))
                            .CurrentValue = DateTimeOffset.Now;
                        Entry(entity.Entity).Property(nameof(GardenerEntityBase.UpdateIdentityType))
                            .CurrentValue = IdentityUtil.GetIdentityType();
                    }
                }
                #endregion

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
        /// 数据保存后
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        protected override void SavedChangesEvent(SaveChangesCompletedEventData eventData, int result)
        {
            IOrmAuditService ormAuditService = App.GetService<IOrmAuditService>();
            ormAuditService.SavedChangesEvent();
        }

    }
}
