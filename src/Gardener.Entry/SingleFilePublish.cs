using Furion;
using System.Reflection;

namespace Gardener.Entry;

/// <summary>
/// 单文件发布
/// 存在的问题和解决方案见Furion文档
/// https://furion.baiqian.ltd/docs/singlefile?_highlight=%E5%8D%95&_highlight=%E6%96%87%E4%BB%B6&_highlight=%E5%8F%91%E5%B8%83
/// </summary>
public class SingleFilePublish : ISingleFilePublish
{
    /// <summary>
    /// 解决单文件不能扫描的程序集
    /// </summary>
    /// <remarks>和 <see cref="IncludeAssemblyNames"/> 可同时配置</remarks>
    /// <returns></returns>
    public Assembly[] IncludeAssemblies()
    {
        // 需要 Furion 框架扫描哪些程序集就写上去即可
        return Array.Empty<Assembly>();
    }

    /// <summary>
    /// 解决单文件不能扫描的程序集名称
    /// </summary>
    /// <remarks>和 <see cref="IncludeAssemblies"/> 可同时配置</remarks>
    /// <returns></returns>
    public string[] IncludeAssemblyNames()
    {
        // 需要 Furion 框架扫描哪些程序集就写上去即可
        return new[]
        {
         #region Gardener IncludeAssembly  
        "Gardener",
        "Gardener.Api.Core",
        //"Gardener.Api.Entry",
        "Gardener.Attachment",
        "Gardener.Attachment.Client",
        "Gardener.Attributes",
        "Gardener.Audit",
        "Gardener.Audit.Client",
        "Gardener.Authentication",
        "Gardener.Authentication.Client",
        "Gardener.Authorization",
        "Gardener.Base",
        "Gardener.Base.Entity",
        "Gardener.Cache",
        "Gardener.Client.Base",
        "Gardener.Client.Core",
        "Gardener.Client.Entry",
        //"Gardener.Client.WPF",
        "Gardener.CodeGeneration",
        "Gardener.CodeGeneration.Client",
        "Gardener.Common",
        "Gardener.Email",
        "Gardener.Email.Client",
        "Gardener.EntityFramwork",
        //"Gardener.Entry",
        "Gardener.Enums",
        "Gardener.EventBus",
        "Gardener.FileStore",
        "Gardener.NotificationSystem",
        "Gardener.NotificationSystem.Client",
        "Gardener.SqlSugar",
        "Gardener.Swagger",
        "Gardener.Swagger.Client",
        "Gardener.SystemManager",
        "Gardener.SystemManager.Client",
        "Gardener.SysTimer",
        "Gardener.SysTimer.Client",
        "Gardener.SysTimer.Impl",
        "Gardener.UserCenter",
        "Gardener.UserCenter.Client",
        "Gardener.UserCenter.Impl",
        "Gardener.VerifyCode",
        "Gardener.VerifyCode.CacheStore",
        "Gardener.VerifyCode.Client",
        "Gardener.VerifyCode.DbStore",
        #endregion

         #region Custom IncludeAssembly
         
        #endregion
        };
    }
}

