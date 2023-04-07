// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Gardener.Authentication.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Furion.DependencyInjection;

namespace Gardener.Base.Entity
{
    /// <summary>
    /// Entity 接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <remarks>
    /// 实现该接口即认为是数据实体
    /// </remarks>
    public interface IDBEntityBase<TDbContextLocator1> : IPrivateEntity where TDbContextLocator1 : class, IDbContextLocator
    {
    }
    /// <summary>
    /// Entity 接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <remarks>
    /// 实现该接口即认为是数据实体
    /// </remarks>
    public interface IDBEntityBase<TDbContextLocator1, TDbContextLocator2> : IPrivateEntity where TDbContextLocator1 : class, IDbContextLocator where TDbContextLocator2 : class, IDbContextLocator
    {
    }
    /// <summary>
    /// Entity 接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <typeparam name="TDbContextLocator3"></typeparam>
    /// <remarks>
    /// 实现该接口即认为是数据实体
    /// </remarks>
    public interface IDBEntityBase<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3> : IPrivateEntity where TDbContextLocator1 : class, IDbContextLocator where TDbContextLocator2 : class, IDbContextLocator where TDbContextLocator3 : class, IDbContextLocator
    {
    }
    /// <summary>
    /// Entity 接口
    /// </summary>
    /// <typeparam name="TDbContextLocator1"></typeparam>
    /// <typeparam name="TDbContextLocator2"></typeparam>
    /// <typeparam name="TDbContextLocator3"></typeparam>
    /// <typeparam name="TDbContextLocator4"></typeparam>
    /// <remarks>
    /// 实现该接口即认为是数据实体
    /// </remarks>
    public interface IDBEntityBase<TDbContextLocator1, TDbContextLocator2, TDbContextLocator3, TDbContextLocator4> : IPrivateEntity where TDbContextLocator1 : class, IDbContextLocator where TDbContextLocator2 : class, IDbContextLocator where TDbContextLocator3 : class, IDbContextLocator where TDbContextLocator4 : class, IDbContextLocator
    {
    }
}
