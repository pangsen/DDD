using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DDD.IOC
{
    public class Registration : IDisposable
    {
        private readonly Type _serviceType;
        private readonly IFactory _factory;
        private readonly Lifetime _lifetime;
        private readonly Dictionary<Type, object> _singletons = new Dictionary<Type, object>();
        public Registration(Type serviceType, IFactory factory, Lifetime lifetime)
        {
            _serviceType = serviceType;
            _factory = factory;
            _lifetime = lifetime;
        }

        public void Dispose()
        {
            foreach (var singleton in _singletons.Values)
            {
                (singleton as IDisposable)?.Dispose();
            }

            _singletons.Clear();
        }

        public object Create(IResolverContext resolverContext, Type[] genericArguments)
        {
            var serviceType = genericArguments.Any() && _serviceType.GetTypeInfo().IsGenericType
                ? _serviceType.MakeGenericType(genericArguments)
                : _serviceType;
            if (_lifetime == Lifetime.Singleton)
            {
                object singleton;
                if (!_singletons.TryGetValue(serviceType, out singleton))
                {
                    singleton = _factory.Create(resolverContext, genericArguments);
                    _singletons.Add(serviceType, singleton);
                }
                return singleton;
            }
            return _factory.Create(resolverContext, genericArguments);
        }

        ~Registration()
        {
            Dispose();
        }
    }
}