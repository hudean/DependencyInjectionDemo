using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionDemo
{
    /// <summary>
    ///  参考ABP VNext的约定注册基类
    ///  参考其他文章：https://blog.csdn.net/u013400314/article/details/144557533
    /// </summary>
    public abstract class ConventionalRegistrarBase  // public class ServiceRegister
    {
        public virtual void AddAssembly(IServiceCollection services, Assembly assembly)
        {
            // 查找程序中的类型
            var types = assembly.GetTypes()
                .Where(
                    type => type != null &&
                            type.IsClass &&
                            !type.IsAbstract &&
                            !type.IsGenericType
                ).ToArray();
            if (types is not null)
                AddTypes(services, types);
        }

        public virtual void AddTypes(IServiceCollection services, params Type[] types)
        {
            // 遍历每一个类检查释放满足约定的规则
            foreach (var type in types)
            {
                AddType(services, type);
            }
        }

        /// <summary>
        /// 添加当前类型的依赖注入关系
        /// </summary>
        /// <param name="services"></param>
        /// <param name="type"></param>
        public abstract void AddType(IServiceCollection services, Type type);

        /// <summary>
        /// 是否禁用约定注册
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual bool IsConventionalRegistrationDisabled(Type type)
        {
            return type.IsDefined(typeof(DisableConventionalRegistrationAttribute), true);
        }

        /// <summary>
        /// 获取依赖注入特性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual DependencyAttribute? GetDependencyAttributeOrNull(Type type)
        {
            return type.GetCustomAttribute<DependencyAttribute>(true);
        }

        /// <summary>
        /// 获取生命周期
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dependencyAttribute"></param>
        /// <returns></returns>
        protected virtual ServiceLifetime? GetLifeTimeOrNull(Type type, DependencyAttribute? dependencyAttribute)
        {
            return dependencyAttribute?.Lifetime ?? GetServiceLifetimeFromClassHierarchy(type) ?? GetDefaultLifeTimeOrNull(type);
        }

        /// <summary>
        /// 从类层次结构中获取服务生命周期
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual ServiceLifetime? GetServiceLifetimeFromClassHierarchy(Type type)
        {
            //IsAssignableFrom(TypeInfo) 返回一个值，该值指示指定类型是否可分配给当前的类型。
            if (typeof(ITransientDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Transient;
            }

            if (typeof(ISingletonDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Singleton;
            }

            if (typeof(IScopedDependency).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Scoped;
            }

            return null;
        }

        /// <summary>
        /// 获取默认生命周期
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual ServiceLifetime? GetDefaultLifeTimeOrNull(Type type)
        {
            return null;
        }

        /// <summary>
        /// 根据约定的规则查找当前类对于的服务类型
        /// 通过接口实现的方式，查找当前类实现的接口，如果一个接口名称去除了 "I" 之后与当前类的后半段一样，
        /// 则当前类应该被注册为这个接口的服务。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [Obsolete("Use the GetExposedServiceTypes and GetExposedKeyedServiceTypes method")]
        public IList<Type> ExposeService(Type type)
        {
            var serviceTypes = new List<Type>();
            //var interfaceTypes = type.GetTypeInfo().GetInterfaces().ToList();
            var interfaces = type.GetInterfaces();
            foreach (var interfacesType in interfaces)
            {
                var interfaceName = interfacesType.Name;
                if (interfaceName.StartsWith('I'))
                {
                    //interfaceName = interfaceName.Substring(1);
                    interfaceName = interfaceName[1..];
                }
                if (type.Name.EndsWith(interfaceName))
                {
                    serviceTypes.Add(interfacesType);
                }
            }
            return serviceTypes;
        }

        /// <summary>
        /// 返回已经暴露的服务类型集合
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual List<Type> GetExposedServiceTypes(Type type)
        {
            return ExposedServiceExplorer.GetExposedServices(type);
        }

        /// <summary>
        /// 返回已经暴露的带键的服务类型集合
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual List<ServiceIdentifier> GetExposedKeyedServiceTypes(Type type)
        {
            return ExposedServiceExplorer.GetExposedKeyedServices(type);
        }

        protected virtual ServiceDescriptor CreateServiceDescriptor(Type implementationType,object? serviceKey,Type exposingServiceType,
            List<ServiceIdentifier> allExposingServiceTypes,ServiceLifetime lifeTime)
        {
            if (lifeTime == ServiceLifetime.Singleton || lifeTime == ServiceLifetime.Scoped)
            {
                var redirectedType = GetRedirectedTypeOrNull(
                    implementationType,
                    exposingServiceType,
                    allExposingServiceTypes
                );

                if (redirectedType != null)
                {
                    return serviceKey == null
                        ? ServiceDescriptor.Describe(
                            exposingServiceType,
                            provider => provider.GetService(redirectedType)!,
                            lifeTime
                        )
                        : ServiceDescriptor.DescribeKeyed(
                            exposingServiceType,
                            serviceKey,
                            (provider, key) => provider.GetKeyedService(redirectedType, key)!,
                            lifeTime
                        );
                }
            }

            return serviceKey == null
                ? ServiceDescriptor.Describe(
                    exposingServiceType,
                    implementationType,
                    lifeTime
                )
                : ServiceDescriptor.DescribeKeyed(
                    exposingServiceType,
                    serviceKey,
                    implementationType,
                    lifeTime
                );
        }

        protected virtual Type? GetRedirectedTypeOrNull(Type implementationType, Type exposingServiceType, List<ServiceIdentifier> allExposingKeyedServiceTypes)
        {
            if (allExposingKeyedServiceTypes.Count < 2)
            {
                return null;
            }

            if (exposingServiceType == implementationType)
            {
                return null;
            }

            if (allExposingKeyedServiceTypes.Any(t => t.ServiceType == implementationType))
            {
                return implementationType;
            }

            return allExposingKeyedServiceTypes.FirstOrDefault(
                t => t.ServiceType != exposingServiceType && exposingServiceType.IsAssignableFrom(t.ServiceType)
            ).ServiceType;
        }
    }
}