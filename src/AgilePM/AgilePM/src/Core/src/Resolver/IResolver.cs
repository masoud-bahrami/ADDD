using System;
using AgilePM.Core.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace AgilePM.Core.Resolver
{
    public interface IResolver
    {
        T Resolve<T>();
        object Resolve(Type type);
    }

    public class ServiceCollectionResolver : IResolver
    {

        private readonly ServiceProvider _serviceProvider;

        public ServiceCollectionResolver(ServiceProvider serviceCollection) => _serviceProvider = serviceCollection;

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type type)
        {
            var component = _serviceProvider.GetService(type);
            if (component != null)
                return component;

            throw new NotRegisteredDependencyException(type);
        }
    }
}