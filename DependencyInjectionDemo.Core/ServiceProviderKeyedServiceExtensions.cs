using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Core
{
    public static class ServiceProviderKeyedServiceExtensions
    {
        public static object? GetKeyedService(this IServiceProvider provider, Type serviceType, object? serviceKey)
        {
            if (provider is IKeyedServiceProvider keyedServiceProvider)
            {
                return keyedServiceProvider.GetKeyedService(serviceType, serviceKey);
            }

            throw new InvalidOperationException("This service provider doesn't support keyed services.");
        }
    }
}
