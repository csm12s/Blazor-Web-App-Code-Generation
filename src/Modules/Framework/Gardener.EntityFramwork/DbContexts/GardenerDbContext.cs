// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Gardener.Authentication.Dtos;
using Gardener.EntityFramwork.Audit.Core;
using Gardener.EntityFramwork.Audit.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
            var entities = dbContext.ChangeTracker.Entries()
                  .Where(u => u.Entity.GetType() != typeof(AuditEntity) 
                  && u.Entity.GetType() != typeof(AuditOperation) 
                  && u.Entity.GetType() != typeof(AuditProperty) 
                  && (u.State == EntityState.Added || u.State == EntityState.Modified || u.State == EntityState.Deleted)).ToList();
            if (entities == null || entities.Count < 1)
            { 
                return;
            } 

            // User
            var userId = App.User?.FindFirst(nameof(Identity.Id))?.Value;
            var userName = App.User?.FindFirst(nameof(Identity.Name))?.Value;
            foreach (var entity in entities)
            {
                // TODO 1: UpdatedTime 之类字段的修改应该放在这里，不然每次重写Update方法都要修改UpdatedTime；
                // 或者可以写一个基础Service/Repository类，在基础Service/Repository类里处理Entity的时候修改UpdatedTime
                // （现在的Service其实是controller）

                // TODO 2: 这里获取不到 GardenerEntityBase

                // 参考 Admin.Net\backend\Admin.NET.EntityFramework.Core\DbContexts\DefaultDbContext.cs
                // Tenant
                //if (entity.Entity.GetType().IsSubclassOf(typeof(GardenerTenantEntityBase)))
                //{
                //}
                //// Normal entity
                //else if (entity.Entity.GetType().IsSubclassOf(typeof(GardenerEntityBase)))
                //{
                //    if (entity.State == EntityState.Added)
                //    {
                //    }
                //    else if (entity.State == EntityState.Modified)
                //    {
                //    }
                //}
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
