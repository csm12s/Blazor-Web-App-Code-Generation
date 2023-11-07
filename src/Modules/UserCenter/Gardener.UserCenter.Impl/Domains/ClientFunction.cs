// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Base.Entity;
using Gardener.UserCenter.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel;

namespace Gardener.UserCenter.Impl.Domains
{
    /// <summary>
    /// 客户端功能信息
    /// </summary>
    public class ClientFunction : ClientFunctionDto, IEntityBase, IEntityTypeBuilder<ClientFunction>
    {
        /// <summary>
        /// 客户端
        /// </summary>
        [DisplayName("客户端")]
        public Client Client { get; set; } = null!;

        /// <summary>
        /// 功能
        /// </summary>
        [DisplayName("功能")]
        public Function Function { get; set; } = null!;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<ClientFunction> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder
                .HasKey(t => new { t.ClientId, t.FunctionId });

            entityBuilder
                .HasOne(pt => pt.Client)
                .WithMany(t => t.ClientFunctions)
                .HasForeignKey(pt => pt.ClientId);
        }
    }
}
