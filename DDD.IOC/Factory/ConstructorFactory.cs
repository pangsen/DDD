using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DDD.IOC
{
    public class ConstructorFactory : IFactory
    {
        private readonly ConstructorInfo _constructorInfo;
        private readonly IReadOnlyCollection<ParameterInfo> _paramterInfos;
        private readonly object[] _parameters;
        public ConstructorFactory(Type serviceType, object[] parameters=null)
        {
            _parameters = parameters;
            var constructorInfos = serviceType.GetTypeInfo().GetConstructors();
            if (constructorInfos.Length > 1)
            {
                throw new Exception($"Type {serviceType.Name} has more than one constructor");
            }

            _constructorInfo = constructorInfos.Single();
            _paramterInfos = _constructorInfo.GetParameters();
        }
        public object Create(IResolverContext resolverContext, Type[] genericTypeArguments)
        {
            if (_parameters != null)
            {
                return _constructorInfo.Invoke(_parameters);
            }
            var parameters = new object[_paramterInfos.Count];
            foreach (var parameterInfo in _paramterInfos)
            {
                parameters[parameterInfo.Position] = resolverContext.Resolver.Resolve(parameterInfo.ParameterType);
            }
            return _constructorInfo.Invoke(parameters);
        }
    }
}