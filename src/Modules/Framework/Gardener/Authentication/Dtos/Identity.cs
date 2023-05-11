// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Authentication.Enums;
using Gardener.Base;
using System;
using System.ComponentModel;

namespace Gardener.Authentication.Dtos
{
    /// <summary>
    /// 身份信息
    /// </summary>
    [Description("身份信息")]
    public class Identity : IModelTenant
    {
        /// <summary>
        /// 身份唯一编号
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// 身份唯一名称
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// 身份昵称
        /// </summary>
        public string? NickName { get; set; }
        /// <summary>
        /// 身份类型
        /// </summary>
        public IdentityType IdentityType { get; set; }
        /// <summary>
        /// 客户端类型
        /// </summary>
        public LoginClientType LoginClientType { get; set; }
        /// <summary>
        /// 获取或设置 登录Id(每次登录该Id自动生成)
        /// </summary>
        public string LoginId { get; set; } = null!;
        /// <summary>
        /// 租户编号
        /// </summary>
        public Guid? TenantId { get; set; }
        /// <summary>
        /// 自定义数据
        /// </summary>
        ///<remarks>
        ///可以在登录后放入一些数据（注意不能放入敏感数据，前端能够看到）
        /// </remarks>
        public string? CustomData { get; set; }

        /// <summary>
        /// 是否是租户管理员
        /// </summary>
        /// <remarks>
        /// <para>默认：租户编号为null或空的是租户管理员</para>
        /// <para>租户管理员在查询时不限制租户编号</para>
        /// <para>租户管理员在新增更新时不处理租户编号</para>
        /// <para>
        /// 处理详情在
        /// <code>Gardener.EntityFramwork.DbContexts.GardenerMultiTenantDbContext</code>
        /// </para>
        /// </remarks>
        /// <returns></returns>
        public bool IsTenantAdministrator()
        {
            return TenantId == null || TenantId.Equals(Guid.Empty);
        }
    }
}
