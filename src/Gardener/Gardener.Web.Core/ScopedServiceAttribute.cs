using System;

namespace Gardener.Web.Core
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
    public sealed class ScopedServiceAttribute : Attribute
    {
    }
}
