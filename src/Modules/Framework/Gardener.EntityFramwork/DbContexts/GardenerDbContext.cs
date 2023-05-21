﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Gardener.EntityFramwork.Core;
using Gardener.EntityFramwork.EFAudit;
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
            var context = eventData.Context;
            if (context == null) { return; }

            //基础数据初始化
            GlobalEntityEntryHandle.Handle(context.ChangeTracker.Entries());
            IOrmAuditService ormAuditService = App.GetService<IOrmAuditService>();
            ormAuditService.SavingChangesEvent(context.ChangeTracker.Entries());
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
