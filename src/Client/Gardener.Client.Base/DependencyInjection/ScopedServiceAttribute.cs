using System;

namespace Gardener.Client.Base
{
    /// <summary>
    /// 自动注入为Scoped服务
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
    public sealed class ScopedServiceAttribute : Attribute
    {
    }
}
