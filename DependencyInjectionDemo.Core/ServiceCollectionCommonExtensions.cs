﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Core
{
    public static class ServiceCollectionCommonExtensions
    {
        public static bool IsAdded<T>(this IServiceCollection services)
        {
            return services.IsAdded(typeof(T));
        }

        public static bool IsAdded(this IServiceCollection services, Type type)
        {
            return services.Any(d => d.ServiceType == type);
        }

       
        public static T? GetSingletonInstanceOrNull<T>(this IServiceCollection services)
        {
            return (T?)services
                .FirstOrDefault(d => d.ServiceType == typeof(T))
                ?.NormalizedImplementationInstance();
        }

        public static T GetSingletonInstance<T>(this IServiceCollection services)
        {
            var service = services.GetSingletonInstanceOrNull<T>();
            if (service == null)
            {
                throw new InvalidOperationException("Could not find singleton service: " + typeof(T).AssemblyQualifiedName);
            }

            return service;
        }

        //public static IServiceProvider BuildServiceProviderFromFactory([NotNull] this IServiceCollection services)
        //{

        //    foreach (var service in services)
        //    {
        //        var factoryInterface = service.NormalizedImplementationInstance()?.GetType()
        //            .GetTypeInfo()
        //            .GetInterfaces()
        //            .FirstOrDefault(i => i.GetTypeInfo().IsGenericType &&
        //                                 i.GetGenericTypeDefinition() == typeof(IServiceProviderFactory<>));

        //        if (factoryInterface == null)
        //        {
        //            continue;
        //        }

        //        var containerBuilderType = factoryInterface.GenericTypeArguments[0];
        //        return (IServiceProvider)typeof(ServiceCollectionCommonExtensions)
        //            .GetTypeInfo()
        //            .GetMethods()
        //            .Single(m => m.Name == nameof(BuildServiceProviderFromFactory) && m.IsGenericMethod)
        //            .MakeGenericMethod(containerBuilderType)
        //            .Invoke(null, new object?[] { services, null })!;
        //    }

        //    return services.BuildServiceProvider();
        //}

        //public static IServiceProvider BuildServiceProviderFromFactory<TContainerBuilder>([NotNull] this IServiceCollection services, Action<TContainerBuilder>? builderAction = null) where TContainerBuilder : notnull
        //{

        //    var serviceProviderFactory = services.GetSingletonInstanceOrNull<IServiceProviderFactory<TContainerBuilder>>();
        //    if (serviceProviderFactory == null)
        //    {
        //        throw new Exception($"Could not find {typeof(IServiceProviderFactory<TContainerBuilder>).FullName} in {services}.");
        //    }

        //    var builder = serviceProviderFactory.CreateBuilder(services);
        //    builderAction?.Invoke(builder);
        //    return serviceProviderFactory.CreateServiceProvider(builder);
        //}

        ///// <summary>
        ///// Resolves a dependency using given <see cref="IServiceCollection"/>.
        ///// This method should be used only after dependency injection registration phase completed.
        ///// </summary>
        //internal static T? GetService<T>(this IServiceCollection services)
        //{
        //    return services
        //        .GetSingletonInstance<IAbpApplication>()
        //        .ServiceProvider
        //        .GetService<T>();
        //}

        ///// <summary>
        ///// Resolves a dependency using given <see cref="IServiceCollection"/>.
        ///// This method should be used only after dependency injection registration phase completed.
        ///// </summary>
        //internal static object? GetService(this IServiceCollection services, Type type)
        //{
        //    return services
        //        .GetSingletonInstance<IAbpApplication>()
        //        .ServiceProvider
        //        .GetService(type);
        //}

        ///// <summary>
        ///// Resolves a dependency using given <see cref="IServiceCollection"/>.
        ///// Throws exception if service is not registered.
        ///// This method should be used only after dependency injection registration phase completed.
        ///// </summary>
        //public static T GetRequiredService<T>(this IServiceCollection services) where T : notnull
        //{
        //    return services
        //        .GetSingletonInstance<IAbpApplication>()
        //        .ServiceProvider
        //        .GetRequiredService<T>();
        //}

        ///// <summary>
        ///// Resolves a dependency using given <see cref="IServiceCollection"/>.
        ///// Throws exception if service is not registered.
        ///// This method should be used only after dependency injection registration phase completed.
        ///// </summary>
        //public static object GetRequiredService(this IServiceCollection services, Type type)
        //{
        //    return services
        //        .GetSingletonInstance<IAbpApplication>()
        //        .ServiceProvider
        //        .GetRequiredService(type);
        //}

        ///// <summary>
        ///// Returns a <see cref="Lazy{T}"/> to resolve a service from given <see cref="IServiceCollection"/>
        ///// once dependency injection registration phase completed.
        ///// </summary>
        //public static Lazy<T?> GetServiceLazy<T>(this IServiceCollection services)
        //{
        //    return new Lazy<T?>(services.GetService<T>, true);
        //}

        ///// <summary>
        ///// Returns a <see cref="Lazy{T}"/> to resolve a service from given <see cref="IServiceCollection"/>
        ///// once dependency injection registration phase completed.
        ///// </summary>
        //public static Lazy<object?> GetServiceLazy(this IServiceCollection services, Type type)
        //{
        //    return new Lazy<object?>(() => services.GetService(type), true);
        //}

        ///// <summary>
        ///// Returns a <see cref="Lazy{T}"/> to resolve a service from given <see cref="IServiceCollection"/>
        ///// once dependency injection registration phase completed.
        ///// </summary>
        //public static Lazy<T> GetRequiredServiceLazy<T>(this IServiceCollection services) where T : notnull
        //{
        //    return new Lazy<T>(services.GetRequiredService<T>, true);
        //}

        ///// <summary>
        ///// Returns a <see cref="Lazy{T}"/> to resolve a service from given <see cref="IServiceCollection"/>
        ///// once dependency injection registration phase completed.
        ///// </summary>
        //public static Lazy<object> GetRequiredServiceLazy(this IServiceCollection services, Type type)
        //{
        //    return new Lazy<object>(() => services.GetRequiredService(type), true);
        //}

        //public static IServiceProvider? GetServiceProviderOrNull(this IServiceCollection services)
        //{
        //    return services.GetObjectOrNull<IServiceProvider>();
        //}        //public static IServiceProvider BuildServiceProviderFromFactory([NotNull] this IServiceCollection services)
        //{

        //    foreach (var service in services)
        //    {
        //        var factoryInterface = service.NormalizedImplementationInstance()?.GetType()
        //            .GetTypeInfo()
        //            .GetInterfaces()
        //            .FirstOrDefault(i => i.GetTypeInfo().IsGenericType &&
        //                                 i.GetGenericTypeDefinition() == typeof(IServiceProviderFactory<>));

        //        if (factoryInterface == null)
        //        {
        //            continue;
        //        }

        //        var containerBuilderType = factoryInterface.GenericTypeArguments[0];
        //        return (IServiceProvider)typeof(ServiceCollectionCommonExtensions)
        //            .GetTypeInfo()
        //            .GetMethods()
        //            .Single(m => m.Name == nameof(BuildServiceProviderFromFactory) && m.IsGenericMethod)
        //            .MakeGenericMethod(containerBuilderType)
        //            .Invoke(null, new object?[] { services, null })!;
        //    }

        //    return services.BuildServiceProvider();
        //}

        //public static IServiceProvider BuildServiceProviderFromFactory<TContainerBuilder>([NotNull] this IServiceCollection services, Action<TContainerBuilder>? builderAction = null) where TContainerBuilder : notnull
        //{

        //    var serviceProviderFactory = services.GetSingletonInstanceOrNull<IServiceProviderFactory<TContainerBuilder>>();
        //    if (serviceProviderFactory == null)
        //    {
        //        throw new Exception($"Could not find {typeof(IServiceProviderFactory<TContainerBuilder>).FullName} in {services}.");
        //    }

        //    var builder = serviceProviderFactory.CreateBuilder(services);
        //    builderAction?.Invoke(builder);
        //    return serviceProviderFactory.CreateServiceProvider(builder);
        //}

        ///// <summary>
        ///// Resolves a dependency using given <see cref="IServiceCollection"/>.
        ///// This method should be used only after dependency injection registration phase completed.
        ///// </summary>
        //internal static T? GetService<T>(this IServiceCollection services)
        //{
        //    return services
        //        .GetSingletonInstance<IAbpApplication>()
        //        .ServiceProvider
        //        .GetService<T>();
        //}

        ///// <summary>
        ///// Resolves a dependency using given <see cref="IServiceCollection"/>.
        ///// This method should be used only after dependency injection registration phase completed.
        ///// </summary>
        //internal static object? GetService(this IServiceCollection services, Type type)
        //{
        //    return services
        //        .GetSingletonInstance<IAbpApplication>()
        //        .ServiceProvider
        //        .GetService(type);
        //}

        ///// <summary>
        ///// Resolves a dependency using given <see cref="IServiceCollection"/>.
        ///// Throws exception if service is not registered.
        ///// This method should be used only after dependency injection registration phase completed.
        ///// </summary>
        //public static T GetRequiredService<T>(this IServiceCollection services) where T : notnull
        //{
        //    return services
        //        .GetSingletonInstance<IAbpApplication>()
        //        .ServiceProvider
        //        .GetRequiredService<T>();
        //}

        ///// <summary>
        ///// Resolves a dependency using given <see cref="IServiceCollection"/>.
        ///// Throws exception if service is not registered.
        ///// This method should be used only after dependency injection registration phase completed.
        ///// </summary>
        //public static object GetRequiredService(this IServiceCollection services, Type type)
        //{
        //    return services
        //        .GetSingletonInstance<IAbpApplication>()
        //        .ServiceProvider
        //        .GetRequiredService(type);
        //}

        ///// <summary>
        ///// Returns a <see cref="Lazy{T}"/> to resolve a service from given <see cref="IServiceCollection"/>
        ///// once dependency injection registration phase completed.
        ///// </summary>
        //public static Lazy<T?> GetServiceLazy<T>(this IServiceCollection services)
        //{
        //    return new Lazy<T?>(services.GetService<T>, true);
        //}

        ///// <summary>
        ///// Returns a <see cref="Lazy{T}"/> to resolve a service from given <see cref="IServiceCollection"/>
        ///// once dependency injection registration phase completed.
        ///// </summary>
        //public static Lazy<object?> GetServiceLazy(this IServiceCollection services, Type type)
        //{
        //    return new Lazy<object?>(() => services.GetService(type), true);
        //}

        ///// <summary>
        ///// Returns a <see cref="Lazy{T}"/> to resolve a service from given <see cref="IServiceCollection"/>
        ///// once dependency injection registration phase completed.
        ///// </summary>
        //public static Lazy<T> GetRequiredServiceLazy<T>(this IServiceCollection services) where T : notnull
        //{
        //    return new Lazy<T>(services.GetRequiredService<T>, true);
        //}

        ///// <summary>
        ///// Returns a <see cref="Lazy{T}"/> to resolve a service from given <see cref="IServiceCollection"/>
        ///// once dependency injection registration phase completed.
        ///// </summary>
        //public static Lazy<object> GetRequiredServiceLazy(this IServiceCollection services, Type type)
        //{
        //    return new Lazy<object>(() => services.GetRequiredService(type), true);
        //}

        //public static IServiceProvider? GetServiceProviderOrNull(this IServiceCollection services)
        //{
        //    return services.GetObjectOrNull<IServiceProvider>();
        //}
    }

}
