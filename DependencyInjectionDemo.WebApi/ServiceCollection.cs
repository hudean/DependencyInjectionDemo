//using System.Reflection;

//namespace DependencyInjectionDemo.WebApi
//{
//    [Obsolete("不用的")]
//    public static class ServiceCollectionExtensions
//    {
//        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
//        {
//            services.AddAttributeInjectService();
//            services.AddInterfaceInjectService();
//            return services;
//        }
//        private static IServiceCollection AddAttributeInjectService(this IServiceCollection services)
//        {
//            var types = Assembly.GetExecutingAssembly().GetTypes();
//            foreach (var type in types)
//            {
//                var attribute = type.GetCustomAttribute<DependencyAttribute>();
//                if (attribute != null)
//                {
//                    var serviceType = type.GetInterfaces().FirstOrDefault();
//                    if (serviceType == null)
//                    {
//                        services.Add(new ServiceDescriptor(type, type, attribute.Lifetime));
//                    }
//                    else
//                    {
//                        services.Add(new ServiceDescriptor(serviceType, type, attribute.Lifetime));
//                    }
//                }
//            }
//            return services;
//        }
//        private static IServiceCollection AddInterfaceInjectService(this IServiceCollection services)
//        {
//            var types = Assembly.GetExecutingAssembly().GetTypes();
//            foreach (var type in types)
//            {
//                if (type.GetInterface(nameof(IDependency)) != null)
//                {
//                    var attribute = type.GetCustomAttribute<DependencyAttribute>();
//                    if (attribute != null)
//                    {
//                        services.Add(new ServiceDescriptor(type, type, attribute.Lifetime));
//                    }
//                }
//            }
//            return services;
//        }


//        private static void Inject(this IServiceCollection services)
//        {
//            var assembly = Assembly.GetAssembly(typeof(Program));
//            var types = assembly!.GetTypes().Where(type =>
//                type != null &&
//                type.IsClass &&
//                !type.IsAbstract &&
//                !type.IsGenericType).ToArray();
//            if (types is not null)
//            {
//                foreach (var type in types)
//                {
//                    AddType(services, type);
//                }
//            }
//        }

//        private static  void AddType(IServiceCollection services, Type type)
//        {
//            if (IsConventionalRegistrationDisabled(type))
//            {
//                return;
//            }

//            var dependencyAttribute = GetDependencyAttributeOrNull(type);
//            var lifeTime = GetLifeTimeOrNull(type, dependencyAttribute);

//            if (lifeTime == null)
//            {
//                return;
//            }


//        }

//        /// <summary>
//        /// 是否禁用了约定注册
//        /// </summary>
//        /// <param name="type"></param>
//        /// <returns></returns>
//        private static bool IsConventionalRegistrationDisabled(Type type)
//        {
//            return type.IsDefined(typeof(DisableConventionalRegistrationAttribute), true);
//        }

//        /// <summary>
//        /// 获取该类型生命周期特性
//        /// </summary>
//        /// <param name="type"></param>
//        /// <returns></returns>
//        private static DependencyAttribute? GetDependencyAttributeOrNull(Type type)
//        {
//            return type.GetCustomAttribute<DependencyAttribute>(true);
//        }

//        /// <summary>
//        /// 获取该类型的服务注册生命周期
//        /// </summary>
//        /// <param name="type"></param>
//        /// <param name="dependencyAttribute"></param>
//        /// <returns></returns>
//        private static ServiceLifetime? GetLifeTimeOrNull(Type type, DependencyAttribute? dependencyAttribute)
//        {
//            return dependencyAttribute?.Lifetime ?? GetServiceLifetimeFromClassHierarchy(type) ?? GetDefaultLifeTimeOrNull(type);
//        }

//        private static ServiceLifetime? GetServiceLifetimeFromClassHierarchy(Type type)
//        {
//            if (typeof(ITransientDependency).GetTypeInfo().IsAssignableFrom(type))
//            {
//                return ServiceLifetime.Transient;
//            }

//            if (typeof(ISingletonDependency).GetTypeInfo().IsAssignableFrom(type))
//            {
//                return ServiceLifetime.Singleton;
//            }

//            if (typeof(IScopedDependency).GetTypeInfo().IsAssignableFrom(type))
//            {
//                return ServiceLifetime.Scoped;
//            }

//            return null;
//        }

//        private static ServiceLifetime? GetDefaultLifeTimeOrNull(Type type)
//        {
//            return null;
//        }
//    }
//}
