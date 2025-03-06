using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Core;

public class OnServiceExposingContext : IOnServiceExposingContext
{
    public Type ImplementationType { get; }

    public List<ServiceIdentifier> ExposedTypes { get; }

    public OnServiceExposingContext([NotNull] Type implementationType, List<Type> exposedTypes)
    {
        ImplementationType = implementationType;
        ExposedTypes = exposedTypes.ConvertAll(t => new ServiceIdentifier(t));
    }

    public OnServiceExposingContext([NotNull] Type implementationType, List<ServiceIdentifier> exposedTypes)
    {
        ImplementationType = implementationType;
        ExposedTypes = exposedTypes;
    }
}
