using System;

namespace Gardener.Client.Core
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
    public sealed class ScopedServiceAttribute : Attribute
    {
    }
}
