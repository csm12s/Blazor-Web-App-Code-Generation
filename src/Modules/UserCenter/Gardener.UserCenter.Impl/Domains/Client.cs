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
using System.Collections.Generic;

namespace Gardener.UserCenter.Impl.Domains
{
    /// <summary>
    /// 客户端信息
    /// </summary>
    public class Client : ClientDto, IEntityBase, IEntitySeedData<Client>, IEntityTypeBuilder<Client>
    {
        /// <summary>
        /// 多对多中间表
        /// </summary>
        public List<ClientFunction> ClientFunctions { get; set; } = new List<ClientFunction>();

        /// <summary>
        /// 配置信息
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<Client> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
        }

        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        /// <returns></returns>
        public IEnumerable<Client> HasData(DbContext dbContext, Type dbContextLocator)
        {
            return new[] {
                new Client{
                Id=Guid.Parse("96c0eec0-861f-4ed2-a183-5604b20bdff9"),
                Name="测试client1",
                Contacts="园丁",
                CreatedTime=DateTimeOffset.FromUnixTimeSeconds(1628689311),
                Email="qq@qq.com",
                SecretKey="9f700cec-b787-4e23-a2da-9e45b3bd6cbb",
                Remark="用于测试",
                Tel="13838888888"
                }
            };
        }
    }
}