// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Web.Core
{
    public static class Test
    {
        public static void AddServicesWithAttributeOfType<T>(IEnumerable<Assembly> assembliesToBeScanned)
        {

            if (assembliesToBeScanned == null)
            {
                throw new ArgumentNullException(nameof(assembliesToBeScanned));
            }

            if (!assembliesToBeScanned.Any())
            {
                throw new ArgumentException($"The {assembliesToBeScanned} is empty.", nameof(assembliesToBeScanned));
            }

            ServiceLifetime lifetime = ServiceLifetime.Scoped;

            switch (typeof(T).Name)
            {
                case nameof(ScopedServiceAttribute):
                    lifetime = ServiceLifetime.Scoped;
                    break;
                default:
                    throw new ArgumentException($"The type {typeof(T).Name} is not a valid type in this context.");
            }

            List<Type> servicesToBeRegistered = assembliesToBeScanned
                .SelectMany(assembly => assembly.GetTypes()).Where(type => type.IsDefined(typeof(T), false)).ToList();

            foreach (Type serviceType in servicesToBeRegistered)
            {
                List<Type> implementations = new List<Type>();

                if (serviceType.IsGenericType && serviceType.IsGenericTypeDefinition)
                {
                    implementations = assembliesToBeScanned.SelectMany(a => a.GetTypes())
                    .Where(type => type.IsGenericType && type.IsClass && type.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == serviceType.GetGenericTypeDefinition()))
                    .ToList();
                }
                else
                {
                    implementations = assembliesToBeScanned.SelectMany(a => a.GetTypes())
                    .Where(type => serviceType.IsAssignableFrom(type) && type.IsClass).ToList();
                }

                if (implementations.Any())
                {
                    foreach (Type implementation in implementations)
                    {
                        bool isGenericTypeDefinition = implementation.IsGenericType && implementation.IsGenericTypeDefinition;
                        Type service = isGenericTypeDefinition
                            && serviceType.IsGenericType
                            && serviceType.IsGenericTypeDefinition == false
                            && serviceType.ContainsGenericParameters
                                  ? serviceType.GetGenericTypeDefinition()
                                  : serviceType;

                        new ServiceDescriptor(service, implementation, lifetime);
                    }
                }
                else
                {
                    if (serviceType.IsClass)
                    {

                        new ServiceDescriptor(serviceType, serviceType, lifetime);
                    }
                }
            }
        }
    }
}
