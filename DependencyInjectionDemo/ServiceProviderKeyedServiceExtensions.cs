using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceProviderKeyedServiceExtensions
{
    public static object? GetKeyedService([NotNull] this IServiceProvider provider, Type serviceType, object? serviceKey)
    {
        if (provider is IKeyedServiceProvider keyedServiceProvider)
        {
            return keyedServiceProvider.GetKeyedService(serviceType, serviceKey);
        }

        throw new InvalidOperationException("This service provider doesn't support keyed services.");
    }
}
