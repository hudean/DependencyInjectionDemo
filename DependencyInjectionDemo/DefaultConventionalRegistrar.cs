using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionDemo
{
    /// <summary>
    /// 默认约定注册器
    /// </summary>
    public class DefaultConventionalRegistrar : ConventionalRegistrarBase
    {
        public override void AddType(IServiceCollection services, Type type)
        {
            if (IsConventionalRegistrationDisabled(type))
            {
                return;
            }

            var dependencyAttribute = GetDependencyAttributeOrNull(type);
            var lifeTime = GetLifeTimeOrNull(type, dependencyAttribute);

            if (lifeTime == null)
            {
                return;
            }

            //services.AddSingleton(type);
            //Services.AddKeyedSingleton<ICache, BigCache>("big");
            //services.AddScoped(interfaceType, type);

            // 获取该类型需要注入的接口-获取暴露的服务类型和键值服务类型

            #region 以前没有键控注册的方式-参考

            //var exposeServices = ExposeService(type);
            //foreach (var serviceType in exposeServices)
            //{
            //    var serviceDescriptor = new ServiceDescriptor(serviceType, type, lifeTime.Value);
            //    //services.Add(serviceDescriptor);
            //    if (dependencyAttribute?.ReplaceServices == true)
            //    {
            //        services.Replace(serviceDescriptor);
            //    }
            //    else if (dependencyAttribute?.TryRegister == true)
            //    {
            //        services.TryAdd(serviceDescriptor);
            //    }
            //    else
            //    {
            //        services.Add(serviceDescriptor);
            //    }
            //}

            #endregion

            #region 兼容键控注册的方式

            var exposedServiceAndKeyedServiceTypes = GetExposedKeyedServiceTypes(type).Concat(GetExposedServiceTypes(type).Select(t => new ServiceIdentifier(t))).ToList();

            // ABP VNext的用来触发服务暴露对不方便使用依赖注入的服务使用 ObjectAccessor 主要用于跨作用域访问数据。例如，你可能需要在一个请求的生命周期内，多个地方访问和修改某个对象，但该对象并不直接注入到这些地方。ObjectAccessor 可以提供一个集中式的对象访问点，而不需要在每个地方都显式注入对象。

            // TriggerServiceExposing(services, type, exposedServiceAndKeyedServiceTypes);

            foreach (var exposedServiceType in exposedServiceAndKeyedServiceTypes)
            {
                var allExposingServiceTypes = exposedServiceType.ServiceKey == null
              ? exposedServiceAndKeyedServiceTypes.Where(x => x.ServiceKey == null).ToList()
              : exposedServiceAndKeyedServiceTypes.Where(x => x.ServiceKey?.ToString() == exposedServiceType.ServiceKey?.ToString()).ToList();

                var serviceDescriptor = CreateServiceDescriptor(type, exposedServiceType.ServiceKey,
              exposedServiceType.ServiceType,allExposingServiceTypes,lifeTime.Value);

                if (dependencyAttribute?.ReplaceServices == true)
                {
                    services.Replace(serviceDescriptor);
                }
                else if (dependencyAttribute?.TryRegister == true)
                {
                    services.TryAdd(serviceDescriptor);
                }
                else
                {
                    services.Add(serviceDescriptor);
                }
            }
            #endregion

            // CreateServiceDescriptor();
        }

        ///// <summary>
        ///// 创建服务描述符
        ///// </summary>
        ///// <param name="serviceType"></param>
        ///// <param name="serviceKey"></param>
        ///// <param name="implementationType"></param>
        ///// <param name="lifeTime"></param>
        ///// <returns></returns>
        //private ServiceDescriptor CreateServiceDescriptor(Type serviceType, object? serviceKey, Type implementationType, ServiceLifetime lifeTime)
        //{
        //    var exposingServiceType = serviceType;
        //    if (lifeTime == ServiceLifetime.Singleton || lifeTime == ServiceLifetime.Scoped)
        //    {

        //    }



        //    return serviceKey == null
        //   ? ServiceDescriptor.Describe(
        //       exposingServiceType,
        //       implementationType,
        //       lifeTime
        //   )
        //   : ServiceDescriptor.DescribeKeyed(
        //       exposingServiceType,
        //       serviceKey,
        //       implementationType,
        //       lifeTime
        //   );

        //}
    }
}
