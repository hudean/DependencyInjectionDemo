using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Core
{
    /// <summary>
    /// Extensions for working with <see cref="ServiceDescriptor"/>.
    /// </summary>
    public static class ServiceDescriptorExtensions
    {
        /// <summary>
        /// Normalizes the implementation instance data between keyed and not keyed services.
        /// </summary>
        /// <param name="descriptor">
        /// The <see cref="ServiceDescriptor"/> to normalize.
        /// </param>
        /// <returns>
        /// The appropriate implementation instance from the service descriptor.
        /// </returns>
        public static object? NormalizedImplementationInstance(this ServiceDescriptor descriptor) => descriptor.IsKeyedService ? descriptor.KeyedImplementationInstance : descriptor.ImplementationInstance;

        /// <summary>
        /// Normalizes the implementation type data between keyed and not keyed services.
        /// </summary>
        /// <param name="descriptor">
        /// The <see cref="ServiceDescriptor"/> to normalize.
        /// </param>
        /// <returns>
        /// The appropriate implementation type from the service descriptor.
        /// </returns>
        public static Type? NormalizedImplementationType(this ServiceDescriptor descriptor) => descriptor.IsKeyedService ? descriptor.KeyedImplementationType : descriptor.ImplementationType;
    }
}
