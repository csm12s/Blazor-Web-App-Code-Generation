// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Client.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gardener.Client.MAUI.Extensions
{
    /// <summary>
    /// 通过扫描特性注册服务
    /// </summary>
    public static class AttributeBasedServiceCollectionExtensions
    {
        /// <summary>
        /// 通过扫描特性注册服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceCollection"></param>
        /// <param name="assemblys"></param>
        public static void AddServicesWithAttributeOfTypeFromModuleContextMAUI(this IServiceCollection serviceCollection, Assembly[] assemblys)
        {
            List<Assembly> all = new List<Assembly>();
            all.AddRange(ModuleExtension.GetModuleContext().ModeuleAssemblies);
            all.AddRange(assemblys);
            serviceCollection.AddServicesWithAttributeOfType(all.ToArray());
        }
        /// <summary>
        /// 通过扫描特性注册服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceCollection"></param>
        /// <param name="assemblys"></param>
        public static void AddServicesWithAttributeOfType(this IServiceCollection serviceCollection, params Assembly[] assemblys)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            if (assemblys == null)
            {
                throw new ArgumentNullException(nameof(assemblys));
            }

            AddServicesWithAttributeOfType<ScopedServiceAttribute>(serviceCollection, assemblys.ToList());
            AddServicesWithAttributeOfType<TransientServiceAttribute>(serviceCollection, assemblys.ToList());
            AddServicesWithAttributeOfType<SingletonServiceAttribute>(serviceCollection, assemblys.ToList());
        }
        /// <summary>
        /// 通过扫描特性注册服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceCollection"></param>
        /// <param name="assemblys"></param>
        public static void AddServicesWithAttributeOfType<T>(this IServiceCollection serviceCollection, params Assembly[] assemblys)
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
        /// <summary>
        /// 先判断是否注册过，没有注册时才注册
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceCollection"></param>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        /// <param name="lifetime"></param>
        public static void TryAdd(this IServiceCollection serviceCollection, Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            bool isAlreadyRegistered = serviceCollection.Any(s => s.ServiceType == serviceType && s.ImplementationType == implementationType);
            if (!isAlreadyRegistered)
            {
                serviceCollection.Add(new ServiceDescriptor(serviceType, implementationType, lifetime));
            }
        }
        /// <summary>
        /// 通过扫描特性注册服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceCollection"></param>
        /// <param name="assembliesToBeScanned"></param>
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
                case nameof(TransientServiceAttribute):
                    lifetime = ServiceLifetime.Transient;
                    break;
                case nameof(ScopedServiceAttribute):
                    lifetime = ServiceLifetime.Scoped;
                    break;
                case nameof(SingletonServiceAttribute):
                    lifetime = ServiceLifetime.Singleton;
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
                        Type[] ts = implementation.GetInterfaces();
                        if (ts?.Length > 0)
                        {
                            foreach (Type type in implementation.GetInterfaces())
                            {
                                Console.WriteLine("注入服务：" + type.Name + "," + implementation.Name);
                                serviceCollection.TryAdd(type, implementation, lifetime);
                            }
                        }

                        Type baseType = implementation.BaseType;
                        if (!baseType.Equals(typeof(Object)))
                        {
                            Console.WriteLine("注入服务：" + baseType.Name + "," + implementation.Name);
                            serviceCollection.TryAdd(baseType, implementation, lifetime);
                        }
                        else
                        {
                            Console.WriteLine("注入服务：" + implementation.Name + "," + implementation.Name);
                            serviceCollection.TryAdd(implementation, implementation, lifetime);
                        }
                    }
                }
                else
                {
                    if (serviceType.IsClass)
                    {
                        serviceCollection.TryAdd(serviceType, serviceType, lifetime);
                    }
                }
            }
        }
    }
}
