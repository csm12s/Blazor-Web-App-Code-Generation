//// -----------------------------------------------------------------------------
//// 园丁,是个很简单的管理系统
////  gitee:https://gitee.com/hgflydream/Gardener 
////  issues:https://gitee.com/hgflydream/Gardener/issues 
//// -----------------------------------------------------------------------------

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace Gardener.Web.Core
//{
    
//    public abstract class TestBase
//    { 
        
//    }
//    public interface ITest
//    {

//    }
//    [ScopedService]
//    public class Test1
//    {

//    }
//    [ScopedService]
//    public class Test2: TestBase
//    { 
    
//    }
//    [ScopedService]
//    public class Test3 : ITest
//    {

//    }
    
//    [ScopedService]
//    public class Test4 : TestBase, ITest
//    {

//    }
//    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false)]
//    public sealed class ScopedServiceAttribute : Attribute
//    {
//    }
//    public static class Cs
//    { 
//        public static void Ce(IEnumerable<Assembly> assembliesToBeScanned)
//        {

//            List<Type> servicesToBeRegistered = assembliesToBeScanned
//                .SelectMany(assembly => assembly.GetTypes())
//                .Where(type => type.IsDefined(typeof(ScopedServiceAttribute), false))
//                .ToList();

//            foreach (Type serviceType in servicesToBeRegistered)
//            {
//                List<Type> implementations = new List<Type>();

//                if (serviceType.IsGenericType && serviceType.IsGenericTypeDefinition)
//                {
//                    implementations = assembliesToBeScanned.SelectMany(a => a.GetTypes())
//                    .Where(type => type.IsGenericType && type.IsClass && type.GetInterfaces()
//                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == serviceType.GetGenericTypeDefinition()))
//                    .ToList();
//                }
//                else
//                {
//                    implementations = assembliesToBeScanned.SelectMany(a => a.GetTypes())
//                    .Where(type => serviceType.IsAssignableFrom(type) && type.IsClass).ToList();
//                }

//                if (implementations.Any())
//                {
//                    foreach (Type implementation in implementations)
//                    {
//                        Type[] ts = implementation.GetInterfaces();
//                        if (ts?.Length > 0)
//                        {
//                            foreach (Type type in implementation.GetInterfaces())
//                            {
//                                if (type.IsInterface)
//                                {
//                                    Console.WriteLine(type.FullName+"_"+ implementation.FullName);
//                                }
//                            }
//                        }

//                        Type baseType = implementation.BaseType;
//                        if (!baseType.Equals(typeof(Object)))
//                        {
//                            Console.WriteLine(baseType.FullName + "_____" + implementation.FullName);
//                        }
//                        else
//                        {
//                            Console.WriteLine(implementation.FullName + "_____" + implementation.FullName);
//                        }

//                    }
//                }
//                else
//                {
//                    if (serviceType.IsClass)
//                    {
//                        Console.WriteLine(serviceType.FullName + "_____" + serviceType.FullName);
//                    }
//                }
//            }


//        }
    
//    }
//}
