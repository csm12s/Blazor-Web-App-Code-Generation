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

namespace Gardener.Client.Core
{
    public static class AttributeBasedServiceCollectionExtensions
    {
        public static void AddServicesWithAttributeOfType<T>(this IServiceCollection serviceCollection,params Assembly [] assemblys)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            if (assemblys == null)
            {
                throw new ArgumentNullException(nameof(assemblys));
            }

            AddServicesWithAttributeOfType<T>(serviceCollection, assemblys.ToList());
        }
        public static void AddServicesWithAttributeOfType<T>(this IServiceCollection serviceCollection, IEnumerable<Assembly> assembliesToBeScanned)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

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
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsDefined(typeof(T), false))
                .ToList();

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

                        bool isAlreadyRegistered = serviceCollection.Any(s => s.ServiceType == service && s.ImplementationType == implementation);

                        if (!isAlreadyRegistered)
                        {
                            foreach (Type type in implementation.GetInterfaces())
                            {
                                Console.WriteLine(type.FullName + "_" + implementation.FullName);
                                serviceCollection.Add(new ServiceDescriptor(type, implementation, lifetime));
                            }
                           
                        }
                    }
                }
                else
                {
                    if (serviceType.IsClass)
                    {
                        bool isAlreadyRegistered = serviceCollection.Any(s => s.ServiceType == serviceType && s.ImplementationType == serviceType);

                        if (!isAlreadyRegistered)
                        {
                            Console.WriteLine("class:"+serviceType.FullName + "_" + serviceType.FullName);
                            serviceCollection.Add(new ServiceDescriptor(serviceType, serviceType, lifetime));
                        }
                    }
                }
            }
        }
    }
}
