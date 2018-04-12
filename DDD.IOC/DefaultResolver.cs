using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DDD.IOC
{
    public class DefaultResolver : IResolver
    {
        private readonly IReadOnlyDictionary<Type, List<Registration>> _registrations;
        private readonly Type[] _emptyTypeArray = { };
        private readonly IResolverContext _resolverContext;
        public DefaultResolver(IReadOnlyDictionary<Type, List<Registration>> registrations)
        {
            _registrations = registrations;
            _resolverContext = new DefaultResolverContext(this);
        }
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }
        public object Resolve(Type serviceType)
        {
            List<Registration> registrations;
            var genericArguments = _emptyTypeArray;
            var typeInfo = serviceType.GetTypeInfo();

            if (typeInfo.IsGenericType &&
                !_registrations.ContainsKey(serviceType))
            {
                genericArguments = typeInfo.GetGenericArguments();
                serviceType = typeInfo.GetGenericTypeDefinition();
            }

            if (_registrations.TryGetValue(serviceType, out registrations))
            {
                return registrations.First().Create(_resolverContext, genericArguments);
            }
            return null;
        }
        public IEnumerable<object> ResolveAll(Type serviceType)
        {
            List<Registration> registrations;
            return _registrations.TryGetValue(serviceType, out registrations)
                ? registrations.Select(a => a.Create(_resolverContext, _emptyTypeArray)) :
                Enumerable.Empty<object>();
        }
        public bool HasRegistrationFor<T>() where T : class
        {
            return _registrations.ContainsKey(typeof(T));
        }
    }
}